function _AjaxGET(url, completeFunction) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        type: "GET",
        url: url,
        dataType: "JSON",
        data: { __RequestVerificationToken: token},
        //data: "{}",
        success: function (response) {
            completeFunction(response);   
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log("ERROR EN PETICIÓN AJAX");
            return null;
        }
    });
}

function _getUrlParameter(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}