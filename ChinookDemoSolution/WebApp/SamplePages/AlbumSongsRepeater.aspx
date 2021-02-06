<%@ Page Title="Album with related songs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlbumSongsRepeater.aspx.cs" Inherits="WebApp.SamplePages.AlbumSongsRepeater" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Albums with related songs</h1>

    <br />

    <div class="row">
        <div class="offset-2">

            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

        </div>
    </div>

    <br />

    <div class="row">
        <div class="offset-2">

            <asp:Repeater ID="AlbumsWithSongsRepeater" runat="server" DataSourceID="AlbumsWithSongsRepeaterODS"
                ItemType="ChinookSystem.ViewModels.AlbumWithSongs">

                <HeaderTemplate>
                    <h3>Albums with corresponding songs</h3>
                </HeaderTemplate>

                <ItemTemplate>
                    Album: <%# Item.Title %>
                    <br />
                    Artist: <%# Item.Artist %>
                    <br />
                    Number of tracks: <%# Item.TrackCount %>
                    <br />
                    Tracks list:
                    <div class="offset-1">
                        <asp:Repeater ID="SongsRepeater" runat="server" DataSource="<%# Item.Songs %>"
                            ItemType="ChinookSystem.ViewModels.SongFromAlbum">
                            <HeaderTemplate>Track Name And Length In Seconds</HeaderTemplate>
                            <ItemTemplate>
                                <br />
                                <%# Item.SongName %> - <%# Item.LengthInSeconds %>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <p>----------------------------------</p>
                </ItemTemplate>
                
            </asp:Repeater>

        </div>
    </div>

    <%-- ODS Section --%>

    <asp:ObjectDataSource ID="AlbumsWithSongsRepeaterODS" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="Album_GetAlbumWithSongs" TypeName="ChinookSystem.BLL.AlbumController"></asp:ObjectDataSource>

</asp:Content>
