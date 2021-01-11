using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additionl Namespaces
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace ChinookSystem.Entities
{
    [Table("Albums")]
    internal class Album
    {
        private string _ReleaseLabel;
        private int _ReleaseYear;

        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Album Title is required.")]
        [StringLength(160, ErrorMessage = "Album Title cannot be longer than 160 characters.")]
        public string Title { get; set; }

        // Required for int is not always needed since it defaults to 0
        // [Required(ErrorMessage = "Artist Id is required.")]
        public int ArtistId { get; set; }

        // if we want to validate the release year, fully implement it
        public int ReleaseYear 
        { 
            get { return _ReleaseYear; }
            set 
            {
                if (_ReleaseYear < 1950 || _ReleaseYear > DateTime.Today.Year)
                {
                    throw new Exception("Release year must be between 1950 and the current year.");
                } 
                else
                {
                    _ReleaseYear = value;
                }
            }
        }

        [StringLength(50, ErrorMessage = "Release Label cannot be longer than 50 characters.")]
        public string ReleaseLabel 
        {
            get { return _ReleaseLabel; }
            set { _ReleaseLabel = string.IsNullOrEmpty(value) ? null : value; }
        }

        // you can still use [NotMapped] annotations

        // navigational properties (relational table property)
        // many to one relationship (many albums to one artist)

        public virtual Artist Artist { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
