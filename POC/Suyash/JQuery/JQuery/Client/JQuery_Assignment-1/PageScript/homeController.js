    $(document).ready(function () {
        //Populate Customer
        LoadContacts();
       
    });

    function LoadContacts() {
        $('#update_panel').html('Loading Data...');       
        var $dfd = $.Deferred();
            $.ajax({
                url: 'http://localhost:63868/api/contacts',
                type: 'GET',
                dataType: 'json',               
                success: function (d) {
                    if (d.length > 0) {
                        GenerateTableStructure(d);
                    }
                    else {
                        var $noData = $('<div/>').html('No Data Found!');
                        $('#update_panel').html($noData);
                    }
                    $dfd.resolve(d);
                },
                error: function () {
                    $dfd.reject();
                }
            });
            return $dfd.promise();
        }

    function GenerateTableStructure(d)
    {
        var $data = $('<table id="keywords" class="display"></table>').addClass(' display table table-responsive table-striped');
        var header = "<thead><tr><th id=sl>#</th><th id=nm>Name</th><th>Designation</th><th>Email</th><th>Address</span></th><th>Gneder</th><th>Hobby</th><th>Action</th></tr></thead>";
        $data.append(header);
        $.each(d, function (i, row) {
            var $row = $('<tr/>');
            $row.append($('<td/>').html(i+1));
            $row.append($('<td/>').html(row.name));
            $row.append($('<td/>').html(row.designation));
            $row.append($('<td/>').html(row.email));
            $row.append($('<td/>').html(row.address));
            $row.append($('<td/>').html(row.gender));
            $row.append($('<td/>').html(row.hobies));           
            //$row.append($('<td/>').html('<a href="#" onclick="return getbyID(' + item.EmployeeID + ')">Edit</a> | <input type="button" value="Delete"  onclick="javascript:Delete(' + row._id + ')" />'));
            $row.append($('<td/>').html('<a href="AddNewCustomer.html?custId=' + row._id + ' ">Edit</a> | <a href="#" onclick="javascript:Delete(\'' + row._id + '\')" >Delete</a>'));
            $data.append($row);
        });
        $('#update_panel').html($data);
        $('#keywords').DataTable({
            columnDefs: [{ targets: 7, orderable: false }, { "searchable": false, "targets": 7 }],
            "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]]
        });
    }
   
    //function for deleting employee's record  
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
                    LoadContacts();
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