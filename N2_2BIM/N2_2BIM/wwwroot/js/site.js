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
