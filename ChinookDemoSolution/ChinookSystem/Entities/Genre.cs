﻿using System;
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
    [Table("Genres")]
    internal class Genre
    {
        private string _Name;

        [Key]
        public int GenreId { get; set; }

        [StringLength(120, ErrorMessage = "Genre Name cannot be longer than 120 characters.")]
        public string Name 
        { 
            get { return _Name; }
            set { _Name = string.IsNullOrEmpty(value) ? null : value; }
        }

        // navigational properties

        // one to many
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
