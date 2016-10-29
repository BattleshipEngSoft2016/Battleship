var count = 0;
var cElement = "";
var cValueElement = "";
var descricaoItem = "";

jQuery(document).ready(function () { 
    var tbSkin = $('#niveis').DataTable({
        responsive: true,
        "language": {
            "lengthMenu": "Exibir _MENU_ niveis por página",
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

function addNivel() {
    document.querySelector("#txtNomNivel").value = "";
    document.querySelector("#cmbStatusNivel").selectedIndex = 0;
	 document.querySelector("#txtTamanhoNivel").value = "";
    var cText = '<div id="criarNivelMod">';
    cText += document.querySelector("#criarNivel").innerHTML;
    cText += '</div>';

    modalBS(cText, 'Incluir Nivel', '#fff', '#000', 'Confirmar~confirmAddNivel()@Cancelar~closeModalBS("alertNivelExist")');
}

function confirmAddNivel() {
    var lRet = true;
    if (document.querySelector("#txtNomNivel").value == "") {
        lRet = false;
        $("#txtNomNivel").addClass('input-error');
    }
    if (document.querySelector("#txtTamanhoNivel").value == "" || document.querySelector("#txtTamanhoNivel").value == "0" || document.querySelector("#txtTamanhoNivel").value == "1") {
        lRet = false;
        $("#txtTamanhoNivel").addClass('input-error');
    }

    if (lRet) {
		var data = "{";
        data += "'nomeNivel':'" + document.querySelector("#txtNomNivel").value + "',";
        data += "'tamanhoNivel':'" + document.querySelector("#txtTamanhoNivel").value + "',";
		data += "'isAtivo':'" + document.querySelector("#cmbStatusNivel").value + "'";
        data += "}";
        $.ajax({
            type: "POST",
            data: data,
            url: "Index.aspx/CriarNivel",  //COLOCAR URL RUBY
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (output) {
                if (output.d == "JAEXISTE") {

                    document.getElementById("alertNivelExist").innerHTML = "<i class='fa fa-times-circle' aria-hidden='true'></i>&nbsp;<strong>Oops! &nbsp; </strong>Nivel já existente com esse nome!";
                    $('#alertNivelExist').show();

                }
                else {
                    $('#alertNivelExist').hide();
                    var obj = output.d; //aqui prestar atenção no formato do objeto
                    obj = JSON.parse(obj.replace(/'/g, '"'));
                    count++;
					
                    var newRow = $('#niveis').dataTable().fnAddData([
                        //aqui atribuir os valores que voltarem da function no ruby
						"",
                        obj.IdNivel,
						obj.NomeNivel,
						obj.StatusNivel,
						obj.DtCriacao,
                        obj.TamanhoNivel					
                    ]);

                    var oSettings = $('#niveis').dataTable().fnSettings();
                    var nTr = oSettings.aoData[newRow[0]].nTr;
                    $(nTr).addClass('linhaTabela');
                    $('td', nTr)[1].setAttribute('class', 'idNivel');
                    $('td', nTr)[2].setAttribute('class', 'nomeNivel');
                    $('td', nTr)[3].setAttribute('class', 'statusNivel');
                    $('td', nTr)[4].setAttribute('class', 'dtCriacaoNivel');
                    $('td', nTr)[5].setAttribute('class', 'tamanhoNivel');

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
        var cNomeNivel = $(row).children('.nomeNivel').text();
        var cStatusNivel = $(row).children('.statusNivel').text();
        var dDtCriacaoNivel = $(row).children('.dtCriacaoNivel').text();
        var nTamanhoNivel = $(row).children('.tamanhoNivel').text();

        var cText = '<div id="editarNivelMod">';
        cText += document.querySelector("#criarNivel").innerHTML;
        cText += '</div>';

        modalBS(cText, 'Editar Nivel', '#fff', '#000', 'Confirmar~confirmEditNivel("' + cIdNivel + '")@Cancelar~closeModalBS()');
		
		document.querySelector("#txtNomSkin").value = cNomeNivel ;
		document.querySelector("#cmbStatusNivel").selectedIndex = cStatusNivel == 'Ativo' ? 0 : 1;
		document.querySelector("#txtTamanhoNivel").value = nTamanhoNivel;
    }
    else
        modalBS('<p>Selecione um nivel clicando na linha correspondente da tabela abaixo!</p>', '<i class="fa fa-times-circle" aria-hidden="true"></i> &nbsp; Erro', '#FF3919', '#fff', 'OK~closeModalBS()', true);
}

function confirmEditNivel(idNivel) {
    var lRet = true;
	
     if (document.querySelector("#txtNomNivel").value == "") {
        lRet = false;
        $("#txtNomNivel").addClass('input-error');
    }
    if (document.querySelector("#txtTamanhoNivel").value == "" || document.querySelector("#txtTamanhoNivel").value == "0" || document.querySelector("#txtTamanhoNivel").value == "1") {
        lRet = false;
        $("#txtTamanhoNivel").addClass('input-error');
    }

    if (lRet) {
        var data = "{";
       data += "'nomeNivel':'" + document.querySelector("#txtNomNivel").value + "',";
        data += "'tamanhoNivel':'" + document.querySelector("#txtTamanhoNivel").value + "',";
		data += "'isAtivo':'" + document.querySelector("#cmbStatusNivel").value + "',";
        data += "'id':'" + idNivel + "'";
        data += "}";
        $.ajax({
            type: "POST",
            data: data,
            url: "Index.aspx/EditarNivel", //COLOCAR URL RUBY
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (output) {
                if (output.d == "JAEXISTE") {
                    document.getElementById("alertNivelExist").innerHTML = "<i class='fa fa-times-circle' aria-hidden='true'></i>&nbsp;<strong>Oops! &nbsp; </strong>Nivel já existente com esse nome!";
                    $('#alertNivelExist').show();
                } else {

                    var obj = JSON.parse(output.d); //aqui prestar atenção no formato do objeto
					
                    var table = $('#niveis').DataTable();
                    var indexes = table.rows().eq(0).filter(function (rowIdx) {
                        var row = $('#niveis tbody').children('.selected');
                        var cNomeSkin = $(row).children('.nomeNivel').text();

						//precisa editar o objeto da grid, não adianta editar só o html 
                        if ((table.cell(rowIdx, 2).data().search(cNomeSkin) != -1) ) {
						    var status = obj.StatusNivel == "1" ? "Ativo" : "Inativo";
						    $('#niveis').dataTable().fnUpdate(obj.NomeNivel, rowIdx, 2); 
							$('#niveis').dataTable().fnUpdate(status, rowIdx, 3);
                            $('#niveis').dataTable().fnUpdate(obj.TamanhoNivel, rowIdx, 5); //o 4 é a data de criacao do nivel					                           
                        }
                    });

					//agora edito o html
					var status = obj.StatusNivel == "1" ? "Ativo" : "Inativo";
                    $('#niveis tbody').children('.selected').children('.nomeNivel').text(obj.NomeNivel);
					$('#niveis tbody').children('.selected').children('.statusNivel').text(status);
					$('#niveis tbody').children('.selected').children('.tamanhoNivel').text(obj.TamanhoNivel);

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
        url: "Index.aspx/ExcluirNivel", //COLOCAR URL RUBY
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