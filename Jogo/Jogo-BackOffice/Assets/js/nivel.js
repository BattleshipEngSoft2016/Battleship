var count = 0;
var cElement = "";
var cValueElement = "";
var descricaoItem = "";

jQuery(document).ready(function () { 
    $('#niveis tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tbSkin.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
});

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

    if (document.querySelector("#txtTempoJogada").value == "" || document.querySelector("#txtTempoJogada").value < 10 ) {
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
            url: "Index.aspx/CriarNivel",  //COLOCAR URL CONTROLLER
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (output) {
                if (output.d == "JAEXISTE") {

                    document.getElementById("alertNivelExist").innerHTML = "<i class='fa fa-times-circle' aria-hidden='true'></i>&nbsp;<strong>Oops! &nbsp; </strong>Nivel já existente com esse nome!";
                    $('#alertNivelExist').show();

                }
                else {
                    $('#alertNivelExist').hide();
                    var niveis = output.d; //aqui prestar atenção no formato do objeto
                    obj = JSON.parse(obj.replace(/'/g, '"'));
                    count++;
					
                    var newRow = $('#niveis').dataTable().fnAddData([
                       "",
                       niveis[nI].Id,
                       niveis[nI].Nome,
                       niveis[nI].QtdColunas,
                       niveis[nI].QtdLinhas,
                       niveis[nI].QtdNav01,
                       niveis[nI].QtdNav02,
                       niveis[nI].QtdNav03,
                       niveis[nI].QtdNav04,
                       niveis[nI].QtdNav05,
                       niveis[nI].TempoJogada,
                       niveis[nI].TempoPosicionamento
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
    } return lRet;
} 

function editNivel() {
    if (($('#niveis tbody').children('.selected').length > 0) && $('#niveis tbody').children('.selected').children('.nomeNivel').text() != "") {
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
		
		document.querySelector("#txtNomSkin").value = cNomeNivel ;
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
            url: "Index.aspx/EditarNivel", //COLOCAR URL
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (output) {
                if (output.d == "JAEXISTE") {
                    document.getElementById("alertNivelExist").innerHTML = "<i class='fa fa-times-circle' aria-hidden='true'></i>&nbsp;<strong>Oops! &nbsp; </strong>Nivel já existente com esse nome!";
                    $('#alertNivelExist').show();
                } else {

                    var niveis = JSON.parse(output.d); //aqui prestar atenção no formato do objeto
					
                    var table = $('#niveis').DataTable();
                    var indexes = table.rows().eq(0).filter(function (rowIdx) {
                        var row = $('#niveis tbody').children('.selected');
                        var cNomeSkin = $(row).children('.nome').text();

						//precisa editar o objeto da grid, não adianta editar só o html 
                        if ((table.cell(rowIdx, 2).data().search(cNomeSkin) != -1) ) {
                            $('#niveis').dataTable().fnUpdate(niveis.Nome, rowIdx, 2);
                            $('#niveis').dataTable().fnUpdate(niveis.QtdColunas, rowIdx, 3);
                            $('#niveis').dataTable().fnUpdate(niveis.QtdLinhas, rowIdx, 4);
                            $('#niveis').dataTable().fnUpdate(niveis.QtdNav01, rowIdx, 5);
                            $('#niveis').dataTable().fnUpdate(niveis.QtdNav02, rowIdx, 6);
                            $('#niveis').dataTable().fnUpdate(niveis.QtdNav03, rowIdx, 7);
                            $('#niveis').dataTable().fnUpdate(niveis.QtdNav04, rowIdx, 8);
                            $('#niveis').dataTable().fnUpdate(niveis.QtdNav05, rowIdx, 9);
                            $('#niveis').dataTable().fnUpdate(niveis.TempoJogada, rowIdx, 10);
                            $('#niveis').dataTable().fnUpdate(niveis.TempoPosicionamento, rowIdx, 11);
									                           
                        }
                    });

					//agora edito o html
                    $('#niveis tbody').children('.selected').children('.nome').text(niveis.Nome);
                    $('#niveis tbody').children('.selected').children('.qtdColunas').text(niveis.QtdColunas);
                    $('#niveis tbody').children('.selected').children('.qtdLinhas').text(niveis.QtdLinhas);
                    $('#niveis tbody').children('.selected').children('.qtdNav01').text(niveis.QtdNav01);
                    $('#niveis tbody').children('.selected').children('.qtdNav02').text(niveis.QtdNav02);
                    $('#niveis tbody').children('.selected').children('.qtdNav03').text(niveis.QtdNav03);
                    $('#niveis tbody').children('.selected').children('.qtdNav04').text(niveis.QtdNav04);
                    $('#niveis tbody').children('.selected').children('.qtdNav05').text(niveis.QtdNav05);
                    $('#niveis tbody').children('.selected').children('.tempoJogada').text(niveis.TempoJogada);
                    $('#niveis tbody').children('.selected').children('.tempoPosicionamento').text(niveis.TempoPosicionamento);
                    
                    closeModalBS();
					
                }
            }
        });
    }
}

function delNivel() {
    if (($('#niveis tbody').children('.selected').length > 0) && $('#niveis tbody').children('.selected').children('.nomeNivel').text() != "")
        modalBS('<p>Tem certeza que deseja excluir este nivel?</p>', '<i class="fa fa-exclamation-triangle" aria-hidden="true"></i> &nbsp; Excluir Nivel', '#fcfc4b', '#000', 'Confirmar~confirmDelNivel()@Cancelar~closeModalBS()', true);
    else
        modalBS('<p>Selecione um nivel clicando na linha correspondente da tabela!</p>', '<i class="fa fa-times-circle" aria-hidden="true"></i> &nbsp; Erro', '#FF3919', '#fff', 'OK~closeModalBS()', true);
}

function confirmDelSkin() {
    var row = $('#niveis tbody').children('.selected');
    var idNivel = $(row).children('.idNivel').text();

    var data = "{";
    data += "'id':'" + idNivel + "'";
    data += "}";

    $.ajax({
        type: "POST",
        data: data,
        url: "Index.aspx/ExcluirNivel", //COLOCAR URL
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",

        success: function (output) {
        
                var row = $('#niveis tbody').children('.selected')
                var nRow = row[0];
                $('#niveis').dataTable().fnDeleteRow(nRow);
                modalBS('<p>Nivel excluído com sucesso</p>', '<i class="fa fa-check-square" aria-hidden="true"></i> &nbsp; Sucesso', '#25A947', '#fff', 'OK~closeModalBS()', true);
            
        }
    });
    
}