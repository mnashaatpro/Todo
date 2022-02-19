function submitTodo() {
    if (!validateInput()) {
        return false;
    }
    $.ajax({
        type: 'POST',
        url: '/api/todo/UpSert',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: { //Passing data
            Title: $("#Title").val(), //Reading text box values using Jquery
            DueDate: $("#DueDate").val(),
            Id: $("#Id").val()
        }
        ,
        success: function (data,text) {

            //setTimeout(function () {
                Swal.fire(
                    'Good job!',
                    'Completed Task Successfully!',
                    'success'
                )

                $('#tblTable').DataTable().ajax.reload();
                $('#createOrEditModal').modal("hide");
                //loadDataTable();
            //}, 100);

        },
        error: function (request, status, error) {
            alert(request.responseText);
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!'
            });
            console.log('Failed ');
        }
    })
}

$('#tblTable').on('click', '.js-delete', function () {
    var btn = $(this);
    //alert(btn.data('id'))

    bootbox.confirm({
        message: "Are you sure that you need to delete this Todo ?",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-danger'
            },
            cancel: {
                label: 'No',
                className: 'btn-outline-secondary'
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: "/api/todo/Delete/" + btn.data('id'),
                    type: 'DELETE',
                    success: function () {


                        if (($('#' + tableName + ' tr').length - 1) <= 1) {
                            dataTable.ajax.reload();
                        } else {

                            // Reduce count items  --1 
                            const txtInfo = $('#tblTable_info').text()
                            const pieces = txtInfo.split(' ')
                            const countNum = pieces[pieces.length - 2];
                            $('#tblTable_info').text(txtInfo.replace(countNum, countNum - 1));



                            var container = btn.parents('tr');
                            container.addClass('animate__animated animate__zoomOut');

                            setTimeout(function () {
                                container.remove();
                            }, 750);
                        }


                        Swal.fire(
                            '',
                            ' Task deleted!',
                            'success'
                        )

                        toastr.success('Item deleted');
                    },
                    error: function (a, b, c) {
                        toastr.error('Something went wrong!');
                    }
                });
            }
        }
    });
});

$('#tblTable').on('click', '.js-mark-done', function () {
    var btn = $(this);

    bootbox.confirm({
        message: "Are you sure you Todo Task ?",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-outline-secondary'
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: "/api/todo/MarkDone/" + btn.data('id'),
                    type: 'Post',
                    success: function () {

                        var container = btn.parents('tr');
                        container.addClass('animate__animated animate__backInDown');

                        $(container).css('background-color', '#2ae82aa3');
                        $(container).find('.btn-warning').hide("slow");
                        btn.hide("slow");

                        Swal.fire(
                            'Good job!',
                            'Completed Task Successfully!',
                            'success'
                        );


                        toastr.success('Completed Task Successfully ');
                    },
                    error: function (a, b, c) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Something went wrong!'
                        });
                       // toastr.error('Something went wrong!');
                    }
                });
            }
        }
    });
});



$('#tblTable').on('click', '.js-modal-popup', function (e) {
    modalPopup(this);
});

$(".js-modal-popup").on('click', function (e) {
    modalPopup(this);
});
function modalPopup(that) {
    var url = $(that).data('url');
    var dateVal = $(that).data('date');

    $.get(url).done(function (data) {
        $('#createOrEditModal').find(".modal-dialog").html(data);
        $('#createOrEditModal').modal("show");
        $('.datepicker').datepicker({
            format: "yyyy/mm/dd",
            //startDate: new Date(),
            //endDate: "01-01-2020",
            todayBtn: "linked",
            autoclose: true,
            todayHighlight: true
            //container: '.modal-dialog'
        });


        if ($('#Id').val() == 0) {
            $('.datepicker').datepicker('setDate', 'now');
        } else {
            $('.datepicker').datepicker('setDate', dateVal);
        }
    });
}



function validateInput() {
    var title = document.getElementById("Title").value;
    var dt = document.getElementById("DueDate").value;

    if (dt.toString().length == 0) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please enter pickup DueDate!'
        });
        return false;
    }
    else if (title.toString().length == 0) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please enter pickup title!'
        });
        return false;
    }

    else {
        return true;
    }
}
