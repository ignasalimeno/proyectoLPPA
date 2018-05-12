<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BDAlteradaWebMaster.aspx.cs" Inherits="ProyectoLPPA.BDAlteradaWebMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     La BD está alterada. A continuación se detallas las tablas y el ID del registro que están corruptos:
    
    <asp:GridView ID="grillaErrores" runat="server" OnPageIndexChanging="grillaErrores_PageIndexChanging">
    </asp:GridView>

    <br />
    <asp:Button ID="btn_RecalcularDV" runat="server" Text="Recalcular DV" OnClick="btn_RecalcularDV_Click" />
     <br />
     Elija una ruta para restablecer un backup:<br />
     <asp:FileUpload ID="FileUpload1" runat="server" Width="449px" />
    <br />
    <asp:Button ID="btn_RestaurarBD" runat="server" Text="Restaurar BD" OnClick="btn_RestaurarBD_Click" />
</asp:Content>
