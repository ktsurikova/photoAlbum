$(document).ready(function () {

    var ImageGalleryModule = (function() {

        function onSelected(data) {
            var url = '/Photos/Search';
            var dataSend = {
                tag: data
            };
            //$('.activeLink').removeClass('activeLink');
            $.post(
                url,
                dataSend,
                function(response) {
                    ShowImagesAndPagination(response);
                });
        }

        function DisplayImages(data) {
            if (data.Items.length == 0) {
                $('#images').prepend("<div class=\"emptySearch\">Sorry, there are no results for your search</div>");
            } else {
                $.each(data.Items,
                    function(index, value) {
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

                        $('<div><a data-fancybox data-type="ajax" data-src="/Photos/PhotoDetails/' +
                                value.Id +
                                '" href="/Photos/PhotoDetails/' +
                                value.Id +
                                '" class="viewDetails">View Details</a><div>')
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

        function ShowImagesAndPagination(data) {

            ShowImages(data);
            ListOfPages(data.PageInfo);

        }

        function ListOfPages(data) {
            var i;
            for (i = 1; i <= data.TotalPages; i++) {
                $('#page' + i).attr('href', data.UrlPart);
                $('#page' + i).attr('page', i);
                $('#page' + i).show();
            }
            while (i < 5) {
                $('#page' + i).hide();
                i++;
            }

            //$('#page' + data.PageNumber).addClass('activeLink');
        }

        function ShowImages(data) {
            var i = 1;

            $.each(data.Items,
                function(index, value) {
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

        return{
            onSelected: onSelected,
            DisplayImages: DisplayImages,
            ShowImages: ShowImages
        }
    })();

    $(function () {
        var autocompleteUrl = '/Photos/Find';
        $("#tag").autocomplete({
            source: autocompleteUrl,
            minLength: 1,
            select: function (event, ui) {
               ImageGalleryModule.onSelected(ui.item.label);
            }
        });
    });

    $('#searchForm').submit(function (event) {
        event.preventDefault();
        ImageGalleryModule.onSelected($('#tag').val());
    });



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
                    ImageGalleryModule.DisplayImages(response);
                });
        });

    $('#loadMoreForm').submit(function (event) {
        event.preventDefault();

        var url = $('#loadMoreForm').attr('action');
        var data = $('#loadMoreForm').serialize();
        $('#loadMore').empty();
        $.post(url, data,
            function (response) {
                ImageGalleryModule.DisplayImages(response);
            });
    });

    $('.anotherPage').click(function (event) {

        event.preventDefault();
        var data = {
            page: $(this).attr('page')
        };

        //$('.activeLink').removeClass('activeLink');
        //$(this).addClass('activeLink');

        $.post($(this).attr('href'), data,
            function (response) {
                ImageGalleryModule.ShowImages(response);
            });

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