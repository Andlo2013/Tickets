$(document).on("ready", _iniJTPlan);

//Pone en español jtable
$.extend(true, $.hik.jtable.prototype.options.messages, spanishMessages);

//Inicializa este script
function _iniJTPlan() {
    _planTable();
}

//Crea la tabla principal
function _planTable() {
    $('#PlanTable').jtable({
        messages: spanishMessages,
        title: 'Planes de asistencia',
        sorting: false,
        paging: true, //Enable paging
        pageSize: 15,
        actions: {
            listAction: '/supportSI/si_Plan/listarPlan',
            createAction: '/supportSI/si_Plan/Create',
            updateAction: '/supportSI/si_Plan/Edit'
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

            Descripcion: {
                title: "Plan",
                width: '50%',
                maxlength: 50
            },

            Minutos: {
                title: 'Minutos',
                width: '30%',
            },

            EstReg: {
                title: 'Estado',
                width: '20%',
                type: 'checkbox',
                values: { 'false': 'Inactivo', 'true': 'Activo' },
                defaultValue: 'true',
                create: false,
            }

        }

    }).jtable('load');
}