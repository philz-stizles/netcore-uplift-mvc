var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    console.log(url);

    if (url.includes('pending')) {
        loadDataTable("/Admin/Order/GetAllPending");
    } else if (url.includes('approved')) {
        loadDataTable("/Admin/Order/GetAllApproved");
    } else if (url.includes('rejected')) {
        loadDataTable("/Admin/Order/GetAllRejected");
    } else {
        loadDataTable("/Admin/Order/GetAll");
    }
});

function loadDataTable(url) {
    dataTable = $('#orderTable').DataTable({
        "ajax": {
            "url": url,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "customerDetail.name", "width": "25%" },
            { "data": "customerDetail.phone", "width": "15%" },
            { "data": "totalItems", "width": "15%"},
            { "data": "totalPrice", "width": "15%" },
            { "data": "orderStatus", "width": "10%" },
            {"data": "id", "render": function(id) {
                return `<div class="text-center">
                    <a class="btn btn-success btn-sm text-white" style="cursor: pointer; width: 100px;" href="/Admin/Order/Details/${id}"><i class="fa fa-edit"></i> &nbsp; Details</a>
                </div>`
            }, "width": "20%"},
        ],
        "language": {
            "emptyTable": "No records found"
        }
    });
}