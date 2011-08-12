$(document).ready(function () {
    $('button').button();
    var toggleHover = function () {
        $(this).toggleClass('ui-state-hover');
    };

    $('.grid > thead > tr > th').each(function () {
        $(this)
				.addClass('ui-state-default')
				.addClass('ui-th-column')
				.addClass('ui-th-ltr');

        $(this).hover(toggleHover, toggleHover);
    });

    $('.grid > tbody > tr:even').each(function () {
        $(this).addClass('even');
    });
});