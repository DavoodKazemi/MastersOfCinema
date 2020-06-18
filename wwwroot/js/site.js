// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function readURL(input) {
    if (input.files && input.files[0]) {

        var reader = new FileReader();

        
        reader.onload = function (e) {
            $('#blah').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]); // convert to base64 string
    }
}

$("#imgInp").change(function () {
    if ($('#imgInp').get(0).files.length !== 0) {

        readURL(this);
    }
});
