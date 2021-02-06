<%@ Page Title="Repeater With Nested Query" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODS_Repeater.aspx.cs" Inherits="WebApp.SamplePages.ODS_Repeater" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Repeater With Nested Query</h1>

    <br />

    <div class="row">
        <div class="offset-2">
            <uc1:messageusercontrol runat="server" id="MessageUserControl" />
        </div>
    </div>

    <br />

    <div class="row">
        <div class="offset-2">
            <asp:Repeater ID="EmployeeCustomers" runat="server" DataSourceID="EmployeeCustomersODS" 
                ItemType="ChinookSystem.ViewModels.EmployeeCustomerList">

                <HeaderTemplate>
                    <h3>Sales Support Employees</h3>
                </HeaderTemplate>

                <ItemTemplate>

                    <%# Item.EmployeeName %> (<%# Item.Title %>) has <%# Item.NumberOfCustomers %> Customers

                    <%-- NOTE: USE DataSource NOT DataSourceID in this scenario --%>
                    <%--<asp:GridView ID="SupportedCustomersOfEmployee" runat="server" DataSource="<%# Item.Customers %>" 
                        ItemType="ChinookSystem.ViewModels.CustomerSupportItem">

                    </asp:GridView>--%>

                    <%-- Alternatively, we can nest a repeater inside the other repeater to show the inner list --%>
                    <asp:Repeater ID="SupportedCustomersOfEmploye" runat="server" DataSource="<%# Item.Customers %>" 
                        ItemType="ChinookSystem.ViewModels.CustomerSupportItem">

                        <ItemTemplate>
                            Name: <%# Item.CustomerName %> &nbsp; &nbsp;
                            Phone: <%# Item.Phone %> &nbsp; &nbsp;
                            State: <%# Item.State %> &nbsp; &nbsp;
                            City: <%# Item.City %> &nbsp; &nbsp;
                            <br />
                        </ItemTemplate>

                    </asp:Repeater>

                </ItemTemplate>

            </asp:Repeater>
        </div>
    </div>

    <%-- ODS Sections --%>

    <asp:ObjectDataSource ID="EmployeeCustomersODS" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Employee_EmployeeCustomerList" TypeName="ChinookSystem.BLL.EmployeeController"></asp:ObjectDataSource>

</asp:Content>
