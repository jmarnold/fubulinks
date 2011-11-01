var FubuLinks = window["FubuLinks"] = {
    Models: {},
    Views: {},
    start: function (links) {
        var theLinks = new FubuLinks.Models.Links();
        var linksView = new FubuLinks.Views.LinksView({
            el: '#links',
            collection: theLinks
        });

        links = links ? links : [];
        for (var i = 0; i < links.length; i++) {
            theLinks.add(links[i]);
        }

        // fire this off if ajaxcontinuation is successful
        amplify.subscribe('LinkAdded', function (link) {
            theLinks.add(link);
        });
    }
};        // namespacing

FubuLinks.Models.Link = Backbone.Model.extend({});
FubuLinks.Models.Links = Backbone.Collection.extend({
    model: FubuLinks.Models.Link
});

FubuLinks.Views.LinkView = Backbone.View.extend({
    template: '#link-template',
    initialize: function () {
        var tmpl = $(this.template).html();
        this.template = _.template(tmpl);
    },
    model: FubuLinks.Models.Link,
    render: function () {
        $(this.el).html(this.template({ model: this.model.toJSON() }));
    }
});

FubuLinks.Views.LinksView = Backbone.View.extend({
    initialize: function () {
        this.collection.bind('add', this.onLinkAdded, this);
    },
    onLinkAdded: function (link) {
        var view = new FubuLinks.Views.LinkView({ model: link });
        view.render();
        $(this.el).find('tbody').append($(view.el).find('tr'));
    }
});

$(document).ready(function () {
    $('#ShortenUrlForm').submit(function () {
        $(this).ajaxSubmit({
            success: function (continuation) {
                // technically, you can standarize this pretty easily
                // sigh...codebetter TC is still broken. when it's back up, this won't be necessary
                if (!continuation.success && !continuation.Success) {
                    // handle the error
                    return;
                }

                // otherwise, we're good to go
                amplify.publish('LinkAdded', continuation.link);
            }
        });
        return false;
    });
});