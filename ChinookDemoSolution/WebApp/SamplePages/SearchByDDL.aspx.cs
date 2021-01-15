using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class SearchByDDL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadArtistList();
            }
        }

        protected void LoadArtistList()
        {
            ArtistController sysmgr = new ArtistController();
            List<SelectionList> info = sysmgr.Artists_DDLList();

            // if you didn't have the data sorted via LINQ, you can sort it this way:
            info.Sort((x,y) => x.DisplayField.CompareTo(y.DisplayField));

            ArtistList.DataSource = info;
            ArtistList.DataTextField = nameof(SelectionList.DisplayField);
            ArtistList.DataValueField = nameof(SelectionList.ValueField);
            ArtistList.DataBind();

            // setup of a prompt line
            ArtistList.Items.Insert(0, new ListItem("Select an Artist...","0"));
        }

        protected void SearchAlbums_Click(object sender, EventArgs e)
        {
            if (ArtistList.SelectedIndex == 0)
            {
                // Am I on the first line of the list (prompt)
                MessageLabel.Text = "Please select an Artist";
                ArtistAlbumsGridView.DataSource = null;
                ArtistAlbumsGridView.DataBind();
            }
            else
            {
                AlbumController sysmgr = new AlbumController();
                List<ChinookSystem.ViewModels.ArtistAlbums> info = sysmgr.Albums_GetAlbumsForArtist(int.Parse(ArtistList.SelectedValue));

                ArtistAlbumsGridView.DataSource = info;
                ArtistAlbumsGridView.DataBind();
            }
        }
    }
}