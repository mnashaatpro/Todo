
function GetColumns() {
    var columns = [
            
        {
            "data": null, "sortable": false,
            render: function (data, type, row, meta) {
                return meta.row + meta.settings._iDisplayStart + 1;
            }
        },
        //{ "data": "Id", "name": "Id", "autoWidth": true },
        { "data": "Title", "name": "Title", "autoWidth": true },
        { "render": function (data, type, row) {//data
                return moment(row.DueDate).format('DD/MM/YYYY');  }
        },
        {
            "render": function (data, type, row) {
                var htmlEdit = '', htmlMarkDone = '';
                if (!row.IsDone) {
                    htmlMarkDone = `<a class="btn btn-warning mx-2 js-modal-popup" data-date="${moment(row.DueDate).format('YYYY/MM/DD')}" data-url="/Home/Edit/${row.Id}/" >
                            <i class=" fas fa-edit"></i> &nbsp; Edit
                        </a>
                             <a class='btn btn-success mx-2 js-mark-done'  data-id='${row.Id}'   >
                            <i class=" fas fa-check"></i> &nbsp; Mark Done
                        </a>
                       `;
                } 
                
               
                return `
                    <div class='text-center'>
                    <div id='groupAction' class="w-50 btn-group " role="group">

                 ${htmlMarkDone}
               
                    <a class='btn btn-danger text-white js-delete' style='cursor:pointer; width:100px;' data-id='${row.Id}'  >
                          <i class='far fa-trash-alt'></i> Delete
                      </a>
                    </div>
                     </div>`;
            }, "orderable": false
        }
    ]
    return columns;
}