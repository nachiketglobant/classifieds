function UpdaetCustomer() {
    var custId = GetParameterValues('id');
    var customerInfo = new Object();
    customerInfo.Name = $('#cName').val(),
    customerInfo.Email = $('#cEmail').val(),
    customerInfo.DOB = $('#cDob').val(),
    customerInfo.Type = $('#Type').val(),
    customerInfo.Gender = $('input[name=Gender]:checked').val()

    var hobbies = [];
    $.each($("input[name='hobbies']:checked"), function () {
        hobbies.push($(this).val());
    });
    customerInfo.Hobbies = hobbies.join(", ");
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
        alert('Update success');
    }).error(function (err) {
        alert("Error! " + err.status);
    });
}

//FInd customer data on customer ID  
function getCustomerbyID(custID) {
    $.ajax({
        url: "http://localhost:63868/api/contacts/" + custID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#cName').val(result.Name);
            $('#cEmail').val(result.Email);
            $('#cDob').val(result.DOB);
            if (result.Gender == 'M') {
                $('input:radio[name="Gender"]').filter('[value="M"]').attr('checked', true);
            } else {
                $('input:radio[name="Gender"]').filter('[value="F"]').attr('checked', true);
            }
            $("#Type option[value='" + result.Type + "']").attr("selected", "selected");

            arrhobbies = result.Hobbies.split(',');
            $.each(arrhobbies, function (index, value) {
                $('input[name="hobbies"][value="' + $.trim(value.toString()) + '"]').prop("checked", true);
            });

            $('#btnEdit').show();
            $('#btnSave').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}