using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.ViewModels;

namespace SecuritySystemContracts.BuisnessLogicsContracts
{
    public interface ISecureLogic
    {
        List<SecureViewModel> Read(SecureBindingModel model);
        void CreateOrUpdate(SecureBindingModel model);
        void Delete(SecureBindingModel model);

    }
}
