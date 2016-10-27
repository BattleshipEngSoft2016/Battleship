function modalBS(cText, cHeaderText, cHeaderColor, cFontHeaderColor, cButtons, isSm) {
    var modalHeader = document.querySelector('.modal-header');
    var modalTitle = document.querySelector('.modal-title');
    var modalBody = document.querySelector('.modal-body');
    var modalFooter = document.querySelector('.modal-footer');

    if (isSm)
        $('.modal-dialog').addClass('modal-sm');

    modalTitle.innerHTML = cHeaderText;
    modalBody.innerHTML = "<p>" + cText + "</p>"


    if (cHeaderColor !== undefined) {
        $(modalHeader).css("background-color", cHeaderColor);
    }

    if (cFontHeaderColor !== undefined) {
        $(modalTitle).css("color", cFontHeaderColor);
    }

    modalFooter.innerHTML = "";
    if (cButtons !== undefined) {
        cButtons = cButtons.split('@');
        for (var i = 0; i < cButtons.length; i++) {
            var btn = cButtons[i].split('~');
            modalFooter.innerHTML += "<button type='button' class='btn btn-default' onclick=" + btn[1] + ">" + btn[0] + "</button>";
        }
    }

    $('#modalIndex').modal('show');
}

function closeModalBS(idAlert) {
    var modalHeader = document.querySelector('.modal-header');
    var modalTitle = document.querySelector('.modal-title');
    var modalBody = document.querySelector('.modal-body');
    var modalFooter = document.querySelector('.modal-footer');

    $('#' + idAlert).hide();
    modalTitle.innerHTML = "";
    modalBody.innerHTML = "";
    modalFooter.innerHTML = "";
    $(modalHeader).css("background-color", "#fff");
    $(modalHeader).css("color", "#000");

    $('#modalIndex').modal('hide');
    $(".modal-backdrop.fade.in").remove();
}

function removeClass(elem) {
    if (elem.value != "") {
        $(elem).removeClass('input-error');
    }
}
