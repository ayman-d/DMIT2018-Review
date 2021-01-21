<%@ Page Title="List View ODS CRUD" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListViewODSCRUD.aspx.cs" Inherits="WebApp.SamplePages.ListViewODSCRUD" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Single Control ODS CRUD : ListView</h1>
    <div class="row">
        <div class="offset-2">
            <blockquote class="alert alert-info">
                This page will:
                <ul>
                    <li>use the asp:ListView control</li>
                    <li>use Object Data Source ODS with the List View control</li>
                    <li>use minimum code behind to do CRUD operations</li>
                    <li>use the MessageUserControl for error handling</li>
                    <li>demonstrate validation on the List View control</li>
                </ul>
            </blockquote>
        </div>
    </div>
    <div class="row">
        <div class="offset-1">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
            <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" HeaderText="Following are concerns with your data:"
                ValidationGroup="egroup"/>
            <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" HeaderText="Following are concerns with your data:"
                ValidationGroup="igroup"/>
        </div>
    </div>

    <div class="row">
        <div class="offset-2">
            <%-- IMPORTANT --%>
            <%-- REMINDER: USE THE ATTRIBUTE DATAKEYNAMES TO GET THE DELETE FUNCTION OF YOUR LIST VIEW CRUD TO WORK --%>
            <%-- The DataKeyNames attribute is set to your PK field --%>
            
            <asp:ListView ID="AlbumList" runat="server" DataSourceID="AlbumListODS" InsertItemPosition="LastItem" DataKeyNames="AlbumId">

                <AlternatingItemTemplate>
                    <tr style="background-color: #FFFFFF; color: #284775;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton"
                                OnClientClick="return confirm('Are you sure you want to delete this Album?')" />
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" Width="60px" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                            <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" DataTextField="DisplayField" Width="300"
                                DataValueField="ValueField" SelectedValue='<%# Eval("ArtistId") %>' Enabled="false">
                            </asp:DropDownList>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" Width="70" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                    </tr>
                </AlternatingItemTemplate>

                <EditItemTemplate>
                    <%-- Validation controls will be placed inside the associated template (edit, insert, etc..) --%>
                    <%-- The ID of the validated control needs to be unique --%>
                    <%-- The validation controls for a particular template need to be grouped --%>
                    <%-- The validation executes ONLY on the use of the template of the user --%>

                    <asp:RequiredFieldValidator ID="RequiredTitleE" runat="server" 
                        ErrorMessage="Album title is required on Edit" ControlToValidate="TitleTextBoxE" 
                        Display="None" ValidationGroup="egroup">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredReleaseYearE" runat="server" 
                        ErrorMessage="Release year is required on Edit" ControlToValidate="ReleaseYearTextBoxE" 
                        Display="None" ValidationGroup="egroup">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegExTitleE" runat="server" 
                        ErrorMessage="Album title is limited to 160 characters." ControlToValidate="TitleTextBoxE"
                        Display="None" ValidationGroup="egroup" ValidationExpression="^.{1,160}$">
                    </asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegExReleaseLabelE" runat="server" 
                        ErrorMessage="Album Label is limited to 50 characters." ControlToValidate="ReleaseLabelTextBoxE"
                        Display="None" ValidationGroup="egroup" ValidationExpression="^.{1,50}$">
                    </asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareReleaseYearE" runat="server" 
                        ErrorMessage="Year must be a number." ControlToValidate="ReleaseYearTextBoxE"
                        Display="None" ValidationGroup="egroup" Operator="DataTypeCheck" Type="Integer">
                    </asp:CompareValidator>
                    <asp:RangeValidator ID="RangeReleaseYearE" runat="server" 
                        ErrorMessage="Year must be between 1950 and this year" ControlToValidate="ReleaseYearTextBoxE"
                        Display="None" ValidationGroup="egroup" MinimumValue="1950" MaximumValue="<%# DateTime.Today.Year %>">
                    </asp:RangeValidator>

                    <tr style="background-color: #999999;">
                        <td>
                            <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" ValidationGroup="egroup" />
                            <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" Width="60px" Enabled="false" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxE" /></td>
                        <td>
                            <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" DataTextField="DisplayField" Width="300"
                                DataValueField="ValueField" SelectedValue='<%# Bind("ArtistId") %>'>
                            </asp:DropDownList>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBoxE" Width="70" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBoxE" /></td>
                    </tr>
                </EditItemTemplate>

                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>

                <InsertItemTemplate>
                    <%-- Validation controls will be placed inside the associated template (edit, insert, etc..) --%>
                    <%-- The ID of the validated control needs to be unique --%>
                    <%-- The validation controls for a particular template need to be grouped --%>
                    <%-- The validation executes ONLY on the use of the template of the user --%>

                    <asp:RequiredFieldValidator ID="RequiredTitleI" runat="server" 
                        ErrorMessage="Album title is required on Insert" ControlToValidate="TitleTextBoxI" 
                        Display="None" ValidationGroup="igroup">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredReleaseYearI" runat="server" 
                        ErrorMessage="Release year is required on Insert" ControlToValidate="ReleaseYearTextBoxI" 
                        Display="None" ValidationGroup="igroup">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegExTitleI" runat="server" 
                        ErrorMessage="Album title is limited to 160 characters." ControlToValidate="TitleTextBoxI"
                        Display="None" ValidationGroup="igroup" ValidationExpression="^.{1,160}$">
                    </asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegExReleaseLabelI" runat="server" 
                        ErrorMessage="Album Label is limited to 50 characters." ControlToValidate="ReleaseLabelTextBoxI"
                        Display="None" ValidationGroup="igroup" ValidationExpression="^.{1,50}$">
                    </asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareReleaseYearI" runat="server" 
                        ErrorMessage="Year must be a number." ControlToValidate="ReleaseYearTextBoxI"
                        Display="None" ValidationGroup="igroup" Operator="DataTypeCheck" Type="Integer">
                    </asp:CompareValidator>
                    <asp:RangeValidator ID="RangeReleaseYearI" runat="server" 
                        ErrorMessage="Year must be between 1950 and this year" ControlToValidate="ReleaseYearTextBoxI"
                        Display="None" ValidationGroup="igroup" MinimumValue="1950" MaximumValue="<%# DateTime.Today.Year %>">
                    </asp:RangeValidator>
                    
                    <tr style="">
                        <td>
                            <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" ValidationGroup="igroup" />
                            <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" Width="60px" Enabled="false" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxI" /></td>
                        <td>
                            <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" DataTextField="DisplayField" Width="300"
                                DataValueField="ValueField" SelectedValue='<%# Bind("ArtistId") %>'>
                            </asp:DropDownList>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBoxI" Width="70" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBoxI" /></td>
                    </tr>
                </InsertItemTemplate>

                <ItemTemplate>
                    <tr style="background-color: #E0FFFF; color: #333333;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton"
                                OnClientClick="return confirm('Are you sure you want to delete this Album?')" />
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" Width="60px" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                            <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" DataTextField="DisplayField" Width="300"
                                DataValueField="ValueField" SelectedValue='<%# Eval("ArtistId") %>' Enabled="false">
                            </asp:DropDownList>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" Width="70" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                    </tr>
                </ItemTemplate>

                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                    <tr runat="server" style="background-color: #87ba80; color: #333333;">
                                        <th runat="server"></th>
                                        <th runat="server">Id</th>
                                        <th runat="server">Title</th>
                                        <th runat="server">Artist</th>
                                        <th runat="server">Year</th>
                                        <th runat="server">Label</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center; background-color: #c0c0c0; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                                <asp:DataPager runat="server" ID="DataPager1">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                        <asp:NumericPagerField ></asp:NumericPagerField>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>

                <SelectedItemTemplate>
                    <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton"
                                OnClientClick="return confirm('Are you sure you want to delete this Album?')" />
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                            <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" DataTextField="DisplayField" Width="300"
                                DataValueField="ValueField" SelectedValue='<%# Eval("ArtistId") %>' Enabled="false">
                            </asp:DropDownList>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                    </tr>
                </SelectedItemTemplate>

            </asp:ListView>
            <asp:ObjectDataSource ID="AlbumListODS" runat="server" DataObjectTypeName="ChinookSystem.ViewModels.AlbumItem"
                SelectMethod="Albums_List"
                InsertMethod="Albums_Add"
                UpdateMethod="Albums_Update"
                DeleteMethod="Albums_Delete"
                OnSelected="SelectCheckForException"
                OnInserted="InsertCheckForException"
                OnUpdated="UpdateCheckForException"
                OnDeleted="DeleteCheckForException"
                OldValuesParameterFormatString="original_{0}"
                TypeName="ChinookSystem.BLL.AlbumController"></asp:ObjectDataSource>
            <%-- ODS to populate the Artists dropdown list --%>
            <asp:ObjectDataSource ID="ArtistListODS" runat="server" OldValuesParameterFormatString="original_{0}" 
                SelectMethod="Artists_DDLList" 
                OnSelected="SelectCheckForException"
                TypeName="ChinookSystem.BLL.ArtistController"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
