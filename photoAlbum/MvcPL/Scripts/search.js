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
                        'data-fancybox': 'images'
                    });
                link.append(
                    $('<img />').attr({
                        'src': '/Photos/ShowImage/' + value.Id,
                        //"data:image/png;base64," + _arrayBufferToBase64(value.Image),
                        'height': '350px',
                        'width': 'auto'
                    })
                ).appendTo(imdiv);
            });

        if (data.PageInfo.TotalPages != data.PageInfo.PageNumber) {
            //var imdiv = $('<div/>',
            //    {
            //        id: 'loadMore'
            //    }).appendTo('.images');

            var mform = $("<form/>",
                {
                    action: '/Photos/Search', //?tag='+ data.PageInfo.Tag + '&page=' + (+data.PageInfo.PageNumber + +1),
                    method: 'post',
                    tag: data.PageInfo.Tag,
                    page: (+data.PageInfo.PageNumber + +1),
                    class: 'loadMoreForm',
                    id: 'loadMoreForm'
                }).appendTo('#loadMore');
            mform.append($('<button/>',
                {
                    text: 'Load more',
                    class: 'btn my-2 my-sm-0 buttonLoadMore',
                    id: 'buttonLoadMore'

                }));


            //var temp = '<a class="btn btn-outline-success my-2 my-sm-0 fuckingYou" id="fuckingYou" onclick="getPagedData(' +
            //    data.PageInfo.Tag + ', ' + (+data.PageInfo.PageNumber + +1) + ')" href="#">Load more&nbsp;&nbsp;</a>';
            //imdiv.append(temp);
        }
    }
}

$('#loadMore').on('click',
    'form.loadMoreForm',
    function (event) {
        //console.log("click");
        event.preventDefault();
        var url = $('#loadMoreForm').attr('action');
        var data = {
            tag: $('#loadMoreForm').attr('tag'),
            page: $('#loadMoreForm').attr('page')
        }
        //console.log($('#loadMoreForm').attr('action'));
        //console.log($('#loadMoreForm').attr('tag'));
        //console.log($('#loadMoreForm').data());
        //console.log($('#loadMoreFrom').data('tag'));
        //var data = $('#loadMoreForm').serialize();
        $('#loadMore').empty();
        $.post(url, data,
            function (response) {
                DisplayImages(response);
            });
    });


//function getPagedData(tag, page) {
//    event.preventDefault();
//    $('#loadMore').remove();
//    $.getJSON("/Photos/Search", { tag: tag, page: page }, function (response) {
//        DisplayImages(response);
//    });
//}

$('#loadMoreForm').submit(function (event) {
    event.preventDefault();

    var url = $('#loadMoreForm').attr('action');
    //console.log($('#loadMoreForm').attr('action'));
    var data = $('#loadMoreForm').serialize();
    $('#loadMore').empty();
    $.post(url, data,
        function (response) {
            DisplayImages(response);
        });
});

