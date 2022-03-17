using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.ViewModels;

namespace SecuritySystemContracts.StoragesContracts
{
    public interface ISecureStorage
    {
        List<SecureViewModel> GetFullList();
        List<SecureViewModel> GetFilteredList(SecureBindingModel model);
        SecureViewModel GetElement(SecureBindingModel model);
        void Insert(SecureBindingModel model);
        void Update(SecureBindingModel model);
        void Delete(SecureBindingModel model);
    }
}
