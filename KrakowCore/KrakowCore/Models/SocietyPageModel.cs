using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrakowCore.Models
{
    public class SocietyPageModel
    {
       public string Checker { get; set; }

        public IList<IList<Object>> SheetValues { get; set; }
    }
}
