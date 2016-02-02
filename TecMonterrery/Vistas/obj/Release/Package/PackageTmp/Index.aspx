<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Vistas.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
        <h1 class="page-header">Reportes Generales</h1>
        <span class="label label-primary">Fecha Inicio :</span>
        <input type="text" name="txtfechainicio" class="txtfechainicio" id="txtfechainicio" value="" runat="server" required="required" />
        <span class="label label-primary">Fecha Fin :</span><input type="text" class="txtfechafin" name="txtfechafin" id="txtfechafin" value="" runat="server" required="required" />
        <!--<button class="btn btn-primary btngeberarReporte"  id="btngeberarReporte" OnClick="btngeberarReporte_Click"  >Generar</button>-->
       <asp:Button Text="Generar" class="btn btn-primary btngeberarReporte" id="btngeberarReportes" OnClick="btngeberarReporte_Click" runat="server" />
        <button class="btn btn-default" id="txtGrafica">Grafica</button>
        <!--<button class="btn btn-primary" id="txtGrafica">Grafica</button>-->

        
        <script>
            var logic = function (currentDateTime) {
                // 'this' is jquery object datetimepicker
                if (currentDateTime.getDay() == 6) {
                    this.setOptions({
                        minTime: '6:00'
                    });
                } else
                    this.setOptions({
                        minTime: '6:00'
                    });
            };
            jQuery('#<%=txtfechainicio.ClientID%>').datetimepicker({
                onChangeDateTime: logic,
                onShow: logic

            });
            jQuery('#<%=txtfechafin.ClientID%>').datetimepicker({
                onChangeDateTime: logic,
                onShow: logic

            });
        </script>
        <div id="graph"></div>
            
    </div>
</asp:Content>
