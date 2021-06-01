function apagarRegistro(id, controller) {
    swal({
        title: "Tem certeza?",
        text: "Este registro será removido para sempre!",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Sim!",
        cancelButtonText: "Não!",
        closeOnConfirm: false
    },
        function () {
            location.href = '/' + controller + '/Delete?id=' + id;
        });
}

function exibirImagem() {
    var oFReader = new FileReader();
    oFReader.readAsDataURL(document.getElementById("Imagem").files[0]);
    oFReader.onload = function (oFREvent) {
        document.getElementById("imgPreview").src = oFREvent.target.result;
    };
}

function apagar(id,controller) {
    if (confirm('Confirma a exclusão do registro?'))
        location.href = '/' + controller + '/Delete?id=' + id;
}


function efetuaFiltroAluno() {
	var nomeAluno = $("#nomeAluno").val();
	var dataNascimento = $("#dataNascimento").val();

	$.ajax({
		url: "/aluno/FazConsultaAjax?nomeAluno=" + nomeAluno +
			"&dataNascimento=" + dataNascimento,

		cache: false,
		beforeSend: function () {
			$("#imgWait").show();
		},
		success: function (dados) {
			$("#imgWait").hide();
			if (dados.erro != undefined)  // quando o CEP não existe...
			{
				alert('Ocorreu um erro ao processar a sua requisição. Tente novamente mais tarde..');
			}
			else // quando o CEP existe			   
			{
				$("#conteudoGrid").html(dados);
			}
		}
	});
}

function efetuaFiltroAula() {
	var nomeAluno = $("#nomeAlunoAula").val();
	var dataAula = $("#dataAula").val();

	$.ajax({
		url: "/aula/FazConsultaAjax?nomeAlunoAula=" + nomeAluno +
			"&dataNascimento=" + dataAula,

		cache: false,
		beforeSend: function () {
			$("#imgWait").show();
		},
		success: function (dados) {
			$("#imgWait").hide();
			if (dados.erro != undefined)  // quando o CEP não existe...
			{
				alert('Ocorreu um erro ao processar a sua requisição. Tente novamente mais tarde..');
			}
			else // quando o CEP existe			   
			{
				$("#conteudoGrid").html(dados);
			}
		}
	});
}


function calculoRiscoCardiaco() {
	var nomeAluno = $("#nomeAlunoAula").val();
	var dataAula = $("#dataAula").val();

	$.ajax({
		url: "/RiscoCardiaco/CalculaCardiaco?nomeAlunoAula=" + nomeAluno +
			"&dataNascimento=" + dataAula,

		cache: false,
		beforeSend: function () {
			$("#imgWait").show();
		},
		success: function (dados) {
			$("#imgWait").hide();
			if (dados.erro != undefined)  // quando o CEP não existe...
			{
				alert('Ocorreu um erro ao processar a sua requisição. Tente novamente mais tarde..');
			}
			else // quando o CEP existe			   
			{
				$("#conteudoGrid").html(dados);
			}
		}
	});
}

function efetuaFiltroAnamnese() {
	var idAluno = $("#idAluno").val();
	var dataAvaliacao = $("#dataAvaliacao").val();

	$.ajax({
		url: "/anamnese/FazConsultaAjax?nomeAluno=" + idAluno +
			"&dataNascimento=" + dataAvaliacao,

		cache: false,
		beforeSend: function () {
			$("#imgWait").show();
		},
		success: function (dados) {
			$("#imgWait").hide();
			if (dados.erro != undefined)  // quando o CEP não existe...
			{
				alert('Ocorreu um erro ao processar a sua requisição. Tente novamente mais tarde..');
			}
			else // quando o CEP existe			   
			{
				$("#conteudoGrid").html(dados);
			}
		}
	});
}

function validaResultados() {
	if (document.getElementById('IMC').value != null && document.getElementById('Resultado').value != "") {
		{
			document.getElementById('btnSalvar').disabled = false;
		}
    }
}

function imprimeAnamnese() {
	document.getElementById('btnPrint').hidden = true;
	document.getElementById('cabecalhoLayout').hidden = true;
	window.print();
}
