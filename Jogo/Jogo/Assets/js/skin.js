var count = 0;
var cElement = "";
var cValueElement = "";
var descricaoItem = "";

jQuery(document).ready(function () { 
    var tbSkin = $('#skins').DataTable({
        responsive: true,
        "language": {
            "lengthMenu": "Exibir _MENU_ skins por página",
            "zeroRecords": "Desculpe, não encontrei registros",
            "info": "Exibindo página _PAGE_ de _PAGES_",
            "infoEmpty": "Sem informações disponíveis",
            "infoFiltered": "(Total de _MAX_ registros)",
            "search": "Pesquisar:",
            "paginate": {
                "previous": "Página anterior",
                "next": "Próxima página"
            },
            "sDom": 'T<"clear">lfrtip',
            "oTableTools": {
                "sRowSelect": "single"
            }
        },
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [{
            className: 'control',
            orderable: false,
            targets: 0
        }],
        order: [1, 'asc']

    });

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
    document.querySelector("#cmbStatusSkin").selectedIndex = 0;
	 document.querySelector("#txtCoordenadas").value = "";
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
        data += "'nomeSkin':'" + document.querySelector("#txtNomSkin").value + "',";
        data += "'valorSkin':'" + document.querySelector("#txtValor").value + "',";
		data += "'isAtiva':'" + document.querySelector("#cmbStatusSkin").value + "',";
		data += "'coordenada':'" + document.querySelector("#txtCoordenadas").value + "',";
        data += "'destroyer':'" + document.querySelector("#txtDestroyer").value + "',";
		data += "'encouracado':'" + document.querySelector("#txtEncouracado").value + "',";
		data += "'cruzador':'" + document.querySelector("#txtCruzador").value + "',";
		data += "'submarino':'" + document.querySelector("#txtSubmarino").value + "'";
        data += "}";
        $.ajax({
            type: "POST",
            data: data,
            url: "Index.aspx/CriarSkin",  //COLOCAR URL RUBY
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
                    //var btnVerImagens = '<button type="button" id="btnImg' + count + '" class="btn btn-default" onclick="getImages(this);"><i class="fa fa-file-image-o" aria-hidden="true"></i></button>';
                    var newRow = $('#skins').dataTable().fnAddData([
                        //aqui atribuir os valores que voltarem da function no ruby
						"",
                        obj.IdSkin,
						obj.NomeSkin,
						obj.StatusSkin,
						obj.DtCriacao,
                        obj.ValorSkin,
						obj.Coordenada,
						obj.Destroyer,
						obj.Encouracado,
						obj.Cruzador,
						obj.Submarino
                       // btnVerImagens
                    ]);

                    var oSettings = $('#skins').dataTable().fnSettings();
                    var nTr = oSettings.aoData[newRow[0]].nTr;
                    $(nTr).addClass('linhaTabela');
                    $('td', nTr)[1].setAttribute('class', 'idSkin');
                    $('td', nTr)[2].setAttribute('class', 'nomeSkin');
                    $('td', nTr)[3].setAttribute('class', 'statusSkin');
                    $('td', nTr)[4].setAttribute('class', 'dtCriacaoSkin');
                    $('td', nTr)[5].setAttribute('class', 'valorSkin');
					$('td', nTr)[6].setAttribute('class', 'coordenada');
					$('td', nTr)[7].setAttribute('class', 'destroyer');
					$('td', nTr)[8].setAttribute('class', 'encouracado');
					$('td', nTr)[9].setAttribute('class', 'cruzador');
					$('td', nTr)[10].setAttribute('class', 'submarino');
                    //$('td', nTr)[11].setAttribute('class', 'imgSkin');

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
        var cNomeSkin = $(row).children('.nomeSkin').text();
        var cStatusSkin = $(row).children('.statusSkin').text();
        var dDtCriacaoSkin = $(row).children('.dtCriacaoSkin').text();
        var nValorSkin = $(row).children('.valorSkin').text();
		var cCoordenada = $(row).children('.coordenada').text();
		var cDestroyer = $(row).children('.destroyer').text();
		var cEncouracado = $(row).children('.encouracado').text();
		var cCruzador = $(row).children('.cruzador').text();
		var cSubmarino = $(row).children('.submarino').text();

        var cText = '<div id="editarSkinMod">';
        cText += document.querySelector("#criarSkin").innerHTML;
        cText += '</div>';

        modalBS(cText, 'Editar Skin', '#fff', '#000', 'Confirmar~confirmEditSkin("' + cIdSkin + '")@Cancelar~closeModalBS()');
		
		document.querySelector("#txtNomSkin").value = cNomeSkin ;
		document.querySelector("#txtValor").value = nValorSkin;
		document.querySelector("#cmbStatusSkin").selectedIndex = cStatusSkin == 'Ativa' ? 0 : 1;
		document.querySelector("#txtCoordenadas").value = cCoordenada;
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
        data += "'nomeSkin':'" + document.querySelector("#txtNomSkin").value + "',";
        data += "'valorSkin':'" + document.querySelector("#txtValor").value + "',";
		data += "'isAtiva':'" + document.querySelector("#cmbStatusSkin").value + "',";
		data += "'coordenada':'" + document.querySelector("#txtCoordenadas").value + "',";
        data += "'destroyer':'" + document.querySelector("#txtDestroyer").value + "',";
		data += "'encouracado':'" + document.querySelector("#txtEncouracado").value + "',";
		data += "'cruzador':'" + document.querySelector("#txtCruzador").value + "',";
		data += "'submarino':'" + document.querySelector("#txtSubmarino").value + "',";
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
                        var cNomeSkin = $(row).children('.nomeSkin').text();

						//precisa editar o objeto da grid, não adianta editar só o html 
                        if ((table.cell(rowIdx, 2).data().search(cNomeSkin) != -1) ) {
						    var status = obj.StatusSkin == "1" ? "Ativa" : "Inativa";
						    $('#skins').dataTable().fnUpdate(obj.NomeSkin, rowIdx, 2); 
							$('#skins').dataTable().fnUpdate(status, rowIdx, 3);
                            $('#skins').dataTable().fnUpdate(obj.ValorSkin, rowIdx, 5); //o 4 é a data de criacao da skin
							$('#skins').dataTable().fnUpdate(obj.Coordenada, rowIdx, 6);
							$('#skins').dataTable().fnUpdate(obj.Destroyer, rowIdx, 7);
							$('#skins').dataTable().fnUpdate(obj.Encouracado, rowIdx, 8);
							$('#skins').dataTable().fnUpdate(obj.Cruzador, rowIdx, 9);
							$('#skins').dataTable().fnUpdate(obj.Submarino, rowIdx, 10);							                           
                        }
                    });

					//agora edito o html
					var status = obj.StatusSkin == "1" ? "Ativa" : "Inativa";
                    $('#skins tbody').children('.selected').children('.nomeSkin').text(obj.NomeSkin);
					$('#skins tbody').children('.selected').children('.statusSkin').text(status);
					$('#skins tbody').children('.selected').children('.valorSkin').text(obj.ValorSkin);
					$('#skins tbody').children('.selected').children('.coordenada').text(obj.Coordenada);
					$('#skins tbody').children('.selected').children('.destroyer').text(obj.Destroyer);
					$('#skins tbody').children('.selected').children('.encouracado').text(obj.Encouracado);
					$('#skins tbody').children('.selected').children('.cruzador').text(obj.Cruzador);
					$('#skins tbody').children('.selected').children('.submarino').text(obj.Submarino);

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