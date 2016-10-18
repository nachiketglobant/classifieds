var getCustomersAjax = function () {

    var d = $.Deferred();
    $.ajax({
        url: 'http://localhost:52376',
        type: 'GET',
        dataType: "jsonp",
        jsonpCallback: 'callback',
        useDefaultXhrHeader: false,
        headers: {
            "Content-Type": "application/json"
        }
    }).done(function (data) {
        d.resolve(data);
        $.each(data, function (k, v) {
            var counter = 1;
            var custRow = "<tr><td>" + v.Name + "</td>" +
                            "<td>" + v.DateofBirth + "</td>" +
                            "<td>" + v.CustomerType + "</td>" +
                            "<td>" + v.Gender + "</td>" +
                            "<td>" + v.Hobbies + "</td>" +
                            "<td><input type=button value='Edit' id='edit" + v.CustomerId + "' onclick=loadEdit(" + v.CustomerId + "); /> <input type=button value='Delete' id='delete" + v.CustomerId + "' onclick=deleteCustomer(" + v.CustomerId + ") /></td></tr>"
            $('#CustData').append(custRow);
            counter++;
        });
        
    })
        .fail(function (data) {
            alert('failed' + data);
            d.reject();
        });
    return d.promise();
}

var deleteCustomerAjax = function (customerId) {

    var d = $.Deferred();
    $.ajax({
        url: 'http://localhost:52376/Customer/DeleteCustomer/' + customerId,
        type: 'DELETE',
        method: 'DELETE',
        data: customerId,
        dataType: "jsonp",
        jsonpCallback: 'callback',
        useDefaultXhrHeader: false,
        headers: {
            "Content-Type": "application/json"
        }
    }).done(function (data) {
        d.resolve(data);
        var counter = 1;
        $('#CustData tbody').remove();
        $.each(data, function (k, v) {
            if (counter == 1)
                $('#CustData').append("<tbody>");
            var custRow = "<tr><td>" + v.Name + "</td>" +
                            "<td>" + v.DateofBirth + "</td>" +
                            "<td>" + v.CustomerType + "</td>" +
                            "<td>" + v.Gender + "</td>" +
                            "<td>" + v.Hobbies + "</td>" +
                            "<td><input type=button value='Edit' id='edit" + v.CustomerId + "' onclick=loadEdit(" + v.CustomerId + "); /> <input type=button value='Delete' id='delete" + v.CustomerId + "' onclick=deleteCustomer(" + v.CustomerId + ") /></td></tr>"
            $('#CustData').append(custRow);
            counter++;
        });
        
    })
        .fail(function (data) {
            alert('failed' + data.text);
            d.reject();
        });
    return d.promise();
}

