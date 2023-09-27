// Zoom
// document.body.style.zoom = "90%";

// Data Table Code
$(function () {
    $("#example1").DataTable({
        "responsive": true, "lengthChange": false, "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
    $('#example2').DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "bDestroy": true,
    });
});

// Crud Modal Code
function closeModal() {

    var myModal = document.getElementById('updateModal');

    // Add the "show" class to the Modal
    myModal.classList.remove('show');

    // Get the Modal Dialog element
    var modalDialog = myModal.querySelector('.modal-dialog');

    // Add classes and animation effects to the Modal Dialog
    modalDialog.classList.remove('show');
    modalDialog.classList.remove('modal-dialog-centered');
    modalDialog.classList.remove('animate__animated');
    modalDialog.classList.remove('animate__bounceIn');

    myModal.style.display = 'none';
}

function showModal(modal) {

    // Get the Bootstrap Modal element
    var myModal = document.getElementById(modal);

    // Add the "show" class to the Modal
    myModal.classList.add('show');

    // Set the display style to "block"
    myModal.style.display = 'block';

    // Set the background color and other attributes
    myModal.style.backgroundColor = 'rgba(0, 0, 0, 0.5)';
    myModal.setAttribute('aria-modal', 'true');
    myModal.setAttribute('role', 'dialog');

    // Get the Modal Dialog element
    var modalDialog = myModal.querySelector('.modal-dialog');

    // Add classes and animation effects to the Modal Dialog
    modalDialog.classList.add('show');
    modalDialog.classList.add('modal-dialog-centered');
    modalDialog.classList.add('animate__animated');
    modalDialog.classList.add('animate__bounceIn');
}


// Confirm Dialog For Deleting
var object = { status: false, ele: null };

function confirmDeleteRecord(e) {

    if (object.status) {
        return true;
    };

    Swal.fire({
        title: 'Delete Record',
        text: "Do You Want To Delete This Record?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!',
        closeOnConfirm: true

    }).then((result) => {
        if (result.isConfirmed) {

            object.status = true;
            object.ele = e;

            object.ele.click();
        }
    })

    return false;
}
