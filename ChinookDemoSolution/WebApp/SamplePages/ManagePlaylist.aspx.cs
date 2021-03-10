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
            // is there any text in the playlist name field
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Track Movement", "You must have a playlist name.");
            }
            else
            {
                // is a playlist already fetched
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Movement", "You must have a playlist showing.");
                }
                else
                {
                    // was anything actually selected
                    CheckBox songSelected = null;
                    int rowsSelected = 0;
                    MoveTrackItem moveTrackItem = new MoveTrackItem();

                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        // point to the checkbox control on the gridview row
                        songSelected = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                        // test the setting of the checkbox
                        if (songSelected.Checked)
                        {
                            rowsSelected++;
                            moveTrackItem.TrackId = int.Parse((PlayList.Rows[i].FindControl("TrackId") as Label).Text);
                            moveTrackItem.TrackNumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        }
                    }
                    // processing rule (only one track can be selected for this operation)

                    switch (rowsSelected)
                    {
                        case 0:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "No song selected, you must select a single song to move");
                                break;
                            }
                        case 1:
                            {
                                // processing rule: is it the BOTTOM song (in which case we can't process it)
                                if (moveTrackItem.TrackNumber == PlayList.Rows.Count)
                                {
                                    MessageUserControl.ShowInfo("Track Movement", "Selected song is last on the list, no movement necessary.");
                                }
                                else
                                {
                                    // move the track
                                    moveTrackItem.Direction = "down";
                                    MoveTrack(moveTrackItem);
                                }
                                break;
                            }
                        default:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "You can only select a single song to move.");
                                break;
                            }
                    }
                }
            }
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
            //code to go here
            // is there any text in the playlist name field
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Track Movement", "You must have a playlist name.");
            }
            else
            {
                // is a playlist already fetched
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Movement", "You must have a playlist showing.");
                }
                else
                {
                    // was anything actually selected
                    CheckBox songSelected = null;
                    int rowsSelected = 0;
                    MoveTrackItem moveTrackItem = new MoveTrackItem();

                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        // point to the checkbox control on the gridview row
                        songSelected = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                        // test the setting of the checkbox
                        if (songSelected.Checked)
                        {
                            rowsSelected++;
                            moveTrackItem.TrackId = int.Parse((PlayList.Rows[i].FindControl("TrackId") as Label).Text);
                            moveTrackItem.TrackNumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        }
                    }
                    // processing rule (only one track can be selected for this operation)

                    switch (rowsSelected)
                    {
                        case 0:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "No song selected, you must select a single song to move");
                                break;
                            }
                        case 1:
                            {
                                // processing rule: is it the TOP song (in which case we can't process it)
                                if (moveTrackItem.TrackNumber == 1)
                                {
                                    MessageUserControl.ShowInfo("Track Movement", "Selected song is first on the list, no movement necessary.");
                                }
                                else
                                {
                                    // move the track
                                    moveTrackItem.Direction = "up";
                                    MoveTrack(moveTrackItem);
                                }
                                break;
                            }
                        default:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "You can only select a single song to move.");
                                break;
                            }
                    }
                }
            }
        }

        protected void MoveTrack(MoveTrackItem moveTrackItem)
        {
            string username = "HansenB";
            moveTrackItem.PlaylistName = PlaylistName.Text;
            moveTrackItem.UserName = username;
            //call BLL to move track
            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack(moveTrackItem);
                RefreshPlayList(sysmgr, username);
            }, "Track Removal", "Selected track have been moved.");
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            string username = "HansenB";
            //code to go here
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Track Removal", "You must have a playlist name.");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Removal", "You must have a playlist visible to choose removals. Select from the displayed playlist");
                }
                else
                {
                    // collect the tracks indicated on the playlist for removal
                    List<int> trackIds = new List<int>();
                    int rowsSelected = 0;
                    CheckBox tracksSelection = null;
                    // traverse the gridview control PlayList
                    // can do the same with forEach
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        // point to the checkbox control on the gridview row
                        tracksSelection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                        // test the setting of the checkbox
                        if (tracksSelection.Checked)
                        {
                            rowsSelected++;
                            trackIds.Add(int.Parse((PlayList.Rows[i].FindControl("TrackId") as Label).Text));
                        }
                    }

                    // was a song selected
                    if (rowsSelected == 0)
                    {
                        MessageUserControl.ShowInfo("Track Removal", "You must select at least 1 song to remove.");
                    }
                    else
                    {
                        MessageUserControl.TryRun(() =>
                        {
                            PlaylistTracksController sysmgr = new PlaylistTracksController();
                            sysmgr.DeleteTracks(username, PlaylistName.Text, trackIds);
                            RefreshPlayList(sysmgr, username);
                        },"Track Removal", "Selected track(s) have been removed from the playlist.");
                    }
                }
            }
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