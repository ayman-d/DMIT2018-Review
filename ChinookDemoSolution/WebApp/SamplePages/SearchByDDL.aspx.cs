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
            // use the UserControlMessage instead of try-catch to manage error handling for the app 
            // when it leaves the web page and accesses the class library for data
            // optionally after the closing } you can put a title and success message
            MessageUserControl.TryRun(() =>
            {
                // what does inside the function is all of operation we want to usually try-catch
                ArtistController sysmgr = new ArtistController();
                List<SelectionList> info = sysmgr.Artists_DDLList();

                // if you didn't have the data sorted via LINQ, you can sort it this way:
                info.Sort((x, y) => x.DisplayField.CompareTo(y.DisplayField));

                ArtistList.DataSource = info;
                ArtistList.DataTextField = nameof(SelectionList.DisplayField);
                ArtistList.DataValueField = nameof(SelectionList.ValueField);
                ArtistList.DataBind();

                // setup of a prompt line
                ArtistList.Items.Insert(0, new ListItem("Select an Artist...", "0"));
            }, "Success Message", "The success title and body messages are optional");
        }

        // use the MessageUserControl to check for exceptions in the ODS
        #region ErrorHandling Methods for ODS
        protected void SelectCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }
        #endregion

        protected void SearchAlbums_Click(object sender, EventArgs e)
        {

            if (ArtistList.SelectedIndex == 0)
            {
                MessageUserControl.TryRun(() =>
                {
                    // Am I on the first line of the list (prompt)
                    MessageUserControl.ShowInfo("Search Selection Concern", "Please select an artist for the search");
                    ArtistAlbumsGridView.DataSource = null;
                    ArtistAlbumsGridView.DataBind();
                }, "Title Message", "Success Message if no selection made.");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    AlbumController sysmgr = new AlbumController();
                    List<ChinookSystem.ViewModels.ArtistAlbums> info = sysmgr.Albums_GetAlbumsForArtist(int.Parse(ArtistList.SelectedValue));

                    // testing if abort had happened
                    // throw new Exception("This is a test to see an abort from the web page code");

                    ArtistAlbumsGridView.DataSource = info;
                    ArtistAlbumsGridView.DataBind();
                }, "Title Message", "Success Message if selection made.");
            }
        }
    }
}