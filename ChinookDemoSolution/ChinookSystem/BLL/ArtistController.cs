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
    public class ArtistController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SelectionList> Artists_DDLList()
        {
            using (var context = new ChinookSystemContext())
            {
                IEnumerable<SelectionList> results = from x in context.Artists
                                                     orderby x.Name
                                                     select new SelectionList
                                                     {
                                                         ValueField = x.ArtistId,
                                                         DisplayField = x.Name
                                                     };
                return results.ToList();
            }
        }

    }
}
