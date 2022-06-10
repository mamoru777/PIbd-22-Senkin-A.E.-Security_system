using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.ViewModels;


namespace SecuritySystemContracts.BuisnessLogicsContracts
{
    public interface IMessageInfoLogic
    {
        public List<MessageInfoViewModel> Read(MessageInfoBindingModel model);
        public void CreateOrUpdate(MessageInfoBindingModel model);
    }
}
