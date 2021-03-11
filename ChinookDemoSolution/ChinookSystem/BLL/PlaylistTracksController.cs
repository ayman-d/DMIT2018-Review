using System;
using System.Collections.Generic;
using System.Linq;

#region Additional Namespaces
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
using ChinookSystem.DAL;
using System.ComponentModel;
using FreeCode.Exceptions;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        // For error handling on the controller level
        // Add a class level variable holding multiple strings representing
        //      any number of error messages
        List<Exception> brokenRules = new List<Exception>();

        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookSystemContext())
            {

                //code to go here
                var results = context.PlaylistTracks
                                .Where(x => x.Playlist.Name.Equals(playlistname) && x.Playlist.UserName.Equals(username))
                                .OrderBy(x => x.TrackNumber)
                                .Select(x => new UserPlaylistTrack
                                {
                                    TrackID = x.Track.TrackId,
                                    TrackNumber = x.TrackNumber,
                                    TrackName = x.Track.Name,
                                    Milliseconds = x.Track.Milliseconds,
                                    UnitPrice = x.Track.UnitPrice
                                });
                return results.ToList();
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid, string song)
        {
            Playlist playlistExists = null;
            PlaylistTrack playlistTrackExists = null;
            int tracknumber = 0;

            using (var context = new ChinookSystemContext())
            {
                //code to go here
                // This class is in what is called the Business Logic Layer
                // Business Logic is the rules of your business
                //      rule: a track can only exist once in a playlist
                //      rule: each track on a playlist is assigned a continious track number
                // The BLL method should also ensure that data exists for the processing of the transaction
                if (string.IsNullOrEmpty(playlistname))
                {
                    // there is a data error, we need to set up an error message
                    // arg1 is the message, arg2 is the type of the data item name, arg3 is the value we are checking
                    brokenRules.Add(new BusinessRuleException<string>("Playlist name is not provided.", nameof(playlistname), playlistname));
                } 
                else if (string.IsNullOrEmpty(username))
                {
                    // do the same for the username
                    brokenRules.Add(new BusinessRuleException<string>("User name is not provided.", nameof(username), username));
                } 
                else
                {
                    // does the playlist exist?
                    playlistExists = context.Playlists
                                        .Where(x => x.Name.Equals(playlistname))
                                        .Select(x => x).FirstOrDefault();

                    if (playlistExists == null)
                    {
                        // the play list does not exist: 
                        //      create a new instance of the playlist variable
                        //      load the instance with data
                        //      stage the addition of the new instance
                        //      set a variable representing the track number to 1
                        playlistExists = new Playlist
                                        {
                                            Name = playlistname,
                                            UserName = username
                                        };

                        context.Playlists.Add(playlistExists); // staged ONLY!! no new ID
                        tracknumber = 1;
                    } 
                    else
                    {
                        // the play list exists:
                        //      verify track not already in playlist
                        //      find out what is the next track number
                        //      add 1 to the track number
                        playlistTrackExists = context.PlaylistTracks
                                                .Where(x => x.Playlist.Name.Equals(playlistname)
                                                    && x.Playlist.UserName.Equals(username)
                                                    && x.TrackId == trackid)
                                                .Select(x => x).FirstOrDefault();

                        if (playlistTrackExists == null)
                        {
                            tracknumber = context.PlaylistTracks
                                                .Where(x => x.Playlist.Name.Equals(playlistname)
                                                    && x.Playlist.UserName.Equals(username))
                                                .Select(x => x)
                                                .Max(x => x.TrackNumber);
                            tracknumber++;
                        }
                        else
                        {
                            brokenRules.Add(new BusinessRuleException<string>(
                                "Track already exists in this playlist.", 
                                nameof(song), song));
                        }
                    }

                    // create the track
                    playlistTrackExists = new PlaylistTrack();

                    // load of the playlistTrack
                    playlistTrackExists.TrackId = trackid;
                    playlistTrackExists.TrackNumber = tracknumber;

                    // ????
                    // what is the playlist id
                    // if the playlist exists we know the id
                    // if it's a new playlist, we DO NOT know the id since it's only staged
                    // if we access the new playlist record in that case the PKey would be 0 (default)

                    // the solution to BOTH of those scenarios is to use navigational properties during .Add command for the new playlisttrack record
                    // the EntityFramework will ensure that the adding of record to DB will be done in the appropriate order
                    //     AND will add the missing compound PKey value (PlayListID) to the new playlisttrack record
                    // NOT LIKE context.PlayListTracks.Add(playlistTrackExists) --- this is wrong
                    // INSTEAD do the staging using the parent.navProperty.Add(xxx)
                    playlistExists.PlaylistTracks.Add(playlistTrackExists);

                    
                }

                // time to commit to SQL
                // check: are there any errors in this transaction
                // brokenRules is a List<Exception>
                if (brokenRules.Count > 0)
                {
                    // at least one error was recorded during the processing of the transaction
                    throw new BusinessRuleCollectionException("Add Playlist Track Concerns", brokenRules);
                }
                else
                {
                    // COMMIT THE TRANSACTION
                    // send ALL the staged records to SQL for processing
                    context.SaveChanges();
                }
            } // eou
        }//eom

        public void MoveTrack(MoveTrackItem moveTrackItem)
        {
            using (var context = new ChinookSystemContext())
            {
                //code to go here 
                if (string.IsNullOrEmpty(moveTrackItem.PlaylistName))
                {
                    // there is a data error, we need to set up an error message
                    // arg1 is the message, arg2 is the type of the data item name, arg3 is the value we are checking
                    brokenRules.Add(new BusinessRuleException<string>("Playlist name is not provided.", 
                                                                        nameof(MoveTrackItem.PlaylistName), 
                                                                        moveTrackItem.PlaylistName));
                }
                else if (string.IsNullOrEmpty(moveTrackItem.UserName))
                {
                    // do the same for the username
                    brokenRules.Add(new BusinessRuleException<string>("User name is not provided.", 
                                                                        nameof(MoveTrackItem.UserName), 
                                                                        moveTrackItem.UserName));
                }
                else if (moveTrackItem.TrackId <= 0)
                {
                    // do the same for the username
                    brokenRules.Add(new BusinessRuleException<int>("Invalid track identifier was supplied", 
                                                                    nameof(MoveTrackItem.TrackId), 
                                                                    moveTrackItem.TrackId));
                }
                else if (moveTrackItem.TrackNumber <= 0)
                {
                    // do the same for the username
                    brokenRules.Add(new BusinessRuleException<int>("Invalid track identifier was supplied", 
                                                                    nameof(MoveTrackItem.TrackNumber), 
                                                                    moveTrackItem.TrackNumber));
                }

                Playlist playlistExists = context.Playlists
                                                .Where(x => x.Name.Equals(moveTrackItem.PlaylistName) && x.UserName.Equals(moveTrackItem.UserName))
                                                .Select(x => x).FirstOrDefault();

                if (playlistExists == null)
                {
                    brokenRules.Add(new BusinessRuleException<string>("Playlist does not exist", nameof(MoveTrackItem.PlaylistName), moveTrackItem.PlaylistName));
                } else
                {
                    PlaylistTrack trackExists = context.PlaylistTracks
                                                    .Where(x => x.Playlist.Name.Equals(moveTrackItem.PlaylistName)
                                                        && x.Playlist.UserName.Equals(moveTrackItem.UserName)
                                                        && x.TrackId == moveTrackItem.TrackId)
                                                    .Select(x => x).FirstOrDefault();

                    if (trackExists == null)
                    {
                        brokenRules.Add(new BusinessRuleException<string>("Track does not exist on the Playlist. Refresh your playlist display.", 
                                                                            nameof(MoveTrackItem.PlaylistName), 
                                                                            moveTrackItem.PlaylistName));
                    }
                    else
                    {
                        // decide the logic depending on direction
                        if (moveTrackItem.Direction.Equals("up"))
                        {
                            // move up
                            // business process check: already at the top
                            if (trackExists.TrackNumber == 1)
                            {
                                brokenRules.Add(new BusinessRuleException<string>("Track already at the top, refresh your display.",
                                                                            nameof(Track.Name),
                                                                            trackExists.Track.Name));
                            }
                            else
                            {
                                // get the other track
                                PlaylistTrack otherTrack = context.PlaylistTracks
                                                    .Where(x => x.Playlist.Name.Equals(moveTrackItem.PlaylistName)
                                                        && x.Playlist.UserName.Equals(moveTrackItem.UserName)
                                                        && x.TrackNumber == trackExists.TrackNumber - 1)
                                                    .Select(x => x).FirstOrDefault();

                                if (otherTrack == null)
                                {
                                    brokenRules.Add(new BusinessRuleException<string>("Track to swap with doesn't exist, refresh your display.",
                                                                            nameof(MoveTrackItem.PlaylistName),
                                                                            moveTrackItem.PlaylistName));
                                }
                                else
                                {
                                    // change the track numbers
                                    trackExists.TrackNumber--;
                                    otherTrack.TrackNumber++;
                                    // stage
                                    context.Entry(trackExists).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;
                                    context.Entry(otherTrack).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;
                                }
                            }
                        }
                        else
                        {
                            // move down
                            // business process check: already at the bottom
                            if (trackExists.TrackNumber == playlistExists.PlaylistTracks.Count)
                            {
                                brokenRules.Add(new BusinessRuleException<string>("Track already at the bottom, refresh your display.",
                                                                            nameof(Track.Name),
                                                                            trackExists.Track.Name));
                            }
                            else
                            {
                                // get the other track
                                PlaylistTrack otherTrack = context.PlaylistTracks
                                                    .Where(x => x.Playlist.Name.Equals(moveTrackItem.PlaylistName)
                                                        && x.Playlist.UserName.Equals(moveTrackItem.UserName)
                                                        && x.TrackNumber == trackExists.TrackNumber + 1)
                                                    .Select(x => x).FirstOrDefault();

                                if (otherTrack == null)
                                {
                                    brokenRules.Add(new BusinessRuleException<string>("Track to swap with doesn't exist, refresh your display.",
                                                                            nameof(MoveTrackItem.PlaylistName),
                                                                            moveTrackItem.PlaylistName));
                                }
                                else
                                {
                                    // change the track numbers
                                    trackExists.TrackNumber++;
                                    otherTrack.TrackNumber--;
                                    // stage
                                    context.Entry(trackExists).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;
                                    context.Entry(otherTrack).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;
                                }
                            }
                        }   
                    }   
                }
                // final check and commit
                if (brokenRules.Count() > 0)
                {
                    throw new BusinessRuleCollectionException("Track movement concerns:", brokenRules);
                }
                else
                {
                    // commit
                    context.SaveChanges();
                }
            } // eou
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookSystemContext())
            {
                //code to go here
                if (string.IsNullOrEmpty(playlistname))
                {
                    // there is a data error, we need to set up an error message
                    // arg1 is the message, arg2 is the type of the data item name, arg3 is the value we are checking
                    brokenRules.Add(new BusinessRuleException<string>("Playlist name is not provided.", nameof(playlistname), playlistname));
                }
                else if (string.IsNullOrEmpty(username))
                {
                    // do the same for the username
                    brokenRules.Add(new BusinessRuleException<string>("User name is not provided.", nameof(username), username));
                }
                else if (trackstodelete.Count == 0)
                {
                    // do the same for the trackstodelete list count
                    // we can also say BusinessRuleException<int> and make the last argument 0 instead of "0"
                    brokenRules.Add(new BusinessRuleException<string>("You did not select any tracks to delete", "Track Count", "0"));
                } 
                else
                {
                    Playlist playlistExists = context.Playlists
                                                .Where(x => x.Name.Equals(playlistname) && x.UserName.Equals(username))
                                                .Select(x => x).FirstOrDefault();

                    if (playlistExists == null)
                    {
                        brokenRules.Add(new BusinessRuleException<string>("Playlist does not exist", nameof(playlistname), playlistname));
                    }
                    else
                    {
                        // grab a list of tracks that are to be kept
                        var tracksToKeep = context.PlaylistTracks
                                            .Where(x => x.Playlist.Name.Equals(playlistname)
                                                    && x.Playlist.UserName.Equals(username)
                                                    && !trackstodelete.Any(y => y == x.TrackId))
                                            .OrderBy(x => x.TrackNumber)
                                            .Select(x => x);
                        // remove the desired tracks to be deleted
                        PlaylistTrack item = null;
                        foreach (int deleteTrackId in trackstodelete)
                        {
                            item = context.PlaylistTracks
                                    .Where(x => x.Playlist.Name.Equals(playlistname)
                                            && x.Playlist.UserName.Equals(username)
                                            && x.TrackId == deleteTrackId)
                                    .Select(x => x).FirstOrDefault();

                            if (item != null)
                            {
                                // staged
                                // parent.navProperty.action
                                playlistExists.PlaylistTracks.Remove(item);
                            }
                        }
                        // re-sequence the kept tracks
                        // option a) use a list and update the records of the list
                        // option b) delete all children records and re-add only the kept records

                        // using option a
                        int trackNumber = 1;
                        foreach (var track in tracksToKeep)
                        {
                            track.TrackNumber = trackNumber;
                            context.Entry(track).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true; // staged ONLY
                            trackNumber++;
                        }

                        
                    }
                }

                // save the transaction
                if (brokenRules.Count() > 0)
                {
                    throw new BusinessRuleCollectionException("Track removal concerns:", brokenRules);
                }
                else
                {
                    context.SaveChanges();
                }

            } // eou
        }//eom
    }
}
