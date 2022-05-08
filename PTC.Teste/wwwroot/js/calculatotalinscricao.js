function calculatotalinscricao(eventoId, provaId, codigoCupomDesconto, quantidadeAcompanhanteAdulto, quantidadeAcompanhanteInfantil) {


    let url = '/Inscricao/CalculaTotalInscricao';

    $.ajax({
        type: "GET",
        datatype: "json",
        data: {
            provaId: provaId,
            eventoId: eventoId,
            codigoCupomDesconto: codigoCupomDesconto,
            quantidadeAcompanhanteAdulto: quantidadeAcompanhanteAdulto,
            quantidadeAcompanhanteInfantil: quantidadeAcompanhanteInfantil
        },
        url: url,
        success: function (data) {
            $("#valorinscricao").html(data.valorInscricao.toFixed(2).replace('.', ','));
            $("#valordesconto").html(data.valorDesconto.toFixed(2).replace('.', ','));
            $("#valortotal").html(data.valorTotal.toFixed(2).replace('.', ','));
            $("#valorAcompanhanteAdulto").html(data.valorAcompanhanteAdulto.toFixed(2).replace('.', ','));
            $("#valorAcompanhanteInfantil").html(data.valorAcompanhanteInfantil.toFixed(2).replace('.', ','));
            $("#totalAcompanhanteAdulto").html(data.totalAcompanhanteAdulto.toFixed(2).replace('.', ','));
            $("#totalAcompanhanteInfantil").html(data.totalAcompanhanteInfantil.toFixed(2).replace('.', ','));
        }
    });

}