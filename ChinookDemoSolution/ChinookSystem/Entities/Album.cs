using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ChinookSystem.Entities
{
    internal partial class Album
    {
        private string _ReleaseLabel;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            Tracks = new HashSet<Track>();
        }

        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Album title is required.")]
        [StringLength(160, ErrorMessage = "Album title cannot be longer than 160 characters.")]
        public string Title { get; set; }

        public int ArtistId { get; set; }

        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "Release label cannot be longer than 50 characters.")]
        public string ReleaseLabel 
        { 
            get { return _ReleaseLabel; }
            set { _ReleaseLabel = string.IsNullOrEmpty(value) ? null : value; }
        }

        public virtual Artist Artist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
