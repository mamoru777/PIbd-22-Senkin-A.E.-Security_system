using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemContracts.ViewModels
{
    public class ReportSecureComponentViewModel
    {
        public string SecureName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Components { get; set; }
    }
}
