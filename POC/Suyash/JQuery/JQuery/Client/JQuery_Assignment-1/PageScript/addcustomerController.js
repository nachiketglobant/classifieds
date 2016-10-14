$(document).ready(function () {
    $("#birthdate").datepicker({
        dateFormat: "mm/dd/yy"
    });
    
    clearTextBox();
    var custId = GetParameterValues('custId');
    if (custId === undefined || custId === null) {
        clearTextBox();
    } else { getbyID(custId); }
});

//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#name').val().trim() == "") {
        $('#name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#name').css('border-color', 'lightgrey');
    }
    if ($('#designation').val().trim() == "") {
        $('#designation').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#designation').css('border-color', 'lightgrey');
    }
    if ($('#email').val().trim() == "") {
        $('#email').css('border-color', 'Red');
        isValid = false;
    } else if (!ValidateEmail($("#email").val())) {
        $('#email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#email').css('border-color', 'lightgrey');
    }
    if ($('#address').val().trim() == "") {
        $('#address').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#address').css('border-color', 'lightgrey');
    }

    if ($("#gender:checked").length == 0) {       
        $('#disgender').slideDown().html('<span id="error">Please select gender</span>');
        $('#disgender').show();
        isValid = false;
    }

    if ($('input[type=checkbox]:checked').length == 0) {
        $('#dishobbies').slideDown().html('<span id="error">Please select atleast one hobby</span>');
        $('#dishobbies').show();
        isValid = false;
    }
    return isValid;
}

//function for adding new record into the db
function AddNewContacts() {
    //debugger;
    var res = validate();
    if (res == false) {
        return false;
    }
    var customerInfo = new Object();
    customerInfo.name = $('#name').val(),
    customerInfo.designation = $('#designation').val(),
    customerInfo.email = $('#email').val(),
    customerInfo.address = $('#address').val(),
    customerInfo.birthdate = $('#birthdate').val(),
    customerInfo.gender = $('input[name=gender]:checked').val() //$('#gender').val(),    
    customerInfo.isactive = 'true';
    
    var favorite = [];
    $.each($("input[name='hobbies']:checked"), function () {
        favorite.push($(this).val());
    });   
    customerInfo.hobies = favorite.join(", ");

    var d = new Date();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    customerInfo.postedon = (('' + month).length < 2 ? '0' : '') + month + '/' + (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

    var $dfd = $.Deferred();
    $.ajax({
        url: "http://localhost:63868/api/contacts",
        type: "POST",
        data: JSON.stringify(customerInfo),
        datatype: "json",
        headers: {
            "Content-Type": "application/json"
        }
    }).done(function (resp) {
        alert('Customer added successfully');
        clearTextBox();
        $dfd.resolve(resp);
    }).error(function (err) {
        alert('Error!' + err.status);
        $dfd.reject();
    });
    return $dfd.promise();
}

//Function for getting the Data Based upon customer ID  
function getbyID(custID) {   
    $.ajax({
        url: "http://localhost:63868/api/contacts/" + custID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
           
            $('#name').val(result.name);
            $('#designation').val(result.designation);
            $('#email').val(result.email);
            $('#address').val(result.address);
            $('#birthdate').val(result.birthdate);
            if (result.gender == 'Male') {
                $('input:radio[name="gender"]').filter('[value="Male"]').attr('checked', true);
            } else {
                $('input:radio[name="gender"]').filter('[value="Female"]').attr('checked', true);
            }
            
            arrhobbies = result.hobies.split(',');
            $.each(arrhobbies, function (index, value) {
                $('input[name="hobbies"][value="' + $.trim(value.toString()) + '"]').prop("checked", true);
            });

            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating existing selected record
function UpdaetContacts() {
    debugger;
    var res = validate();
    if (res == false) {
        return false;
    }
    var custId = GetParameterValues('custId');
    var customerInfo = new Object();
    customerInfo.name = $('#name').val(),
    customerInfo.designation = $('#designation').val(),
    customerInfo.email = $('#email').val(),
    customerInfo.address = $('#address').val(),
    customerInfo.birthdate = $('#birthdate').val(),
    customerInfo.gender = $('input[name=gender]:checked').val() //$('#gender').val(),    
    customerInfo.isactive = 'true';

    var favorite = [];
    $.each($("input[name='hobbies']:checked"), function () {
        favorite.push($(this).val());
    });
    customerInfo.hobies = favorite.join(", ");

    $.ajax({
        url: "http://localhost:63868/api/contacts/" + custId,
        type: "PUT",
        data: JSON.stringify(customerInfo),
        datatype: "json",
        contenttype: "application/json;utf-8",
        headers: {
        "Content-Type": "application/json"
    }
    }).done(function (resp) {
        alert('Customer updated successfully');
        clearTextBox();
        IsUpdatable = false;
    }).error(function (err) {
        alert("Error! " + err.status);
    });
}
