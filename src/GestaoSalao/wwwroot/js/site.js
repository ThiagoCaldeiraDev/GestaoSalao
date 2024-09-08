// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//var site = (function () {
//(function() {
//    $.fn.uploadAjax = function(url, model) {
//        var form = $(this);

//        var formData = new FormData();
//        form.find('input:file').each(function() {
//            var input = $(this),
//                name = input.prop('name'),
//                files = input.get(0).files;

//            if (files && files.length)
//                $(files).each(function(index, item) {
//                    formData.append(name, item);
//                });
//        });

//        model = model || {};
//        $.each(model.split('&'), function (index, value) {
//            var pair = value.split('=');
//            formData.append(decodeURIComponent(pair[0]), decodeURIComponent(pair[1] || ''));
//        });

//        return $.ajax({
//            url: url,
//            type: 'POST',
//            cache: false,
//            contentType: false,
//            processData: false,
//            data: formData
//        });
//    };
//    });
//})();