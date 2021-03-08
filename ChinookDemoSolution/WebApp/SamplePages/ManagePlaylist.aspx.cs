using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {

            //code to go here
            TracksBy.Value = "Artist";
            // hidden fields need a Value instead of Text property
            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                MessageUserControl.ShowInfo("Artist Search", "No artist name was provided.");
            }
            SearchArg.Value = ArtistName.Text;
            // to cause the ODS to re-execute (refresh the call), you only need to bind the data again with .DataBind()
            // against the display control
            TracksSelectionList.DataBind();
          }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {

                //code to go here

        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {

            //code to go here
            TracksBy.Value = "Genre";
            // this DDL doesn't have a prompt so we don't need to test for empty value
            // this only works if the text is unique, otherwise use SelectedValue
            SearchArg.Value = GenreDDL.SelectedItem.Text;

            // we can also use the selected value string and adjust the BLL to look for the id instead
            //SearchArg.Value = GenreDDL.SelectedValue.ToString();

            TracksSelectionList.DataBind();
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

            //code to go here
            TracksBy.Value = "Album";
            // hidden fields need a Value instead of Text property
            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                MessageUserControl.ShowInfo("Album Search", "No album title was provided.");
            }
            SearchArg.Value = AlbumTitle.Text;
            // to cause the ODS to re-execute (refresh the call), you only need to bind the data again with .DataBind()
            // against the display control
            TracksSelectionList.DataBind();
        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            // temporary user value until security is implemented
            string username = "HansenB";
            //code to go here
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("PlayList Search", "No playlist name was provided.");
            } else
            {
                // use user friendly error handling using MessageUserControl
                // instead of using try/catch
                // MessageUserControl has the try/catch embedded within the control
                // within it, there exists a TryRun() method
                // username is coming from the system via security (later)
                MessageUserControl.TryRun(() => 
                {
                    // code to execute under error handling control of MessageUserControl
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    RefreshPlayList(sysmgr, username);
                },/* success message title and body */"PlayList Search", "View the requested playlist tracks below");
            }
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            //code to go here
            string username = "HansenB";

            //validate playlist exists
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "No playlist name was provided.");
            } else
            {
                // grab a value from the selected ListView row (song)
                // the row is referred to as e.Item
                // to access the column: use the (.FindControl("xxx") as ctrlType).ctrlAccess
                string song = (e.Item.FindControl("NameLabel") as Label).Text;
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username, int.Parse(e.CommandArgument.ToString()), song);
                    RefreshPlayList(sysmgr, username);
                }, "Add track to PlayList", "Track has been added to PlayList");
            }
        }

        protected void RefreshPlayList(PlaylistTracksController sysmgr, string username)
        {
            List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
            PlayList.DataSource = info;
            PlayList.DataBind();
        }

        // ERROR HANDLING
        #region ErrorHandling Methods for ODS
        protected void SelectCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void InsertCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Process Success", "Album has been successfully added.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }

        protected void UpdateCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Process Success", "Album has been successfully updated.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }

        protected void DeleteCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Process Success", "Album has been successfully removed.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        #endregion
    }
}