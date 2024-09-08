const messenger = {
    setErrorMessage: function (message, title) {

        message = message ? message : "Um erro ocorreu. Por favor tente novamente.";

        title = title ? title : "Erro";

        return swal({
            title: title,
            text: message,
            icon: "error",
            button: "OK",
        });
    },

    uploadAjax : function (form, url, model) {

        var formData = new FormData();
        form.find('input:file').each(function () {
            var input = $(this),
                name = input.prop('name'),
                files = input.get(0).files;

            if (files && files.length)
                $(files).each(function (index, item) {
                    formData.append(name, item);
                });
        });

        model = model || {};

        $.each(model.split('&'), function (index, value) {
            var pair = value.split('=');
            formData.append(decodeURIComponent(pair[0]), decodeURIComponent(pair[1] || ''));
        });

        return $.ajax({
            url: url,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            data: formData
        });
    },

    setSuccessMessage: function (message, redirectUrl, title) {

        message = message ? message : "Operação efetuada com sucesso!";

        title = title ? title : "Sucesso!";

        if (redirectUrl == undefined || redirectUrl == '') {

            return swal({
                title: "Sucesso",
                text: message,
                icon: "success",
                button: "OK",
            });

        }

        return swal({
            title: "Sucesso",
            text: message,
            icon: "success",
            button: "OK",
        }).then((result) => {
            location.href = redirectUrl;
        });
    },

    setInfoMessage: function (message, title) {

        message = message ? message : "Isto requer sua atenção!";

        title = title ? title : "Atenção!";

        return swal({
            title: title,
            text: message,
            icon: "info",
            button: "OK",
        });
    },

    setConfirmMessage: function (message, title, useHtml, dangerMode = false, type = "warning") {
        return swal({
            title: title,
            html: useHtml ? message : null,
            text: useHtml ? null : message,
            type: type,
            buttons: true,
            dangerMode: dangerMode,
            confirmButtonText: "Sim",
            cancelButtonText: "Não",
            closeOnConfirm: false,
            closeOnCancel: true,
            showCancelButton: true
        });
    }
};