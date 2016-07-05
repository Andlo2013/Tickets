$(document).on("ready", _iniJTEmpresa);

//Pone en español jtable
$.extend(true, $.hik.jtable.prototype.options.messages, spanishMessages);

//Inicializa este script
function _iniJTEmpresa() {
    _empresaTable();
    //Filtros
    $('#buscar').on('click', _filtro);
    $('#buscar').trigger("click");
}

//Crea la tabla principal
function _empresaTable() {
    $('#EmpresaTable').jtable({
        messages: spanishMessages,
        title: 'Listado de Empresas',
        sorting: false,
        paging: true, //Enable paging
        pageSize: 15,
        actions: {
            listAction: '/supportSI/si_Empresa/listarEmpresa',
            createAction: '/supportSI/si_Empresa/Create',
            updateAction: '/supportSI/si_Empresa/Edit'
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

            EmpRuc: {
                title: "RUC",
                width: '15%',
                create: true,
                maxlength: 13
            },

            EmpNom: {
                title: 'Empresa',
                width: '30%',
                create: true
            },

            Direccion: {
                title: 'Dirección',
                width: '30%',
                maxlength: 150,
                type: 'textarea',
                listClass: "ocultarXS"
            },

            Telefono: {
                title: 'Teléfono',
                width: '15%',
            },

            EstReg: {
                title: 'Estado',
                width: '10%',
                create: false,
                type: 'checkbox',
                values: { 'false': 'Inactivo', 'true': 'Activo' },
                defaultValue: 'true'
            }

        }
    });
}

function _filtro(e) {
    e.preventDefault();
    $('#EmpresaTable').jtable('load', {
        campoBuscar: $('#campoBuscar').val(),
        textoBuscar: $('#textoBuscar').val()
    });
}
