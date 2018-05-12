<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="ProyectoLPPA.IniciarSesion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Usuario: "></asp:Label>
    <asp:TextBox ID="txt_Usuario" runat="server"></asp:TextBox>
<br />
<asp:Label ID="Label2" runat="server" Text="Contraseña: "></asp:Label>
<asp:TextBox ID="txt_Password" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Text="El usuario o la contraseña son incorrectos." Visible="False"></asp:Label>
    <br />
<br />
<asp:Button ID="btn_Iniciar" runat="server" Text="Iniciar" OnClick="btn_Iniciar_Click" />
</asp:Content>
