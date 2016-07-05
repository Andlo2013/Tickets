$(document).on("ready", _iniJTUsuario);

//Pone en español jtable
$.extend(true, $.hik.jtable.prototype.options.messages, spanishMessages);

//Inicializa este script
function _iniJTUsuario() {
    _usuarioTable();
    //Filtros
    $('#buscar').on('click', _filtro);
    $('#buscar').trigger("click");
    //nuevo
    $("#ui-id-3").on('click', "#btnGeneraPWD", _generaPWD);
    $("#ui-id-3").on('change', "#Edit-EmpresaRUC", _findEmpresaRUC);
    //edicion
    $("#ui-id-5").on('click', "#btnGeneraPWD", _generaPWD);
    $("#ui-id-5").on('change', "#Edit-EmpresaRUC", _findEmpresaRUC);
}

function _findEmpresaRUC() {
    var rucEmpresa = $("#Edit-EmpresaRUC").val();
    if ($.trim(rucEmpresa)) {
        _AjaxGET("/supportSI/si_Empresa/getEmpresaRUC?RUC=" + rucEmpresa, _showEmpresa);
    }
}

function _showEmpresa(data) {
    $("#Empresa").val(data.Records.EmpNom);
}

//Crea la tabla principal
function _usuarioTable() {
    $('#UsuarioTable').jtable({
        messages: spanishMessages,
        title: 'Listado de Usuarios',
        sorting: false,
        paging: true, //Enable paging
        pageSize: 15,
        actions: {
            listAction: '/supportSI/si_Usuario/listarUsuario',
            createAction: '/supportSI/si_Usuario/Create',
            updateAction: '/supportSI/si_Usuario/Edit'
        },

        fields: {

            UUID: {
                title: "UUID",
                visibility: "hidden",
                create: false,
                edit: true,
                type: "hidden",
                input: function (data) {
                    var UUID = typeof (data.value) != "undefined" ? data.value : "";
                    var $img = $('<input type="Text" readonly id="UUID" name="UUID" value="' + UUID + '"></label>');
                    return $img;
                }
            },

            EmpresaRUC: {
                title: "RUC",
                width: '5%',
                maxlength: 13,
                create: true,
                visibility:'hidden'
            },

            Empresa: {
                title: "Empresa",
                width: '30%',
                maxlength: 50,
                input: function (data) {
                    var empresa = typeof data.value == "undefined" ? "" : data.value;
                    var $img = $('<input type="text" id="Empresa" name="Empresa" readonly maxlength=50 value="' + empresa + '"></input>');
                    return $img;
                }
            },

            Nombre: {
                title: "Nombre",
                width: '20%',
                create: true,
                maxlength: 60
            },

            Email: {
                title: 'Email',
                width: '20%',
                create: true,
                listClass: "ocultarXS"
            },

            Password: {
                title: 'Password',
                width: '20%',
                visibility:'hidden',
                create: true,
                edit: true,
                input: function () {
                    var $img = $('<input type="text" id="password" name="password" readonly maxlength=30></label>'+
                        '<input type="button" id="btnGeneraPWD" value="Generar"/>');
                    return $img;
                }
            },

            Horario: {
                title: 'Horario',
                width: '15%',
                maxlength: 150,
                listClass: "ocultarXS"
            },

            Estado: {
                title: 'Estado',
                width: '15%',
                create: false,
                type: 'checkbox',
                values: { 'false': 'Inactivo', 'true': 'Activo' },
                defaultValue: 'true'
            }
        }
    });
}

function _generaPWD() {
    var patron = "nav4jdgt7cw3e9fm6ho8bip5ukx0y.2qlr1sz"
    var pwd = "";
    //console.log('longitud: ' +patron.length);
    for (var i = 0; i < 10; i++) {
        var pos = Math.floor((Math.random() * 100) + 1);
        //console.log('antes de calculo: ' + pos);
        if (pos >= patron.length - 2) {
            pos = pos % patron.length;
        }
        //console.log('valor es: ' + patron.substring(pos, pos + 1) + ' pos= ' + pos);
        pwd += patron.substring(pos, pos + 1);
    }
    $('#password').val(pwd);
}

function _filtro(e) {
    e.preventDefault();
    $('#UsuarioTable').jtable('load', {
        campoBuscar: $('#campoBuscar').val(),
        textoBuscar: $('#textoBuscar').val()
    });
}
