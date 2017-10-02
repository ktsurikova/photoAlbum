var numberofImages = 1;

$(function () {
    var autocompleteUrl = '/Photos/Find';
    $("#tag").autocomplete({
        source: autocompleteUrl,
        minLength: 1,
        select: function (event, ui) {
            onSelected(ui.item.label);
        }
    });
});

function onSelected(data) {
    $('#loadMore').empty();
    $('#images').empty();
    var url = '/Photos/Search';
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

$('#searchForm').submit(function (event) {
    event.preventDefault();
    onSelected($('#tag').val());
});


function DisplayImages(data) {
    if (data.Items.length == 0) {
        $('#images').prepend("<div class=\"emptySearch\">Sorry, there are no results for your search</div>");
    } else {
        $.each(data.Items,
            function (index, value) {
                var imdiv = $('<div/>',
                    {
                        id: 'image',
                        class: 'col-lg-4 col-md-6'
                    }).appendTo('.images');
                var link = $('<a />',
                    {
                        'href': '/Photos/ShowImage/' + value.Id,
                        'data-fancybox': 'images',
                        'data-type': 'image',
                    });
                link.append(
                    $('<img />').attr({
                        'src': '/Photos/ShowImage/' + value.Id,
                        'class':'viewImage'
                    })
                ).appendTo(imdiv);

                $('<div><a data-fancybox data-type="ajax" data-src="/Photos/PhotoDetails/' + value.Id
                    + '" href="/Photos/PhotoDetails/' + value.Id + '" class="viewDetails">View Details</a><div>')
                    .appendTo(imdiv);


            });

        if (data.PageInfo.TotalPages != data.PageInfo.PageNumber) {

            var mform = $("<form/>",
                {
                    action: '/Photos/Search',
                    method: 'post',
                    tag: data.PageInfo.Tag,
                    page: (+data.PageInfo.PageNumber + +1),
                    class: 'loadMoreForm',
                    id: 'loadMoreForm'
                }).appendTo('#loadMore');
            mform.append($('<button/>',
                {
                    text: 'Load more',
                    class: 'btn my-2 my-sm-0 signUpButton',
                    id: 'buttonLoadMore'

                }));
        }
    }
}

$('#loadMore').on('click',
    'form.loadMoreForm',
    function (event) {
        event.preventDefault();
        var url = $('#loadMoreForm').attr('action');
        var data = {
            tag: $('#loadMoreForm').attr('tag'),
            page: $('#loadMoreForm').attr('page')
        }
        $('#loadMore').empty();
        $.post(url, data,
            function (response) {
                DisplayImages(response);
            });
    });

$('#loadMoreForm').submit(function (event) {
    event.preventDefault();

    var url = $('#loadMoreForm').attr('action');
    var data = $('#loadMoreForm').serialize();
    $('#loadMore').empty();
    $.post(url, data,
        function (response) {
            DisplayImages(response);
        });
});


$('#AnotherPage').submit(function (event) {
    event.preventDefault();

    var url = $('#AnotherPage').attr('action');
    var data = $('#AnotherPage').serialize();

    $.post(url, data,
        function (response) {
            ShowImages(response);
        });
});

function ShowImages(data) {
    var i = 1;

    $.each(data,
        function (index, value) {
            $('#image-' + i).attr('display', 'block');
            $('#fullsize-' + i).attr('href', '/Photos/ShowImage/' + value.Id);
            $('#img-' + i).attr('src', '/Photos/ShowImage/' + value.Id);
            $('#details-' + i).attr('href', '/Photos/PhotoDetails/' + value.Id);
            $('#details-' + i).attr('data-src', '/Photos/PhotoDetails/' + value.Id);
            i++;
        });
}
