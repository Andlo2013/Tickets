$(document).on("ready", _initialize);

//Pone en español jtable
$.extend(true, $.hik.jtable.prototype.options.messages, spanishMessages);

function _initialize() {
    _masterTable();
    $("#limpiar").on("click", _btnlimpiar);
    $("#buscar").on("click", _btnfiltro);
    $("#buscar").trigger("click");
}

function _btnfiltro(e) {
    e.preventDefault();
    _buscar();
}

function _btnlimpiar(e) {
    e.preventDefault();
    _limpiar();
    _buscar();
}

function _buscar() {
    $('#SoporteTable').jtable('load', {
        ticketNumero: $('#ticketnumero').val(),
        ticketEmpresa: $('#ticketempresa').val(),
        ticketPrioridad: $('#ticketprioridad').val(),
        ticketEstado: $('#ticketestado').val()
    });
}

function _limpiar() {
    $('#ticketnumero').val('');
    $('#ticketempresa').val('');
    $('#ticketprioridad').val('0');
    $('#ticketestado').val('0');
}

function _masterTable() {
    $('#SoporteTable').jtable({
        title: 'Tickets ingresados',
        sorting: false,
        paging: true, //Enable paging
        pageSize: 15,
        _cssRowStyle:
            [{ campo: 'newMSG', operador: '>', valor: 0, clase: 'negrita' },
             { campo: 'PrioridadID', operador: '==', valor: 2, clase: 'ticketRed' }],
        actions: {
            listAction: '/supportSI/si_Ticket/listarTicket'
        },

        fields: {

            id: {
                title: 'Ticket',
                width: '5%',
                display: function (ticket) {
                    //Create an image that will be used to open child table
                    var $img = $('<a class="btn btn-default" title="Editar Ticket" href="/supportSI/si_Ticket/editarTicket/' + ticket.record.UUID + '">' + ticket.record.id + '</a>');
                    //Return image to show on the person row
                    return $img;
                }
            },

            Fecha: {
                title: 'Fecha',
                width: '10%',
                type: 'date'
            },

            Pregunta: {
                title: 'Pregunta',
                maxlength: 150,
                width: '35%',
                sorting: false
            },

            Empresa: {
                title: 'Empresa',
                width: '15%'
            },

            tipoPlan: {
                title: 'Plan',
                width: '10%',
                listClass: "ocultarXS"
            },

            Prioridad: {
                title: 'Prioridad',
                width: '12%'
            },

            Estado: {
                title: 'Estado',
                width: '8%',
                listClass: "ocultarXS"
            },

            Tiempo: {
                title: 'Tiempo',
                width: '5%',
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
                display: _detailTable
            }
        }
    });
    $('#SoporteTable');
}

function _detailTable(mainRecord) {
    //Create an image that will be used to open child table
    var $img = $('<label id="' + mainRecord.record.UUID + '" class="btn btn-info" title="Ver respuestas">Ver</label>');
    //Open child table when user clicks the image
    $img.click(function () {
        $('#SoporteTable').jtable('openChildTable',
                $img.closest('tr'),
                {
                    title: 'Respuestas del ticket',
                    paging: true, //Enable paging
                    pageSize: 10,
                    _textNewRecord: 'Responder Ticket',
                    actions: {
                        listAction: '/supportSI/si_Ticket/listarAnswer/' + mainRecord.record.UUID,
                        createAction: '/supportSI/si_Ticket/nuevoAnswer/' + mainRecord.record.UUID,
                    },
                    fields: {
                        SecRespta: {
                            title: 'Nro',
                            width: '5%',
                            create:false,
                            display: function (data) {
                                var linkDET = data.record.SecRespta;
                                var identifier = data.record.UUID;
                                var $link = $('<a href="/supportSI/si_Ticket/verAnswer/' + identifier + '">' + linkDET + '</a>');
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

                        Mensaje: {
                            title: 'Mensaje',
                            maxlength: 150,
                            width: '30%',
                            type: 'textarea',
                        },

                        Observacion: {
                            title: 'Observación',
                            width: '0%',
                            type: 'textarea',
                            maxlength: 2000,
                            create: true,
                            visibility: 'hidden'
                        },

                        TeamViewer: {
                            title: 'TeamViewer',
                            maxlength: 12,
                            width: '10%',
                            create: false
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
                            width: '5%',
                            type: 'number',
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
function _linkUpload(record, ticketUUID, numeroImg) {

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
        $link = upload.replace("progress", "progress" + identifier + numeroImg);
        $link = $link.replace("fileupload", "fileupload" + identifier + numeroImg);
        $link = $link.replace("name0", identifier);
        $link = $link.replace("ticket0", ticketUUID);
        $link = $link.replace("img0", numeroImg);
    }
    return $link;
}

