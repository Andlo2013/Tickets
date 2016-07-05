//Script: Sube el archivo y visuliza el progreso
$(document).on('ready', _initializeUpload);

//Inicializa este script
function _initializeUpload() {

    $("#SoporteTable").on("click", ".fileUpload", _loadFile);
    
}

//funcion de jquery
function _loadFile() {
   'use strict';
    // Adjunta el ID de respuesta (data-name) con el número de imagen (data-numeroimg) y envía al servidor
   var url = '/Cliente/Soporte/UploadFile/' + $(this).data('name')+"?img="+$(this).data('numeroimg');
    $('.fileUpload').fileupload({
        url: url,
        dataType: 'json',
        done: function (e, data) {
            //Hace un click por código en el botón ver del ticket para refrescar los cambios
            var name = $(this).data('ticket');
            $("#" + name).trigger('click');
        },
        progressall: function (e, data) {
            //Identificador de respuesta (data-name)
            var identifier = $(this).data('name');
            //Número de imágen (data-numeroimg)
            var img = $(this).data('numeroimg');
            var progress = parseInt(data.loaded / data.total * 100, 10);
            $('#progress'+identifier+img+' .progress-bar').css(
                'width',
                progress + '%'
            );

        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
}

