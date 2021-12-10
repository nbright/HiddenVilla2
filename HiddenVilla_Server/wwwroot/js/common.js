window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, 'Operation Successful');
    }
    if (type === "error") {
        toastr.error(message, 'Operation Failed!')
    }
}
window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Good job!',
            message,
            'success'
        )
    }
    if (type === "error") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: message,
            footer: '<a href>Why do I have this issue?</a>'
        })
    }
}
function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');

}
function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}