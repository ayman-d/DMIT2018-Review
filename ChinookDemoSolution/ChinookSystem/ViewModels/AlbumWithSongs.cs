using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
    public class AlbumWithSongs
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int TrackCount { get; set; }
        public IEnumerable<SongFromAlbum> Songs { get; set; }
    }
}
