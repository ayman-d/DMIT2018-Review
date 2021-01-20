using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Entities;
using ChinookSystem.DAL;
using ChinookSystem.ViewModels;
using System.ComponentModel; // for ODS
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {

        #region Regular Controller Methods
        // due to the fact that the entities are internal, we cannot use them here and we use ViewModels instead
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ArtistAlbums> Albums_GetArtistAlbums()
        {
            using (var context = new ChinookSystemContext())
            {
                // Link to Entity
                IEnumerable<ArtistAlbums> results = from x in context.Albums
                                                    select new ArtistAlbums
                                                    {
                                                        Title = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistName = x.Artist.Name
                                                    };
                return results.ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ArtistAlbums> Albums_GetAlbumsForArtist(int artistId)
        {
            using (var context = new ChinookSystemContext())
            {
                IEnumerable<ArtistAlbums> results = from x in context.Albums
                                                    where x.Artist.ArtistId == artistId
                                                    select new ArtistAlbums
                                                    {
                                                        Title = x.Title,
                                                        ReleaseYear = x.ReleaseYear,
                                                        ArtistName = x.Artist.Name,
                                                        ArtistId = x.ArtistId
                                                    };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AlbumItem> Albums_List()
        {
            using (var context = new ChinookSystemContext())
            {
                IEnumerable<AlbumItem> results = from x in context.Albums
                                                 select new AlbumItem
                                                 {
                                                     AlbumId = x.AlbumId,
                                                     Title = x.Title,
                                                     ArtistId = x.ArtistId,
                                                     ReleaseYear = x.ReleaseYear,
                                                     ReleaseLabel = x.ReleaseLabel
                                                 };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AlbumItem Albums_FindByID(int albumId)
        {
            using (var context = new ChinookSystemContext())
            {
                AlbumItem results = (from x in context.Albums
                                    where x.AlbumId == albumId
                                    select new AlbumItem
                                    {
                                        AlbumId = x.AlbumId,
                                        Title = x.Title,
                                        ArtistId = x.ArtistId,
                                        ReleaseYear = x.ReleaseYear,
                                        ReleaseLabel = x.ReleaseLabel
                                    }).FirstOrDefault();
                return results;
            }
        }
        #endregion

        // ==============================================================================================

        // Implement CRUD using List View (instead of Grid View)
        #region Add, Update, Delete
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        // we can have it return the int id or void
        public int Albums_Add(AlbumItem item)
        {
            using (var context = new ChinookSystemContext())
            {
                // we need to move the data from the view model class into an entity instance (AlbumItem into Album)
                // this has to be done before staging
                // the PK of the Albums table is identity so it doesn't need to be included when creating a new item
                Album entityItem = new Album
                {
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };

                // staging
                context.Albums.Add(entityItem);
                // commit
                context.SaveChanges();
                // since we are returning int as a return type, we will return the new entity id value
                return entityItem.AlbumId;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        // we can have it return the int id or void
        public void Albums_Update(AlbumItem item)
        {
            using (var context = new ChinookSystemContext())
            {
                // we need to move the data from the view model class into an entity instance (AlbumItem into Album)
                // this has to be done before staging
                // this time we are using an existing record, so we need to map the PK
                Album entityItem = new Album
                {
                    AlbumId = item.AlbumId,
                    Title = item.Title,
                    ArtistId = item.ArtistId,
                    ReleaseYear = item.ReleaseYear,
                    ReleaseLabel = item.ReleaseLabel
                };

                // staging
                context.Entry(entityItem).State = System.Data.Entity.EntityState.Modified;
                // commit
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        // we can have it return the int id or void
        public void Albums_Delete(AlbumItem item)
        {
            // this method will call the actual delete method and pass it the only needed piece of data which is the PK
            Albums_Delete(item.AlbumId);
        }

        public void Albums_Delete(int albumId)
        {
            using (var context = new ChinookSystemContext())
            {
                var exists = context.Albums.Find(albumId);

                context.Albums.Remove(exists);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
