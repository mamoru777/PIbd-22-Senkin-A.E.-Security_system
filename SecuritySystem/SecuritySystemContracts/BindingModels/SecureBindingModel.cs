using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemContracts.BindingModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>

    public class SecureBindingModel
    {
        public int? Id { get; set; }
        public string SecureName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> SecureComponents { get; set; }

    }
}
