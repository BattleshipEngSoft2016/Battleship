var count = 0;
var cElement = "";
var cValueElement = "";
var descricaoItem = "";

function validaPreenchimento() {
    var lRet = true;

    if (document.querySelector("#txtNomNivel").value == "") {
        lRet = false;
        $("#txtNomNivel").addClass('input-error');
    }

    if (document.querySelector("#txtQtdColunas").value == "" || document.querySelector("#txtQtdColunas").value == "0" || document.querySelector("#txtQtdColunas").value == "1") {
        lRet = false;
        $("#txtQtdColunas").addClass('input-error');
    }

    if (document.querySelector("#txtQtdLinhas").value == "" || document.querySelector("#txtQtdLinhas").value == "0" || document.querySelector("#txtQtdLinhas").value == "1") {
        lRet = false;
        $("#txtQtdLinhas").addClass('input-error');
    }

    if (document.querySelector("#txtQtdPortaAvioes").value == "") {
        lRet = false;
        $("#txtQtdPortaAvioes").addClass('input-error');
    }

    if (document.querySelector("#txtQtdDestroiers").value == "") {
        lRet = false;
        $("#txtQtdDestroiers").addClass('input-error');
    }

    if (document.querySelector("#txtQtdEncouracados").value == "") {
        lRet = false;
        $("#txtQtdEncouracados").addClass('input-error');
    }

    if (document.querySelector("#txtQtdCruzadores").value == "") {
        lRet = false;
        $("#txtQtdCruzadores").addClass('input-error');
    }

    if (document.querySelector("#txtQtdSubmarinos").value == "") {
        lRet = false;
        $("#txtQtdSubmarinos").addClass('input-error');
    }

    if (document.querySelector("#txtTempoJogada").value == "" || document.querySelector("#txtTempoJogada").value < 10) {
        lRet = false;
        $("#txtTempoJogada").addClass('input-error');
    }

    if (document.querySelector("#txtTempoPosicionamento").value == "" || document.querySelector("#txtTempoPosicionamento").value < 10) {
        lRet = false;
        $("#txtTempoPosicionamento").addClass('input-error');
    }

    return lRet;

}

function addNivel() {
    document.querySelector("#txtNomNivel").value = "";
    document.querySelector("#txtQtdColunas").value = "";
    document.querySelector("#txtQtdLinhas").value = "";
    document.querySelector("#txtQtdPortaAvioes").value = "";
    document.querySelector("#txtQtdDestroiers").value = "";
    document.querySelector("#txtQtdEncouracados").value = "";
    document.querySelector("#txtQtdCruzadores").value = "";
    document.querySelector("#txtQtdSubmarinos").value = "";
    document.querySelector("#txtTempoJogada").value = "";
    document.querySelector("#txtTempoPosicionamento").value = "";

    var cText = '<div id="criarNivelMod">';
    cText += document.querySelector("#criarNivel").innerHTML;
    cText += '</div>';

    modalBS(cText, 'Incluir Nivel', '#fff', '#000', 'Confirmar~confirmAddNivel()@Cancelar~closeModalBS("alertNivelExist")');
}

