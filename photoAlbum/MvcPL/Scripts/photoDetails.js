$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

$('.popover-dismiss').popover({
    trigger: 'focus'
});

//$('#like').click(function (event) {
//    event.preventDefault();

//    var url = $('#like').attr('href');
//    $.post(url,
//        function(response) {
//            $('#rating').empty();
//            $('#rating').append(response);
//        });
//});

//$('#dislike').click(function (event) {
//    event.preventDefault();

//    var url = $('#dislike').attr('href');
//    $.post(url,
//        function (response) {
//            $('#rating').empty();
//            $('#rating').append(response);
//        });
//});

$('#rating').on('click',
    'a.rat',
    function (event) {
        event.preventDefault();
        console.log('it`s me');
        var url = $('.rat').attr('href');
        $.get(url,
            function (response) {
                $('#rating').empty();
                $('#rating').append(response);
            });
    });

$('#addCommentButton').click(function (event) {
    event.preventDefault();
    if ($('#addComment').val().trim().length != 0) {
        var url = $('#addCommentButton').attr('href');
        var data = {
            PhotoId: $('#addCommentButton').attr('photo'),
            Text: $('#addComment').val()
        }
        $.post(url,
            data,
            function (response) {
                $('#addComment').val('');
                $('#comments').empty();
                $('#comments').append(response);
            });
    }
});

//$('#loadMoreComment').click(function (event) {
//    event.preventDefault();
//    var url = $('#loadMoreComment').attr('href');
//    var data = {
//        id: $('#comments').attr('photo'),
//        page: $('#loadMoreComment').attr('page')
//    }
//    $.post(url,
//        data,
//        function (response) {
//            console.log(response);
//            $('#loadMoreComment').empty();
//            $('#comments').prepend(response);
//        });
//});

$('#comments').on('click',
    'a.loadMoreComment',
    function (event) {
        event.preventDefault();
        var url = $('#loadMoreComment').attr('href');
        var data = {
            id: $('#comments').attr('photo'),
            page: $('#loadMoreComment').attr('page')
        }
        $.post(url,
            data,
            function (response) {
                $('#loadMoreComment').empty();
                $('#comments').prepend(response);
            });
    });