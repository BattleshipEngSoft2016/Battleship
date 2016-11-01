var count = 0;
var cElement = "";
var cValueElement = "";
var descricaoItem = "";

jQuery(document).ready(function () { 

    $('#skins tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tbSkin.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
	
});

function addSkin() {
    document.querySelector("#txtNomSkin").value = "";
    document.querySelector("#txtValor").value = "";
    document.querySelector("#txtCoordenadas").value = "";
    document.querySelector("#txtPortaAvioes").value = "";
    document.querySelector("#txtDestroyer").value = "";
    document.querySelector("#txtEncouracado").value = "";
    document.querySelector("#txtCruzador").value = "";
    document.querySelector("#txtSubmarino").value = "";
    var cText = '<div id="criarSkinMod">';
    cText += document.querySelector("#criarSkin").innerHTML;
    cText += '</div>';

    modalBS(cText, 'Incluir Skin', '#fff', '#000', 'Confirmar~confirmAddSkin()@Cancelar~closeModalBS("alertSkinExist")');
}

function confirmAddSkin() {
    var lRet = true;
    if (document.querySelector("#txtNomSkin").value == "") {
        lRet = false;
        $("#txtNomSkin").addClass('input-error');
    }
    if (document.querySelector("#txtValor").value == "") {
        lRet = false;
        $("#txtValor").addClass('input-error');
    }
	
	if (document.querySelector("#txtCoordenadas").value == "") {
        lRet = false;
        $("#txtCoordenadas").addClass('input-error');
    }
	if (document.querySelector("#txtPortaAvioes").value == "") {
        lRet = false;
        $("#txtPortaAvioes").addClass('input-error');
    }
	
	if (document.querySelector("#txtDestroyer").value == "") {
        lRet = false;
        $("#txtDestroyer").addClass('input-error');
    }
	
	if (document.querySelector("#txtEncouracado").value == "") {
        lRet = false;
        $("#txtEncouracado").addClass('input-error');
    }
	
	if (document.querySelector("#txtCruzador").value == "") {
        lRet = false;
        $("#txtCruzador").addClass('input-error');
    }
	
	if (document.querySelector("#txtSubmarino").value == "") {
        lRet = false;
        $("#txtSubmarino").addClass('input-error');
    }

    if (lRet) {
		var data = "{";
        	data += "'nome':'" + document.querySelector("#txtNomSkin").value + "',";
        	data += "'valor':'" + document.querySelector("#txtValor").value + "',";
		data += "'imgCoordenada':'" + document.querySelector("#txtCoordenadas").value + "',";
	    	data += "'imgNav01':'" + document.querySelector("#txtPortaAvioes").value + "',";
        	data += "'imgNav02':'" + document.querySelector("#txtDestroyer").value + "',";
		data += "'imgNav03':'" + document.querySelector("#txtEncouracado").value + "',";
		data += "'imgNav04':'" + document.querySelector("#txtCruzador").value + "',";
		data += "'imgNav05':'" + document.querySelector("#txtSubmarino").value + "'";
        data += "}";
        $.ajax({
            type: "POST",
            data: data,
            url: "Skin/Cadastrar",  
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (output) {
                if (output.d == "JAEXISTE") {

                    document.getElementById("alertSkinExist").innerHTML = "<i class='fa fa-times-circle' aria-hidden='true'></i>&nbsp;<strong>Oops! &nbsp; </strong>Skin já existente com esse nome!";
                    $('#alertSkinExist').show();

                }
                else {
                    $('#alertSkinExist').hide();
                    var obj = output.d; //aqui prestar atenção no formato do objeto
                    obj = JSON.parse(obj.replace(/'/g, '"'));
                    count++;
               
                    var newRow = $('#skins').dataTable().fnAddData([
                        //aqui atribuir os valores que voltarem da function no ruby
						"",
                        obj.Id,
						obj.Nome,
                        obj.Valor,
						obj.ImgCoordenada,
			    obj.ImgNav01,
						obj.ImgNav02,
						obj.ImgNav03,
						obj.ImgNav04,
						obj.ImgNav05
                    ]);

                    var oSettings = $('#skins').dataTable().fnSettings();
                    var nTr = oSettings.aoData[newRow[0]].nTr;
                    $(nTr).addClass('linhaTabela');
                    $('td', nTr)[1].setAttribute('class', 'idSkin');
                    $('td', nTr)[2].setAttribute('class', 'nome');
                    $('td', nTr)[3].setAttribute('class', 'valor');
		    $('td', nTr)[4].setAttribute('class', 'imgCoordenada');
	            $('td', nTr)[5].setAttribute('class', 'imgNav01');
		    $('td', nTr)[6].setAttribute('class', 'imgNav02');
		    $('td', nTr)[7].setAttribute('class', 'imgNav03');
		    $('td', nTr)[8].setAttribute('class', 'imgNav04');
		    $('td', nTr)[9].setAttribute('class', 'imgNav05');

                    closeModalBS();

                }
            }
       });
    } return lRet;
} 

function editSkin() {
    if (($('#skins tbody').children('.selected').length > 0) && $('#skins tbody').children('.selected').children('.nomeSkin').text() != "") {
        var row = $('#skins tbody').children('.selected');
        var cIdSkin = $(row).children('.idSkin').text();
        var cNomeSkin = $(row).children('.nome').text();
        var nValorSkin = $(row).children('.valor').text();
		var cCoordenada = $(row).children('.imgCoordenada').text();
	        var cPortaAvioes = $(row).children('.imgNav01').text();
		var cDestroyer = $(row).children('.imgNav02').text();
		var cEncouracado = $(row).children('.imgNav03').text();
		var cCruzador = $(row).children('.imgNav04').text();
		var cSubmarino = $(row).children('.imgNav05').text();

        var cText = '<div id="editarSkinMod">';
        cText += document.querySelector("#criarSkin").innerHTML;
        cText += '</div>';

        modalBS(cText, 'Editar Skin', '#fff', '#000', 'Confirmar~confirmEditSkin("' + cIdSkin + '")@Cancelar~closeModalBS()');
		
		document.querySelector("#txtNomSkin").value = cNomeSkin ;
		document.querySelector("#txtValor").value = nValorSkin;
		document.querySelector("#txtCoordenadas").value = cCoordenada;
	        document.querySelector("#txtPortaAvioes").value = cPortaAvioes;
		document.querySelector("#txtDestroyer").value = cDestroyer;
		document.querySelector("#txtEncouracado").value = cEncouracado;
		document.querySelector("#txtCruzador").value = cCruzador;
		document.querySelector("#txtSubmarino").value = cSubmarino;

    }
    else
        modalBS('<p>Selecione uma skin clicando na linha correspondente da tabela abaixo!</p>', '<i class="fa fa-times-circle" aria-hidden="true"></i> &nbsp; Erro', '#FF3919', '#fff', 'OK~closeModalBS()', true);
}

function confirmEditSkin(idSkin) {
    var lRet = true;
	
     if (document.querySelector("#txtNomSkin").value == "") {
        lRet = false;
        $("#txtNomSkin").addClass('input-error');
    }
    if (document.querySelector("#txtValor").value == "") {
        lRet = false;
        $("#txtValor").addClass('input-error');
    }
	
	if (document.querySelector("#txtCoordenadas").value == "") {
        lRet = false;
        $("#txtCoordenadas").addClass('input-error');
    }
	
	if (document.querySelector("#txtPortaAvioes").value == "") {
        lRet = false;
        $("#txtPortaAvioes").addClass('input-error');
    }
	
	if (document.querySelector("#txtDestroyer").value == "") {
        lRet = false;
        $("#txtDestroyer").addClass('input-error');
    }
	
	if (document.querySelector("#txtEncouracado").value == "") {
        lRet = false;
        $("#txtEncouracado").addClass('input-error');
    }
	
	if (document.querySelector("#txtCruzador").value == "") {
        lRet = false;
        $("#txtCruzador").addClass('input-error');
    }
	
	if (document.querySelector("#txtSubmarino").value == "") {
        lRet = false;
        $("#txtSubmarino").addClass('input-error');
    }

    if (lRet) {
        var data = "{";
        data += "'nome':'" + document.querySelector("#txtNomSkin").value + "',";
        data += "'valor':'" + document.querySelector("#txtValor").value + "',";
	data += "'imgCoordenada':'" + document.querySelector("#txtCoordenadas").value + "',";
	data += "'imgNav01':'" + document.querySelector("#txtPortaAvioes").value + "',";
        data += "'imgNav02':'" + document.querySelector("#txtDestroyer").value + "',";
	data += "'imgNav03':'" + document.querySelector("#txtEncouracado").value + "',";
	data += "'imgNav04':'" + document.querySelector("#txtCruzador").value + "',";
	data += "'imgNav05':'" + document.querySelector("#txtSubmarino").value + "',";
        data += "'id':'" + idSkin + "'";
        data += "}";
        $.ajax({
            type: "POST",
            data: data,
            url: "Index.aspx/EditarSkin", //COLOCAR URL RUBY
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (output) {
                if (output.d == "JAEXISTE") {
                    document.getElementById("alertSkinExist").innerHTML = "<i class='fa fa-times-circle' aria-hidden='true'></i>&nbsp;<strong>Oops! &nbsp; </strong>Skin já existente com esse nome!";
                    $('#alertSkinExist').show();
                } else {

                    var obj = JSON.parse(output.d); //aqui prestar atenção no formato do objeto
					
					

                    var table = $('#skins').DataTable();
                    var indexes = table.rows().eq(0).filter(function (rowIdx) {
                        var row = $('#skins tbody').children('.selected');
                        var cNomeSkin = $(row).children('.nome').text();

						//precisa editar o objeto da grid, não adianta editar só o html 
                        if ((table.cell(rowIdx, 2).data().search(cNomeSkin) != -1) ) {
						    $('#skins').dataTable().fnUpdate(obj.Nome, rowIdx, 2); 
                            $('#skins').dataTable().fnUpdate(obj.Valor, rowIdx, 3); //o 4 é a data de criacao da skin
							$('#skins').dataTable().fnUpdate(obj.ImgCoordenada, rowIdx, 4);
							$('#skins').dataTable().fnUpdate(obj.ImgNav01, rowIdx, 5);
							$('#skins').dataTable().fnUpdate(obj.ImgNav02, rowIdx, 6);
							$('#skins').dataTable().fnUpdate(obj.ImgNav03, rowIdx, 7);
							$('#skins').dataTable().fnUpdate(obj.ImgNav04, rowIdx, 8);
							$('#skins').dataTable().fnUpdate(obj.ImgNav05, rowIdx, 9);							                           
                        }
                    });

					//agora edito o html
                    $('#skins tbody').children('.selected').children('.nomeSkin').text(obj.Nome);
					$('#skins tbody').children('.selected').children('.valor').text(obj.Valor);
					$('#skins tbody').children('.selected').children('.imgCoordenada').text(obj.ImgCoordenada);
					$('#skins tbody').children('.selected').children('.imgNav01').text(obj.ImgNav01);
					$('#skins tbody').children('.selected').children('.imgNav02').text(obj.ImgNav02);
					$('#skins tbody').children('.selected').children('.imgNav03').text(obj.ImgNav03);
					$('#skins tbody').children('.selected').children('.imgNav04').text(obj.ImgNav04);
					$('#skins tbody').children('.selected').children('.imgNav05').text(obj.ImgNav05);

                    closeModalBS();
					
                }
            }
        });
    }
}

function delSkin() {
    if (($('#skins tbody').children('.selected').length > 0) && $('#skins tbody').children('.selected').children('.nomeSkin').text() != "")
        modalBS('<p>Tem certeza que deseja excluir esta skin?</p>', '<i class="fa fa-exclamation-triangle" aria-hidden="true"></i> &nbsp; Excluir Skin', '#fcfc4b', '#000', 'Confirmar~confirmDelSkin()@Cancelar~closeModalBS()', true);
    else
        modalBS('<p>Selecione uma skin clicando na linha correspondente da tabela!</p>', '<i class="fa fa-times-circle" aria-hidden="true"></i> &nbsp; Erro', '#FF3919', '#fff', 'OK~closeModalBS()', true);
}

function confirmDelSkin() {
    var row = $('#skins tbody').children('.selected');
    var idSkin = $(row).children('.idSkin').text();

    var data = "{";
    data += "'id':'" + idSkin + "'";
    data += "}";

    $.ajax({
        type: "POST",
        data: data,
        url: "Index.aspx/ExcluirSkin", //COLOCAR URL RUBY
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",

        success: function (output) {
        
                var row = $('#skins tbody').children('.selected')
                var nRow = row[0];
                $('#skins').dataTable().fnDeleteRow(nRow);
                modalBS('<p>Skin excluída com sucesso</p>', '<i class="fa fa-check-square" aria-hidden="true"></i> &nbsp; Sucesso', '#25A947', '#fff', 'OK~closeModalBS()', true);
            
        }
    });
    
}
