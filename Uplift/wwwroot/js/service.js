var dataTable;

$(document).ready(function() {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#serviceTable').DataTable({
        "ajax": {
            "url": "/Admin/Service/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {"data": "name", "width": "20%"},
            {"data": "category.name", "width": "25%"},
            {"data": "price", "width": "10%"},
            {"data": "frequency.frequencyCount", "width": "10%"},
            {"data": "id", "render": function(id) {
                return `<div class="text-center">
                    <a class="btn btn-success btn-sm text-white" style="cursor: pointer; width: 100px;" href="/Admin/Service/Upsert/${id}"><i class="fa fa-edit"></i> &nbsp; Edit</a>
                    &nbsp;
                    <a class="btn btn-danger btn-sm text-white" onclick=Delete("/Admin/Service/Delete/${id}")><i class="far fa-trash-alt"></i> &nbsp; Delete</a>
                </div>`
            }, "width": "25%"},
        ],
        "language": {
            "emptyTable": "No records found"
        }
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "This action is irreversible",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete",
        closeOnConfirm: true
    }, function() {
        $.ajax({
            url: url,
            type: "DELETE",
            success: function(data) {
                console.log(data)
                if(data.status){
                    toastr.success(data.message)
                    dataTable.ajax.reload()
                }else{
                    toastr.error(data.message)
                }
            }
        })
    })
}