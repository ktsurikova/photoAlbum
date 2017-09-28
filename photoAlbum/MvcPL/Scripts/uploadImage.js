$('.new_Btn').bind("click", function () {
    $('#ImageFile').click();
});

$(document).ready(function () {
    $("#ImageFile").change(function () {
        var File = this.files;
        if (File && File[0]) {
            ReadImage(File[0]);
        }
    });
});


var ReadImage = function (file) {

    var reader = new FileReader;
    var image = new Image;

    reader.readAsDataURL(file);
    reader.onload = function (_file) {

        image.src = _file.target.result;
        image.onload = function () {

            $("#targetImg").attr('src', _file.target.result);
            $("#imgPreview").show();
            $("#UploadButton").show();

        }
    }
}