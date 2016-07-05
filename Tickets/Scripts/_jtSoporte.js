$(document).on("ready", _iniJTSoporte);

//Pone en español jtable
$.extend(true, $.hik.jtable.prototype.options.messages, spanishMessages);

//Inicializa este script
function _iniJTSoporte() {
    
    _masterTable();
    $('#buscar').on('click', _filtro);
    $('#buscar').trigger("click");
}

//Crea la tabla principal
function _masterTable() {
    $('#SoporteTable').jtable({
        messages: spanishMessages,
        title: 'Tickets ingresados',
        sorting: false,
        paging: true, //Enable paging
        pageSize: 15,
        _textNewRecord: 'Nuevo Ticket',
        _cssRowStyle:
            [{ campo: 'newMSG', operador: '>', valor: 0,clase:'negrita' },
             { campo: 'EstadoID', operador: '==', valor: 3, clase: 'ticketGreen' },
            { campo: 'EstadoID', operador: '==', valor: 4, clase: 'ticketRed' },
            { campo: 'EstadoID', operador: '==', valor: 5, clase: 'ticketOrange' },
            { campo: 'PrioridadID', operador: '==', valor: 2, clase: 'ticketRed' }],
    
        actions: {
            listAction: '/Cliente/soporte/listarTicket',
            createAction: '/Cliente/Soporte/nuevoTicket'
        },

        //CodTicket	Categoria	Usuario	Fecha	Tecnico	Prioridad	Estado

        fields: {

            id: {
                title: 'Ticket',
                width: '7%',
                create: false
            },

            TeamViewer: {
                title: "TeamViewer",
                create: true,
                maxlength: 12,
                visibility: 'hidden'
            },

            Fecha: {
                title: 'Fecha',
                width: '10%',
                type: 'date',
                create: false
            },

            Pregunta: {
                title: 'Pregunta',
                width: '35%',
                sorting: false,
                maxlength:150,
                type:'textarea'
            },

            Prioridad: {
                title: 'Prioridad',
                width: '12%',
                create: false
            },

            Estado: {
                title: 'Estado',
                width: '8%',
                create: false
            },

            Categoria: {
                title: 'Categoría',
                width: '15%',
                create: false,
                listClass: "ocultarXS"
            },

            Tiempo: {
                title: 'Tiempo',
                width: '5%',
                create: false,
                listClass: "ocultarXS"
            },

            newMSG: {
                title: 'MSG',
                width: '5%',
                create: false
            },

            //CHILD TABLE 
            Detalle: {
                title: 'Ver',
                width: '4%',
                sorting: false,
                create: false,
                display:_detailTable
            }
        }
    });
}

//Crea la tabla detalle
function _detailTable (mainRecord) {
    //Create an image that will be used to open child table
    //var $img = $('<img class="icon" src="/images/detail.png" title="Ver respuestas"/>');
    var $img = $('<label id="' + mainRecord.record.UUID + '"class="btn btn-info" title="Ver respuestas">Ver</label>');
    //Open child table when user clicks the image
    $img.click(function () {
        $('#SoporteTable').jtable('openChildTable',
                $img.closest('tr'),
                {
                    title: 'Respuestas del ticket',
                    _textNewRecord: 'Responder Ticket',
                    paging: true, //Enable paging
                    pageSize: 10,
                    actions: {
                        listAction: '/Cliente/Soporte/listarAnswer/' + mainRecord.record.UUID,
                        createAction: '/Cliente/Soporte/nuevoAnswer/' + mainRecord.record.UUID
                    },

                    fields: {

                        SecRespta: {
                            title: 'Nro',
                            width: '5%',
                            create: false,
                            display: function (data) {
                                var linkDET = data.record.SecRespta;
                                var identifier = data.record.UUID;
                                var $link = $('<a href="/Cliente/Soporte/verAnswer/' + identifier + '">' + linkDET + '</a>');
                                $link.click(function () { });
                                return $link;
                            }
                        },

                        Fecha: {
                            title: 'Fecha',
                            width: '10%',
                            type: 'date',
                            create: false
                        },

                        Usuario: {
                            title: 'Usuario',
                            width: '15%',
                            create: false
                        },

                        TeamViewer: {
                            title: 'TeamViewer',
                            width: '0%',
                            create: true,
                            visibility: 'hidden'
                        },

                        Mensaje: {
                            title: 'Mensaje',
                            type: 'textarea',
                            maxlength: 150,
                            width: '50%'
                        },
                        
                        Observacion: {
                            title: 'Observación',
                            width: '0%',
                            type: 'textarea',
                            maxlength: 2000,
                            create: true,
                            visibility: 'hidden'
                        },

                        File1: {
                            title: 'File 1',
                            width: '10%',
                            create: false,
                            listClass: "ocultarXS",
                            display: function (data) {
                                var $link = _linkUpload(data.record, mainRecord.record.UUID, 1);
                                return $link;
                            }
                        },

                        File2: {
                            title: 'File 2',
                            width: '10%',
                            create: false,
                            listClass: "ocultarSmall",
                            display: function (data) {
                                var $link = _linkUpload(data.record, mainRecord.record.UUID, 2);
                                return $link;
                            }
                        },

                        File3: {
                            title: 'File 3',
                            width: '10%',
                            create: false,
                            listClass: "ocultarMedium",
                            display: function (data) {
                                var $link = _linkUpload(data.record, mainRecord.record.UUID, 3);
                                return $link;
                            }
                        },

                        Minutos: {
                            title: 'Minutos',
                            width: '10%',
                            create: false,
                            listClass: "ocultarXS"
                        }
                    }

                }, function (data) { //opened handler
                    data.childTable.jtable('load');
                });
    });
    //Return image to show on the person row
    return $img;
}

//Crea el link del 'FILEUPLOAD' para cada registro en cada columna.
function _linkUpload(record,ticketUUID,numeroImg) {
   
    //setencia base para crear la interfaz de fileupload en tiempo de ejecución
    var upload = '<div>' +
        '<span class="btn btn-info fileinput-button">' +
        '<i class="glyphicon glyphicon-plus"></i>' +
        '<span id="nombre">Adjuntar</span>' +
        '<input id="fileupload" data-ticket="ticket0" data-numeroImg="img0" data-name="name0" type="file" class="fileUpload" name="files[]" multiple="">' +
        '</span></br>' +
        '<div id="progress" class="barra">' +
        '<div class="progress-bar progress-bar-primary"></div>' +
        '</div>';
    //recuperamos propiedades para no utilizar nombres muy extensos
    var $link;
    var identifier = record.id;
    var fileName = "";
    switch (numeroImg) {
        case 1:
            fileName = record.File1;
            break;
        case 2:
            fileName = record.File2;
            break;
        case 3:
            fileName = record.File3;
            break;
    }
    
    //si tiene un nombre de archivo crea un link de descarga
    if (fileName != null && fileName != "File") {
        $link = $('<a href="/Uploads/' + identifier + '/' + fileName + '" download>' + fileName + '</a>');
    }
        //si no tiene un link de archivo crea el fileuploader identifcando cada controlador por su nombre
    else {
        $link = upload.replace("progress", "progress" + identifier+numeroImg);
        $link = $link.replace("fileupload", "fileupload" + identifier + numeroImg);
        $link = $link.replace("name0", identifier);
        $link = $link.replace("ticket0", ticketUUID);
        $link = $link.replace("img0", numeroImg);
    }
    return $link;
}

function _filtro(e) {
    e.preventDefault();
    $('#SoporteTable').jtable('load', {
        ticketNumero: $('#ticketnumero').val(),
        ticketEstado: $('#ticketestado').val()
    });
}

