<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="ProyectoLPPA.Backup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    A continuación, podrá realizar un restore de la BD.
<br />
<asp:FileUpload ID="FileUpload1" runat="server" Width="479px"  accept=".bak" />
    <br />
<asp:Button ID="btn_Restaurar" runat="server" Text="Restaurar" OnClick="btn_Restaurar_Click" />
</asp:Content>
