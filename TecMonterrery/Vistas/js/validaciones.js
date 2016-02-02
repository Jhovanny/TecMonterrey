$(document).ready(function () {

    var general = "";
    function getOrdersByCustomer(fi, ff, inter) {
        var actiondata = '{""}';
        var uno;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Index.aspx/BuscarNumAleatorio",
            dataType: "json",
            success: function (response) {
                var contactos = (typeof response.d) == 'string' ?
                               eval('(' + response.d + ')') :
                               response.d;
                grafica(contactos);
            },
            error: function (request, status, error) {
                alert(jQuery.parseJSON(request.responseText).Message);
            }

        });
    }
    function GetDate(contactos) {
        var general = [];
        for (var i = 0; i < contactos.length; i++) {
            general.push({ "period": "" + contactos[i].horaInicio + "", "atendidas": contactos[i].atendidas, "abandonadas": contactos[i].abandonadas });
            
        }
        console.log(general);
        return general;
    }
    function grafica(gen) {
        var day_data = GetDate(gen)
        Morris.Line({
            element: 'graph',
            data: day_data,
            xkey: 'period',
            ykeys: ['atendidas', 'abandonadas'],
            labels: ['Atendidas', 'Abandonadas']
        });
    }
    $("#txtGrafica").click(function (e) {
        var fi = $(".txtfechainicio").val();
        var ff = $(".txtfechafin").val();
        if (fi != "" && ff != "") {
            getOrdersByCustomer();
            e.preventDefault();
        }
        else {
            alert("Complete los campos por favor.");
        }

    });

});
    

