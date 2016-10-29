jQuery(document).ready(function() {

    $.backstretch("http://localhost:2121/Assets/img/backgrounds/1.jpg");

    $('input[type="text"], input[type="password"], textarea').blur(function () {
        if ($(this).val() != "") {
            $(this).removeClass('input-error');
        }
    });
});

function enviar() {
    $('#divRegister').find('input[type="text"], input[type="password"], textarea').each(function () {
        if ($(this).val() == "") {
            $(this).addClass('input-error');
        }
        else {
            $(this).removeClass('input-error');
        }
    });

    if (document.getElementById("divRegister").querySelectorAll(".input-error").length == 0) {
        var data = "{";
        data += "'userName':'" + document.querySelector("#txtUsername1").value + "',";
        data += "'password':'" + document.querySelector("#txtPassword1").value + "',";
        data += "'nome':'" + document.querySelector("#txtNome").value + "',";
        data += "'sobrenome':'" + document.querySelector("#txtSobrenome").value + "',";
        data += "'email':'" + document.querySelector("#txtEmail").value + "'";
        data += "}";
        $.ajax({
            type: "POST",
            data:data,
            url: "Login.aspx/CriarUsuario", //COLOCAR URL RUBY
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (output) {
                var msg = "";
                msg = output.d == 1 ? "Usuário criado com sucesso" : output.d == 2 ? "Este nome de usuário já está em uso, por favor, escolha outro" : "Email já cadastrado";
                modalBS(msg, output.d == 1 ? "Sucesso" : "Erro", output.d == 1 ? 'green' : 'red', '#fff', 'OK~voltaLogin(' + output.d + ')');
            }
        });
    }
}

function validaEmail(elem) {
    var emailAddress = elem.value;
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    if (emailAddress != "" && !(pattern.test(emailAddress))) {
        $(elem).addClass('input-error');
        document.getElementById("alertRegister").innerHTML = "<i class='fa fa-times-circle' aria-hidden='true'></i>&nbsp;Email inválido";
        $('#alertRegister').show();
        elem.focus();
    } else {
        $(elem).removeClass('input-error');
        $('#alertRegister').hide();
    }
};

function validaSenha() {
    var senha = document.querySelector("#txtPassword1");
    var confirmSenha = document.querySelector("#txtConfPass");
    if ((senha.value != "" && confirmSenha.value != "") && (senha.value != confirmSenha.value)) {
        $(senha).addClass('input-error');
        confirmSenha.value = "";
        document.getElementById("alertRegister").innerHTML = "<i class='fa fa-times-circle' aria-hidden='true'></i> &nbsp;Senha e confirmação da senha estão diferentes!";
        $('#alertRegister').show();
        senha.focus();
    } else {
        $(senha).removeClass('input-error');
        $('#alertRegister').hide();
    }
};

function modalBS(cText, cHeaderText, cHeaderColor, cFontHeaderColor, cButtons) {
    closeModalBS();
    var modalHeader = document.querySelector('.modal-header');
    var modalTitle = document.querySelector('.modal-title');
    var modalBody = document.querySelector('.modal-body');
    var modalFooter = document.querySelector('.modal-footer');

    modalTitle.innerHTML = cHeaderText;
    modalBody.innerHTML = "<p>" + cText + "</p>"


    if (cHeaderColor !== undefined) {
        $(modalHeader).css("background-color", cHeaderColor);
    }

    if (cFontHeaderColor !== undefined) {
        $(modalTitle).css("color", cFontHeaderColor);
    }

    if(cButtons !== undefined){
        cButtons = cButtons.split('@');
        for(var i = 0;i<cButtons.length;i++){
            var btn = cButtons[i].split('~');
            modalFooter.innerHTML += "<button type='button' class='btn btn-default' onclick=" + btn[1] + ">" + btn[0] + "</button>";
        }
    }

    $('#alertMsgs').modal('show');
}

function closeModalBS() {
    var modalHeader = document.querySelector('.modal-header');
    var modalTitle = document.querySelector('.modal-title');
    var modalBody = document.querySelector('.modal-body');
    var modalFooter = document.querySelector('.modal-footer');

    modalTitle.innerHTML = "";
    modalBody.innerHTML = "";
    modalFooter.innerHTML = "";
    $(modalHeader).css("background-color", "#fff");
    $(modalHeader).css("color", "#000");

    $('#alertMsgs').modal('hide');
}

function voltaLogin(act) {
    if (act == 1) {
        changeContent('divLogin');
    }
    closeModalBS();
}

function fazerLogin() {
    $('#divLogin').find('input[type="text"], input[type="password"], textarea').each(function () {
        if ($(this).val() == "") {
            $(this).addClass('input-error');
        }
        else {
            $(this).removeClass('input-error');
        }
    });

    if (document.getElementById("divLogin").querySelectorAll(".input-error").length == 0) {
        var data = "{";
        data += "'userName':'" + document.querySelector("#txtUsername").value + "',";
        data += "'password':'" + document.querySelector("#txtPassword").value + "'";
        data += "}";
        $.ajax({
            type: "POST",
            data: data,
            url: "Login.aspx/LoginUsuario", //COLOCAR URL RUBY
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (output) {
                var msg = "";
                if (output.d == 0) {
                    msg = "Usuário e/ou senha inválidos!";
                    modalBS(msg, "Erro", "red", "#fff", 'OK~closeModalBS()');
                } else {
                    window.location = "Index.aspx";
                }
                //msg = output.d == 0 ? "Usuário criado com sucesso" : output.d == 2 ? "Este nome de usuário já está em uso, por favor, escolha outro" : "Email já cadastrado";
            }
        });
    }
}