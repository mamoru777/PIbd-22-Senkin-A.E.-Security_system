using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.ViewModels;

namespace SecuritySystemContracts.StoragesContracts
{
    public interface IMessageInfoStorage
    {
        public List<MessageInfoViewModel> GetFullList();
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model);
        public void Insert(MessageInfoBindingModel model);
    }
}
