// datepicker code
$(function () {
    $("#cDob").datepicker();
});

function clearjQueryCache() {
    for (var x in jQuery.cache) {
        delete jQuery.cache[x];
    }
}

$(document).ready(function () {
    clearjQueryCache();
    $('#btnEdit').hide();
    $('#btnSave').show();

    var custId = GetParameterValues('id');
    if (custId === undefined || custId === null) {
    } else { getCustomerbyID(custId); }

    $("#NewCustomer").bind("submit", function () {
        if (custId === undefined || custId === null) {
            AddNewCustomer();
        } else {
            UpdaetCustomer();
        }
    });
});

function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}