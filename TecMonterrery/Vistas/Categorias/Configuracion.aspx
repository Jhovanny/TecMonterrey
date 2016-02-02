<%@ Page Title="" Language="C#" MasterPageFile="~/Cabecera.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="Vistas.Categorias.Configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
        <h1 class="page-header">Configuración</h1>
        <span class="label label-primary" >Lapso de tiempo :</span>
        <asp:DropDownList CssClass="btn btn-default dropdown-toggle" ID="ddltiempo" runat="server">
            <asp:ListItem Value="15">15</asp:ListItem>
            <asp:ListItem Value="30">30</asp:ListItem>
        </asp:DropDownList>
        <asp:Button CssClass="btn btn-primary" Text="Aceptar" OnClick="Unnamed_Click" runat="server" />
    </div>
</asp:Content>
