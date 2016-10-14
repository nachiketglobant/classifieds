//function for deleting customers 
function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        var $dfd = $.Deferred();
        $.ajax({
            url: "http://localhost:63868/api/contacts/" + ID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                LoadCustomers();
                $dfd.resolve(result);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
                $dfd.reject();
            }
        });
        return $dfd.promise();
    }
}