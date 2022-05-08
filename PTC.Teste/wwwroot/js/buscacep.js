function BuscaCepPedido(o) {

    var cep = o.value.replace(/\D/g, '');

    if (cep != "") {
        var validaCep = /^[0-9]{8}$/;

        if (validaCep.test(cep)) {
            if ($("#endereco").val() === "") {
                $("#endereco").val("...");
            }
            if ($("#bairro").val() === "") {
                $("#bairro").val("...");
            }
            if ($("#cidade").val() === "") {
                $("#cidade").val("...");
            }
            if ($("#uf").val() === "") {
                $("#uf").val("...");
            }

            $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                function (dados) {
                    if (!("erro" in dados)) {
                        if ($("#endereco").val() === "...") {
                            $("#endereco").val(dados.logradouro);
                        }

                        if ($("#bairro").val() === "...") {
                            $("#bairro").val(dados.bairro);
                        }
                        if ($("#cidade").val() === "...") {
                            $("#cidade").val(dados.localidade);
                        }
                        if ($("#uf").val() === "...") {
                            $("#uf").val(dados.uf);
                        }
                    }
                    else {
                        alert("Cep não encontrado");
                    }
                });
        }
        else {
            alert("Formato de cep inválido");
        }
    }
}

function limparendereco() {
    $('#endereco').val('');
    $('#numero').val('');
    $('#complemento').val('');
    $('#bairro').val('');
    $('#cidade').val('');
    $('#uf').val('');
}