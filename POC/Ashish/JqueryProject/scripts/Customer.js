$(document).ready(function () {
    var customer = getQueryVariable();
    if (customer != -1) {
        loadCustomerAjax(customer).then(function (data) {
            //alert('hi');
        });
    }
    
    $('#btnSubmit').on("click", function () {
        saveCustomer();
    });
})

var loadCustomerAjax = function (customerId) {
    var d = $.Deferred();
    $.ajax({
        url: 'http://localhost:52376/Customer/GetCustomer/' + customerId,
        type: 'GET',
        data: customerId,
        dataType: "jsonp",
        jsonpCallback: 'callback',
        useDefaultXhrHeader: false,
        headers: {
            "Content-Type": "application/json"
        }
    }).done(function (data) {
        alert(data);
        d.resolve(data);
        $('#txtName').val(data.Name);
        $('#dtDOB').val(getFormattedDate(data.DateofBirth));
        $('#drpType').selected = data.CustomerType;
        $('#rd' + data.Gender).attr('checked',true);
        var parms = data.Hobbies.split(',');
        for (var i = 0; i < parms.length; i++) {
            $('#chk' + parms[i]).prop('checked', true);
        }
    })
        .fail(function (data) {
            alert('failed' + data);
            console.log(data.value);
            d.reject();
        });
    return d.promise();
}
function saveCustomer() {
    var customer = getQueryVariable();
    if (validateForm()) {
        saveCustomerAjax(customer).then(function (data) {
        });
    }
}
var saveCustomerAjax = function(customerId) {
    var d = $.Deferred();
    var customer = new Object();
    customer.CustomerId = customerId;
    customer.Name = $('#txtName').val();
    customer.DateofBirth = $('#dtDOB').val();
    customer.CustomerType = $('#drpType').val();
    customer.Gender = $("input[name='gender']:checked").val();
    var favorite = [];
    $.each($("input[name='hobbies']:checked"), function () {
        favorite.push($(this).val());
    });
    customer.Hobbies = favorite.toString();
    $.ajax({
        url: 'http://localhost:52376/Customer/SaveCustomer',
        type: 'POST',
        data: customer,
        dataType: "jsonp",
        jsonpCallback: 'callback',
        useDefaultXhrHeader: false,
        headers: {
            "Content-Type": "application/json"
        }
    }).done(function (data) {
        //alert(data);
        d.resolve(data);
        $('#postMessage').html('Customer saved successfully');
    })
        .fail(function (data) {
            alert('failed' + data);
            console.log(data.value);
            d.reject();
        });
    return d.promise();
}
// Get formatted date YYYY-MM-DD
function getFormattedDate(data) {
    var date = new Date(data);
    return date.getFullYear()
        + "-"
        + ("0" + (date.getMonth() + 1)).slice(-2)
        + "-"
        + ("0" + date.getDate()).slice(-2);
}

function getQueryVariable() {
    var customer = -1;
    var query = window.location.search.substring(1);
    var parms = query.split('&');
    for (var i = 0; i < parms.length; i++) {
        var pos = parms[i].indexOf('=');
        if (pos > 0) {
            var val = parms[i].substring(pos + 1);
            customer = val;
        }
    }
    return customer;
}

function validateForm() {
    $("p").remove();
    var valid = true;
    if ($('#txtName').val() == '') {
        valid = false;
        $('.inputElement').append('<p><strong>Please enter a name!!</strong></p>');
    }
        
    if ($('#dtDOB').val() == '') {
        valid = false;
        $('#liDate').append('<p><strong>Please select Date of birth!!</strong></p>');
    }
    if ($('#drpType').val() == null) {
        valid = false;
        $('#liType').append('<p><strong>Please select customer type!!</strong></p>');
    }
    if ($("input[name='gender']").filter(':checked').length == 0) {
        valid = false;
        $('#liGender').append('<p><strong>Please select gender!!</strong></p>');
    }
    var favorite = [];
    $.each($("input[name='hobbies']:checked"), function () {
        favorite.push($(this).val());
    });
    if (favorite.length == 0) {
        valid = false;
        $('#liHobbies').append('<p><strong>Please select hobbies!!</strong></p>');
    }
    return valid;
}

