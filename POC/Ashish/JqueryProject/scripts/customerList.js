$(document).ready(function () {
    
    getCustomersAjax().then(function (data) {
        $('#CustData').dataTable();
    });

    
    //function deleteCustomer(customerId) {
    //    deleteCustomerAjax().then(function (data) { });
    //}
})

function loadEdit(data) {
    //$(document).load("Customer.html?customerId=" + customerId);
    window.location = 'Customer.html?customerId=' + data;
}

function deleteCustomer(customerId) {
    deleteCustomerAjax(customerId).then(function (data) { });
}

