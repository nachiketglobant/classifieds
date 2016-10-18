
$(function () {
    var operation = "A"; //"A"=Adding; "E"=Editing

    var selected_index = -1; 

    var tbClients = localStorage.getItem("tbClients");

    tbClients = JSON.parse(tbClients); 
      
    if (tbClients == null) 
        tbClients = [];
      
    
    function Add() {

        var client = {
            ID: $("#txtID").val(),
            Name: $("#txtName").val(),
            Phone: $("#txtPhone").val(),
            Email: $("#txtEmail").val(),
            DOB: $("#datepicker").val(),
            Gender: $("input:radio[name='gender']:checked").val()
        };

        tbClients.push(client);
        localStorage.setItem("tbClients", JSON.stringify(tbClients));
        alert("Data Saved Successfully.");
        return true;

    }

    function Edit() {
        tbClients[selected_index] = {
            ID: $("#txtID").val(),
            Name: $("#txtName").val(),
            Phone: $("#txtPhone").val(),
            Email: $("#txtEmail").val(),
            DOB: $("#datepicker").val(),
            Gender: $("input:radio[name='gender']:checked").val()
        };
        localStorage.setItem("tbClients", JSON.stringify(tbClients));
        alert("The data was edited.")
        operation = "A"; 
        return true;
    }

    function Delete() {
        tbClients.splice(selected_index, 1);
        localStorage.setItem("tbClients", JSON.stringify(tbClients));
        alert("Record deleted.");
    }

    function List() {
        $("#tblList").html("");
        $("#tblList").html(
			"<thead>" +
			"	<tr>" +
			"	<th></th>" +
			"	<th>ID</th>" +
			"	<th>Name</th>" +
			"	<th>Phone</th>" +
			"	<th>Email</th>" +
            "   <th>DOB</th>" +
            "   <th>Gender</th>" +
			"	</tr>" +
			"</thead>" +
			"<tbody>" +
			"</tbody>"
			);
        for (var i in tbClients) {
            var cli = tbClients[i];
            $("#tblList tbody").append("<tr>" +
									 	 "	<td><img src='edit.png' alt='Edit" + i + "' class='btnEdit'/><img src='delete.png' alt='Delete" + i + "' class='btnDelete'/></td>" +
										 "	<td>" + cli.ID + "</td>" +
										 "	<td>" + cli.Name + "</td>" +
										 "	<td>" + cli.Phone + "</td>" +
										 "	<td>" + cli.Email + "</td>" +
                                         "	<td>" + cli.DOB + "</td>" +
                                         "	<td>" + cli.Gender + "</td>" +
		  								 "</tr>");
        }
    }

    $("#frmRegister").bind("submit", function () {
        if (operation == "A")
            return Add();
        else
            return Edit();
    });

    List();
    
    $(".btnEdit").bind("click", function () {

        operation = "E";
        selected_index = parseInt($(this).attr("alt").replace("Edit", ""));

        var cli = tbClients[selected_index];
        $("#txtID").val(cli.ID);
        $("#txtName").val(cli.Name);
        $("#txtPhone").val(cli.Phone);
        $("#txtEmail").val(cli.Email);
        $("#datepicker").val(cli.DOB);
        $("input:radio[name='gender']:checked").val(cli.Gender);
        $("#txtID").attr("readonly", "readonly");
        $("#txtName").focus();
    });

    $(".btnDelete").bind("click", function () {
        selected_index = parseInt($(this).attr("alt").replace("Delete", ""));
        Delete();
        List();
    });

    $("#btnSearch").bind("click", function () {
        //alert("I m in search");
        var searchtext = $("#txtSearch").val();
        List();
    });
});