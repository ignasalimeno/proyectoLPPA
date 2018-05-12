<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="ProyectoLPPA.Bitacora" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    A continuación se muestran el detalle de las acciones registradas en el sistema:
    
    <asp:GridView ID="grillaLog" runat="server" EnableSortingAndPagingCallbacks="True" OnPageIndexChanging="grillaLog_PageIndexChanging" AllowPaging="True">
        <PagerSettings FirstPageText="1" LastPageText="Última" PageButtonCount="10" />
    </asp:GridView>
</asp:Content>
