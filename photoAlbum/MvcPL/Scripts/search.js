$(function () {
    var autocompleteUrl = '@Url.Action("Find", "Photos")';
    $("#tag").autocomplete({
        source: autocompleteUrl,
        minLength: 1,
        select: function (event, ui) {
            onSelected(ui.item.label);
        }
    });
});

function onSelected(data) {
    $('#images').empty();
    var url = '@Url.Action("Search", "Photos")';
    var dataSend = {
        tag: data
    };
    $.post(
        url,
        dataSend,
        function (response) {
            DisplayImages(response);
        });
}

function _arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}

function DisplayImages(data) {
    if (data.length == 0) {
        $('#images').prepend("<div class=\"emptySearch\">Sorry, there are no results for your search</div>");
    } else {
        $.each(data,
            function (index, value) {
                var imdiv = $('<div/>',
                    {
                        id: 'image',
                        class: 'col-lg-4 col-md-6'
                    });
                imdiv.append(
                    $('<img />').attr({
                        'src': "data:image/png;base64," + _arrayBufferToBase64(value.Image),
                        'height': '350px',
                        'width': 'auto'
                    })
                ).appendTo('.images');
            });
        $.each(data,
            function (index, value) {
                var imdiv = $('<div/>',
                    {
                        id: 'image',
                        class: 'col-lg-4 col-md-6'
                    });
                imdiv.append(
                    $('<img />').attr({
                        'src': "data:image/png;base64," + _arrayBufferToBase64(value.Image),
                        'height': '350px',
                        'width': 'auto'
                    })
                ).appendTo('.images');
            });
        $.each(data,
            function (index, value) {
                var imdiv = $('<div/>',
                    {
                        id: 'image',
                        class: 'col-lg-4 col-md-6'
                    });
                imdiv.append(
                    $('<img />').attr({
                        'src': "data:image/png;base64," + _arrayBufferToBase64(value.Image),
                        'height': '350px',
                        'width': 'auto'
                    })
                ).appendTo('.images');
            });

    }
}
