// add new contact
function AddNewCustomer() {
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
        url: "http://localhost:63868/api/contacts/",
        type: "POST",
        data: JSON.stringify(customerInfo),
        datatype: "json",
        headers: {
            "Content-Type": "application/json"
        }
    }).done(function (resp) {
        alert('Add success');

    }).error(function (err) {
        alert('Error!' + err.status);
    });
}