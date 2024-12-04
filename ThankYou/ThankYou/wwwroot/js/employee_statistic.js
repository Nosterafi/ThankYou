(function () {
    var updateButton =
        document.getElementById('qr');
    var cancelButton =
        document.getElementById('cancel');
    var favDialog =
        document.getElementById('favDialog');

    // Update button opens a modal dialog
    updateButton.addEventListener('click', function () {
        favDialog.showModal();
    });

    // Form cancel button closes the dialog box
    cancelButton.addEventListener('click', function () {
        favDialog.close();
    });
})();

(function () {
    var updateButton =
        document.getElementById('stats');
    var cancelButton =
        document.getElementById('cancelstats');
    var statsDialog =
        document.getElementById('statsDialog');

    // Update button opens a modal dialog
    updateButton.addEventListener('click', function () {
        statsDialog.showModal();
    });

    // Form cancel button closes the dialog box
    cancelButton.addEventListener('click', function () {
        statsDialog.close();
    });
})();


