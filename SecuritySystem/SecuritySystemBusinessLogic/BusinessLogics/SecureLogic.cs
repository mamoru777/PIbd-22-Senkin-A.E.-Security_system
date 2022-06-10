using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.BuisnessLogicsContracts;
using SecuritySystemContracts.Enums;
using SecuritySystemContracts.StoragesContracts;
using SecuritySystemContracts.ViewModels;

namespace SecuritySystemBusinessLogic.BusinessLogics
{
    public class SecureLogic : ISecureLogic
    {
        private readonly ISecureStorage _secureStorage;
        public SecureLogic(ISecureStorage secureStorage)
        {
            _secureStorage = secureStorage;
        }
        public List<SecureViewModel> Read(SecureBindingModel model)
        {
            if (model == null)
            {
                return _secureStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<SecureViewModel> { _secureStorage.GetElement(model) };
            }
            return _secureStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(SecureBindingModel model)
        {
            var element = _secureStorage.GetElement(new SecureBindingModel
            {
                SecureName = model.SecureName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть блюдо с таким названием");
            }
            if (model.Id.HasValue)
            {
                _secureStorage.Update(model);
            }
            else
            {
                _secureStorage.Insert(model);
            }
        }
        public void Delete(SecureBindingModel model)
        {
            var element = _secureStorage.GetElement(new SecureBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Блюдо не найдено");
            }
            _secureStorage.Delete(model);
        }
    }
}
