/// <reference path="utility/convertToCamelCase.js" />
/*/// <reference  path="columnsgenredatatable.js" />*/


var dataTable;


document.currentScript = document.currentScript || (function () {
    var scripts = document.getElementsByTagName('script');
    return scripts[scripts.length - 1];
})();

//alert(document.currentScript);

let controllerName = "home";// camelize(window.location.pathname.split('/')[1]);


let tableName = document.currentScript.getAttribute('tableName');
//alert(controllerName);

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#' + tableName + '').DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            //"url": "/" + controllerName + "/GetDataTableData", /*'@Url.Action("GetGenres","Genres")' ,*//*"/Genres/GetGenres",*/
            "url": "home/GetDataTableData/", /*'@Url.Action("GetGenres","Genres")' ,*//*"/Genres/GetGenres",*/
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            //"visible": false,
            "searchable": false
        }],

        "columns": GetColumns(),

        'rowCallback': function (row, data, index) {
            if (data.IsDone ) {
                $(row).css('background-color', '#2ae82aa3');
            }
        }
        
    });
}


