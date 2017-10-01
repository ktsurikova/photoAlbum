$('#loadMore').on('click',
    'a.loadMorePhotos',
    function (event) {
        event.preventDefault();
        var url = $('#loadMorePhotos').attr('href');
        var data = {
            page: $('#loadMorePhotos').attr('page')
        }
        $('#loadMore').empty();
        $.post(url, data,
            function (response) {
                DisplayProfileImages(response);
            });
    });

function DisplayProfileImages(data) {
    if (data.Items.length == 0) {
        $('#userPhotos').prepend("<div class=\"emptySearch\">Upload your first photo</div>");
    } else {
        $.each(data.Items,
            function (index, value) {
                var imdiv = $('<div/>',
                    {
                        id: 'image',
                        class: 'col-lg-4 col-md-6'
                    }).appendTo('.userPhotos');
                var link = $('<a />',
                    {
                        'href': '/Photos/ShowImage/' + value.Id,
                        'data-fancybox': 'images',
                        'data-type': 'image'
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

            $('<a href="Profile/LoadMorePhotos" id="loadMorePhotos" class=" btn loadMorePhotos signUpButton" page="' +
                data.PageInfo.PageNumber +
                '"> Load more</a>').appendTo('#loadMore');
        }
    }
}