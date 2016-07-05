$(document).on("ready", _iniJTContrato);

//Pone en español jtable
$.extend(true, $.hik.jtable.prototype.options.messages, spanishMessages);

//Inicializa este script
function _iniJTContrato() {
    _contratoTable();
    //Filtros
    $('#buscar').on('click', _filtro);
    $('#buscar').trigger("click");
    //nuevo
    $("#ui-id-3").on('change', "#Edit-EmpresaRUC", _findEmpresaRUC);
    $("#ui-id-3").on('change', "#Edit-PlanId", _findPlan);
    //edicion
    $("#ui-id-5").on('change', "#Edit-EmpresaRUC", _findEmpresaRUC);
    $("#ui-id-5").on('change', "#Edit-PlanId", _findPlan);
}

function _findEmpresaRUC() {
    var rucEmpresa = $("#Edit-EmpresaRUC").val();
    if ($.trim(rucEmpresa)) {
        _AjaxGET("/supportSI/si_Empresa/getEmpresaRUC?RUC=" + rucEmpresa, _showEmpresa);
    }
}

function _findPlan() {
    var idPlan = $("#Edit-PlanId").val();
    if ($.trim(idPlan)) {
        _AjaxGET("/supportSI/si_Plan/getPlanId?idPlan=" + idPlan, _showPlan);
    }
}

function _showEmpresa(data) {
    $("#Empresa").val(data.Records.EmpNom);
}

function _showPlan(data) {
    $("#Minutos").val(data.Records.Minutos);
}

//Crea la tabla principal
function _contratoTable() {
    $('#ContratoTable').jtable({
        messages: spanishMessages,
        title: 'Contratos',
        sorting: false,
        paging: true, //Enable paging
        pageSize: 15,
        actions: {
            listAction: '/supportSI/si_Contrato/listarContrato',
            createAction: '/supportSI/si_Contrato/Create',
            updateAction: '/supportSI/si_Contrato/Edit'
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

            EmpresaId: {
                create: false,
                edit: false,
                title: 'EmpresaID',
                visibility: 'hidden'
            },

            EmpresaRUC: {
                title: "RUC",
                width: '15%',
                create: true,
                maxlength: 13
            },

            Empresa: {
                title: 'Empresa',
                width: '30%',
                create: true,
                input: function (data) {
                    var Empresa = typeof (data.value) != "undefined" ? data.value : "";
                    var $img = $('<input type="Text" readonly id="Empresa" name="Empresa" value="' + Empresa + '"/>');
                    return $img;
                }
            },

            PlanId: {
                title: 'Plan',
                //visibility: 'hidden',
                options: '/supportSI/si_Plan/getPlanCMB',
            },

            Minutos: {
                title: 'Minutos',
                width: '10%',
                listClass: "ocultarXS",
                input: function (data) {
                    var Minutos = typeof (data.value) != "undefined" ? data.value : "";
                    var $img = $('<input type="Text" readonly id="Minutos" name="Minutos" value="' + Minutos + '"/>');
                    return $img;
                }
            },
            
            Inicia: {
                title: 'Inicia',
                type: 'date',
                width: '10%',
                listClass: "ocultarXS"
            },

            Termina: {
                title: 'Termina',
                type: 'date',
                width: '10%',
            },

            Observaciones: {
                title: 'Observaciones',
                type: 'textarea',
                visibility: 'hidden'
            },

            Estado: {
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
    $('#ContratoTable').jtable('load', {
        campoBuscar: $('#campoBuscar').val(),
        textoBuscar: $('#textoBuscar').val()
    });
}