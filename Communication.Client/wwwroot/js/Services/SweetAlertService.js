//function sweetSubmit(formId) {
//    $(formId).submit()
//}
function validationAlert(message) {
    const alert = Swal.mixin({
        toast: true,
        position: "bottom",
        showConfirmButton: false,
        timer: 4000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;
        }
    });
    alert.fire({
        icon: "warning",
        title: message
    });
}