function confirmAddNivel() {

    if (validaPreenchimento()) {
        var data = "{";
        data += "'nome':'" + document.querySelector("#txtNomNivel").value + "',";
        data += "'qtdColunas':'" + document.querySelector("#txtQtdColunas").value + "',";
        data += "'qtdLinhas':'" + document.querySelector("#txtQtdLinhas").value + "',";
        data += "'qtdPortaAvioes':'" + document.querySelector("#txtQtdPortaAvioes").value + "',";
        data += "'qtdDestroiers':'" + document.querySelector("#txtQtdDestroiers").value + "',";
        data += "'qtdEncouracados':'" + document.querySelector("#txtQtdEncouracados").value + "',";
        data += "'qtdCruzadores':'" + document.querySelector("#txtQtdCruzadores").value + "',";
        data += "'qtdSubmarinos':'" + document.querySelector("#txtQtdSubmarinos").value + "',";
        data += "'tempoJogada':'" + document.querySelector("#txtTempoJogada").value + "',";
        data += "'tempoPosicionamento':'" + document.querySelector("#txtTempoPosicionamento").value + "'";
        data += "}";
        $.ajax({
            type: "POST",
            data: data,
            url: "http://localhost:5471/Nivel/Cadastrar",  //COLOCAR URL CONTROLLER
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (output) {
                if (output.Sucesso == false) {
                    if (output.Mensagem == "JAEXISTE")     
                        document.getElementById("alertNivelExist").innerHTML = "<i class='fa fa-times-circle' aria-hidden='true'></i>&nbsp;<strong>Oops! &nbsp; </strong>Nivel já existente com esse nome!";
                     else 
                        document.getElementById("alertNivelExist").innerHTML = "<i class='fa fa-times-circle' aria-hidden='true'></i>&nbsp;<strong>Oops! &nbsp; </strong> Ocorreu um Erro !!";
                    $('#alertNivelExist').show();

                } 
                else {
                    $('#alertNivelExist').hide();
                    var niveis = JSON.parse(output.Dados.replace(/'/g, '"')); //aqui prestar atenção no formato do objeto

                    count++;

                    var newRow = $('#niveis').dataTable().fnAddData([
                       "",
                       niveis.Id,
                       niveis.Nome,
                       niveis.QtdColunas,
                       niveis.QtdLinhas,
                       niveis.QtdNav01,
                       niveis.QtdNav02,
                       niveis.QtdNav03,
                       niveis.QtdNav04,
                       niveis.QtdNav05,
                       niveis.TempoJogada,
                       niveis.TempoPosicionamento
                    ]);

                    var oSettings = $('#niveis').dataTable().fnSettings();
                    var nTr = oSettings.aoData[newRow[0]].nTr;
                    $(nTr).addClass('linhaTabela');
                    $('td', nTr)[1].setAttribute('class', 'idNivel');
                    $('td', nTr)[2].setAttribute('class', 'nome');
                    $('td', nTr)[3].setAttribute('class', 'qtdColunas');
                    $('td', nTr)[4].setAttribute('class', 'qtdLinhas');
                    $('td', nTr)[5].setAttribute('class', 'qtdNav01');
                    $('td', nTr)[6].setAttribute('class', 'qtdNav02');
                    $('td', nTr)[7].setAttribute('class', 'qtdNav03');
                    $('td', nTr)[8].setAttribute('class', 'qtdNav04');
                    $('td', nTr)[9].setAttribute('class', 'qtdNav05');
                    $('td', nTr)[10].setAttribute('class', 'tempoJogada');
                    $('td', nTr)[11].setAttribute('class', 'tempoPosicionamento');

                    closeModalBS();

                }
            }
        });
    } return true;
}

function editNivel() {
    if (($('#niveis tbody').children('.selected').length > 0) && $('#niveis tbody').children('.selected').children('.nome').text() != "") {
        var row = $('#niveis tbody').children('.selected');
        var cIdNivel = $(row).children('.idNivel').text();
        var cNomeNivel = $(row).children('.nome').text();
        var nQtdColunas = $(row).children('.qtdColunas').text();
        var nQtdLinhas = $(row).children('.qtdLinhas').text();
        var nQtdPortaAvioes = $(row).children('.qtdNav01').text();
        var nQtdDestroiers = $(row).children('.qtdNav02').text();
        var nQtdEncouracados = $(row).children('.qtdNav03').text();
        var nQtdCruzadores = $(row).children('.qtdNav04').text();
        var nQtdSubmarinos = $(row).children('.qtdNav05').text();
        var nTempoJogada = $(row).children('.tempoJogada').text();
        var nTempoPosicionamento = $(row).children('.tempoPosicionamento').text();

        var cText = '<div id="editarNivelMod">';
        cText += document.querySelector("#criarNivel").innerHTML;
        cText += '</div>';

        modalBS(cText, 'Editar Nivel', '#fff', '#000', 'Confirmar~confirmEditNivel("' + cIdNivel + '")@Cancelar~closeModalBS()');

        document.querySelector("#txtNomNivel").value = cNomeNivel;
        document.querySelector("#txtQtdColunas").value = nQtdColunas;
        document.querySelector("#txtQtdLinhas").value = nQtdLinhas;
        document.querySelector("#txtQtdPortaAvioes").value = nQtdPortaAvioes;
        document.querySelector("#txtQtdDestroiers").value = nQtdDestroiers;
        document.querySelector("#txtQtdEncouracados").value = nQtdEncouracados;
        document.querySelector("#txtQtdCruzadores").value = nQtdCruzadores;
        document.querySelector("#txtQtdSubmarinos").value = nQtdSubmarinos;
        document.querySelector("#txtTempoJogada").value = nTempoJogada;
        document.querySelector("#txtTempoPosicionamento").value = nTempoPosicionamento;
    }
    else
        modalBS('<p>Selecione um nivel clicando na linha correspondente da tabela abaixo!</p>', '<i class="fa fa-times-circle" aria-hidden="true"></i> &nbsp; Erro', '#FF3919', '#fff', 'OK~closeModalBS()', true);
}

function confirmEditNivel(idNivel) {

    if (validaPreenchimento()) {

        var data = "{";
        data += "'nome':'" + document.querySelector("#txtNomNivel").value + "',";
        data += "'qtdColunas':'" + document.querySelector("#txtQtdColunas").value + "',";
        data += "'qtdLinhas':'" + document.querySelector("#txtQtdLinhas").value + "',";
        data += "'qtdPortaAvioes':'" + document.querySelector("#txtQtdPortaAvioes").value + "',";
        data += "'qtdDestroiers':'" + document.querySelector("#txtQtdDestroiers").value + "',";
        data += "'qtdEncouracados':'" + document.querySelector("#txtQtdEncouracados").value + "',";
        data += "'qtdCruzadores':'" + document.querySelector("#txtQtdCruzadores").value + "',";
        data += "'qtdSubmarinos':'" + document.querySelector("#txtQtdSubmarinos").value + "',";
        data += "'tempoJogada':'" + document.querySelector("#txtTempoJogada").value + "',";
        data += "'tempoPosicionamento':'" + document.querySelector("#txtTempoPosicionamento").value + "',";
        data += "'id':'" + idNivel + "'";
        data += "}";

        $.ajax({
            type: "POST",
            data: data,
            url: "http://localhost:5471/Nivel/Editar", //COLOCAR URL
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (output) {
                if (!output.Sucesso) {
                    document.getElementById("alertNivelExist").innerHTML = "<i class='fa fa-times-circle' aria-hidden='true'></i>&nbsp;<strong>Oops! &nbsp; </strong>Nivel já existente com esse nome!";
                    $('#alertNivelExist').show();
                } else {

                    closeModalBS();
                    window.reload();
                }
            }
        });
    }
}

function delNivel() {
    if (($('#niveis tbody').children('.selected').length > 0) && $('#niveis tbody').children('.selected').children('.nome').text() != "")
        modalBS('<p>Tem certeza que deseja excluir este nivel?</p>', '<i class="fa fa-exclamation-triangle" aria-hidden="true"></i> &nbsp; Excluir Nivel', '#fcfc4b', '#000', 'Confirmar~confirmDelNivel()@Cancelar~closeModalBS()', true);
    else
        modalBS('<p>Selecione um nivel clicando na linha correspondente da tabela!</p>', '<i class="fa fa-times-circle" aria-hidden="true"></i> &nbsp; Erro', '#FF3919', '#fff', 'OK~closeModalBS()', true);
}

function confirmDelNivel() {
    var row = $('#niveis tbody').children('.selected');
    var idNivel = $(row).children('.idNivel').text();

    var data = "{";
    data += "'id':'" + idNivel + "'";
    data += "}";

    $.ajax({
        type: "POST",
        data: data,
        url: "http://localhost:5471/Nivel/Excluir",
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",

        success: function (output) {

            if (output.Sucesso == true) {
                
                var row = $('#niveis tbody').children('.selected');
                var nRow = row[0];
                $('#niveis').dataTable().fnDeleteRow(nRow);
                modalBS('<p>Nivel excluído com sucesso</p>', '<i class="fa fa-check-square" aria-hidden="true"></i> &nbsp; Sucesso', '#25A947', '#fff', 'OK~closeModalBS()', true);
                
            }

           
        }
    });

}