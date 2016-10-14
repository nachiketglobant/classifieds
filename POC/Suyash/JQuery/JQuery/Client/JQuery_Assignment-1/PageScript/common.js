//Function for clearing the fields of 'add new contact' form
function clearTextBox() {
    $('#name').val("");
    $('#designation').val("");
    $('#email').val("");
    $('#address').val("");
    $('#birthdate').val("");
    $('#disgender').hide();
    $('#dishobbies').hide();
    $('#name').css('border-color', 'lightgrey');
    $('#designation').css('border-color', 'lightgrey');
    $('#email').css('border-color', 'lightgrey');
    $('#address').css('border-color', 'lightgrey');
    $("input:checkbox").prop('checked', false);
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}

//function for validating email field
function ValidateEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};


function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}

//function for search customer
function searchTable(inputVal)
{
    var table = $('#keywords');
    table.find('tr').each(function(index, row)
    {
        var allCells = $(row).find('td');
        if(allCells.length > 0)
        {
            var found = false;
            allCells.each(function(index, td)
            {
                var regExp = new RegExp(inputVal, 'i');
                if(regExp.test($(td).text()))
                {
                    found = true;
                    return false;
                }
            });
            if(found == true)$(row).show();else $(row).hide();
        }
    });
}

