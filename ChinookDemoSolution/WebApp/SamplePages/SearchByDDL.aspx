<%@ Page Title="Search By DLL" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchByDDL.aspx.cs" Inherits="WebApp.SamplePages.SearchByDDL" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Search Albums by Artist</h1>
    <div class="row">
        <div class="offset-2">
            <br />
            <asp:Label ID="Label1" runat="server" Text="Select an Artist"></asp:Label>&nbsp;&nbsp;
            <asp:DropDownList ID="ArtistList" runat="server"></asp:DropDownList>&nbsp;&nbsp;
            <asp:LinkButton ID="SearchAlbums" runat="server" OnClick="SearchAlbums_Click"> <i class="fa fa-search"></i> Search</asp:LinkButton>&nbsp;&nbsp;
        </div>
    </div>
    <br />
    <hr />
    <br />
    <div class="row">
        <div class="offset-2">
            <%--<asp:Label ID="MessageLabel" runat="server" Text=""></asp:Label>--%>
            <%-- implement the freecode.webapp user control --%>
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>
    <br />
    <br />
    <div class="row">
        <div class="offset-2">
            <asp:GridView ID="ArtistAlbumsGridView" runat="server" AutoGenerateColumns="False" 
                CssClass="table table-striped" 
                GridLines="Horizontal" 
                BorderStyle="Groove">

                <Columns>
                    
                    <asp:TemplateField HeaderText="Album">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Released">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("ReleaseYear") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center">

                        </ItemStyle>
                    </asp:TemplateField>
                        
                    <asp:TemplateField HeaderText="Artist">
                        <ItemTemplate>
                            <asp:DropDownList ID="ArtistNameList" runat="server"
                                DataSourceID="ArtistNameListODS"
                                DataTextField="DisplayField"
                                DataValueField="ValueField"
                                SelectedValue='<%# Eval("ArtistId") %>'>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No records for selected Artist
                </EmptyDataTemplate>
            </asp:GridView>
            <%-- added the SelectCheckForException method we did in the code behind for MessageUserControl (OnSelected="MethodName") --%>
            <asp:ObjectDataSource ID="ArtistNameListODS" runat="server"  OnSelected="SelectCheckForException"
                OldValuesParameterFormatString="original_{0}" SelectMethod="Artists_DDLList" 
                TypeName="ChinookSystem.BLL.ArtistController"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
