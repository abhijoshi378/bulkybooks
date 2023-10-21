var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblCompanies').DataTable({
        "ajax": { url: '/admin/company/getall'},
        "columns": [
            { data: 'name', "width": "10%" },
            { data: 'streetAddress', "width": "20%" },
            { data: 'city', "width": "10%" },
            { data: 'state', "width": "10%" },
            { data: 'postalCode', "width": "15%" },
            { data: 'phoneNumber', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class='btn-group' role='group'>
                        <a href='/admin/company/upsert?id=${data}' asp-route-id='@company.Id' class='btn btn-primary'><i class='bi bi-pencil-square'></i> Edit</a>
                        <a onClick="Delete('/admin/company/delete?id=${data}')" asp-route-id='@company.Id' class='btn btn-danger'><i class='bi bi-trash'></i> Delete</a>
                    </div>`;
                },
                "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            });
        }
    })
}