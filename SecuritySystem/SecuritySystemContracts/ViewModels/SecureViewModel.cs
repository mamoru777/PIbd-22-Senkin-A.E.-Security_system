using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using SecuritySystemContracts.Attributes;

namespace SecuritySystemContracts.ViewModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class SecureViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }
        [Column(title: "Название изделия", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string SecureName { get; set; }
        [Column(title: "Цена", width: 50)]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> SecureComponents { get; set; }
    }
}
