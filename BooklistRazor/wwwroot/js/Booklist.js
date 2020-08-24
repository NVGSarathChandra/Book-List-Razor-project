

var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#dataLoad').DataTable({
        "ajax": {
            "url": "/api/Book",
            "method": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "bookName", "width": "20%" },
            { "data": "authorName", "width": "20%" },
            { "data": "isbn", "width": "20%" },
            {
                "data": "bookId",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/BookList/Upsert?bookId=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white'  style='cursor:pointer; width:70px;'
                            onclick=Delete('/api/book?bookId='+${data})>
                            Delete
                        </a>
                        </div>`
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}


function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this file!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "Delete",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }   

            });
        }
    });
}