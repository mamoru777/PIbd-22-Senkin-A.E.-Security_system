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
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        [Column(title: "ФИО", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFLM { get; set; }

        [Column(title: "Почта", width: 150)]
        public string Email { get; set; }

        [Column(title: "Пароль", width: 150)]
        public string Password { get; set; }
    }
}
