window.showConfirmModal = function () {
    var modal = new bootstrap.Modal(document.getElementById('confirmModalRemove'));
    modal.show();
};

window.hideConfirmModal = function () {
    var modalEl = document.getElementById('confirmModalRemove');
    var modal = bootstrap.Modal.getInstance(modalEl);
    modal.hide();
};