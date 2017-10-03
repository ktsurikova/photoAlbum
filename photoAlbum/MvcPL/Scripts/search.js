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

//function onSelected(data) {
//    $('#loadMore').empty();
//    $('#images').empty();
//    var url = '/Photos/Search';
//    var dataSend = {
//        tag: data
//    };
//    $.post(
//        url,
//        dataSend,
//        function (response) {
//            DisplayImages(response);
//        });
//}

function onSelected(data) {
    var url = '/Photos/Search';
    var dataSend = {
        tag: data
    };
    $.post(
        url,
        dataSend,
        function (response) {
            ShowImagesAndPagination(response);
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
                        'class': 'viewImage'
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









function GetImages(url, page) {
    event.preventDefault();

    var data = {
        page: page
    };
    $.post(url, data,
        function (response) {
            ShowImages(response);
        });
}

function ShowImagesAndPagination(data) {

    ShowImages(data);
    ListOfPages(data.PageInfo);
    //$('#listOfPages').empty();
    //for (var i = 0; i < data.PageInfo.TotalPages; i++) {
    //    $('#listOfPages').append('<a href="#" onclick="GetIamges(' +
    //        data.PageInfo.UrlPart +
    //        ', ' +
    //        (i + 1) +
    //        ')">' +
    //        (i + 1) +
    //        '</a>');
    //}
}

function ListOfPages(data) {
    var i;
    for ( i = 1; i <= data.TotalPages; i++) {
        $('#page' + i).attr('onclick', 'GetImages(\'' + data.UrlPart + '\', ' + (i) + ')');
        $('#page' + i).show();
    }
    while (i < 5) {
        $('#page' + i).hide();
        i++;
    }
}


function ShowImages(data) {
    var i = 1;

    $.each(data.Items,
        function (index, value) {
            $('#image-' + i).show();
            $('#fullsize-' + i).attr('href', value.ImageUrl);
            $('#img-' + i).attr('src', value.ImageUrl);
            $('#details-' + i).attr('href', value.ImageDetailsUrl);
            $('#details-' + i).attr('data-src', value.ImageDetailsUrl);
            i++;
        });

    for (var j = i; j <= data.PageInfo.PageSize; j++) {
        $('#image-' + j).hide();
    }
}
