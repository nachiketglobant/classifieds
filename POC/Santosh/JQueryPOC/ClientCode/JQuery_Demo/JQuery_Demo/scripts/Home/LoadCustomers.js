function LoadCustomers() {
    table = $('#mytable').DataTable();
    table.destroy();
    $.ajax({
        url: 'http://localhost:63868/api/contacts/',
        method: 'get',
        dataType: 'json',
        success: function (apidata) {
            $('#mytable').dataTable({
                "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
                "jQueryUI": true,
                data: apidata,
                columns: [
                    { 'data': 'Name' },
                    { 'data': 'Email' },
                    { 'data': 'Gender' },
                    {
                        'data': 'DOB'
                    },
                    { 'data': 'Type' },
                    { 'data': 'Hobbies' },
                    {
                        'data': '_id',
                        'searchable': false,
                        'sortable':false,
                        'render': function (_id) {
                            return '<a href=Registration.html?id=' + _id + '>Edit</a> / <a href="#" onclick="javascript:Delete(\'' + _id + '\')" >Delete</a>'
                        }
                    }
                ]
            });
        }
    });
}