COMPILE_TARGET = ENV['config'].nil? ? "debug" : ENV['config']

include FileTest
require 'albacore'
load "VERSION.txt"

RESULTS_DIR = "results"
PRODUCT = "FubuLinks"
COPYRIGHT = 'Copyright 2011 Joshua Arnold, et al. All rights reserved.';
COMMON_ASSEMBLY_INFO = 'src/CommonAssemblyInfo.cs';
CLR_TOOLS_VERSION = "v4.0.30319"

buildsupportfiles = Dir["#{File.dirname(__FILE__)}/buildsupport/*.rb"]
raise "Run `git submodule update --init` to populate your buildsupport folder." unless buildsupportfiles.any?
buildsupportfiles.each { |ext| load ext }


tc_build_number = ENV["BUILD_NUMBER"]
build_revision = tc_build_number || Time.new.strftime('5%H%M')
build_number = "#{BUILD_VERSION}.#{build_revision}"
BUILD_NUMBER = build_number 


props = { :stage => File.expand_path("build"), :artifacts => File.expand_path("artifacts") }

desc "**Default**, compiles and runs tests"
task :default => [:compile, :virtual_dir, :unit_test]
task :ci => [:compile, :unit_test]

desc "Update the version information for the build"
assemblyinfo :version do |asm|
  asm_version = build_number
  
  begin
    commit = `git log -1 --pretty=format:%H`
  rescue
    commit = "git unavailable"
  end
  puts "##teamcity[buildNumber '#{build_number}']" unless tc_build_number.nil?
  puts "Version: #{build_number}" if tc_build_number.nil?
  asm.trademark = commit
  asm.product_name = PRODUCT
  asm.description = build_number
  asm.version = asm_version
  asm.file_version = build_number
  asm.custom_attributes :AssemblyInformationalVersion => asm_version
  asm.copyright = COPYRIGHT
  asm.output_file = COMMON_ASSEMBLY_INFO
end


def waitfor(&block)
  checks = 0
  until block.call || checks >10 
    sleep 0.5
    checks += 1
  end
  raise 'waitfor timeout expired' if checks > 10
end

desc "Compiles the app"
msbuild :compile => [:restore_if_missing, :version] do |msb|
	msb.command = File.join(ENV['windir'], 'Microsoft.NET', 'Framework', CLR_TOOLS_VERSION, 'MSBuild.exe')
	msb.properties :configuration => COMPILE_TARGET
	msb.solution = "src/#{PRODUCT}.sln"
    msb.targets :Rebuild
    msb.log_level = :verbose
end



desc "Run unit tests"
task :unit_test do 
  runner = NUnitRunner.new :compilemode => COMPILE_TARGET, :source => 'src', :platform => 'x86'
  runner.executeTests ['FubuLinks.Tests']
end

desc "Set up the virtual directories"
task :virtual_dir => [:compile] do
  dir = File.expand_path("src/FubuLinks.Web")
  fubu("createvdir #{dir} fubulinks")
end

def self.fubu(args)
  fubu = NuGet.tool("FubuMVC.References", "fubu.exe")
  fubu = Platform.runtime(fubu)
  sh "#{fubu} #{args}" 
end
