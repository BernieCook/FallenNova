$(function () {

    if ($("#bookmark").length == 0) {
        return;
    }

    $('#bookmark').click(function () {
        var jsonData =
        {
            AddBookmark: true,
            Url: location.href,
            Title: $(document).attr('title')
        };

        $.ajax({
            type: 'POST',
            url: '/api/bookmark/',
            data: JSON.stringify(jsonData),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                $('#bookmark').toggleClass('btn-primary');
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    });
});
