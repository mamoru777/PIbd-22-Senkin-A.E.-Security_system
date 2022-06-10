using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.StoragesContracts;
using SecuritySystemContracts.ViewModels;
using SecuritySystemFileImplement.Models;


namespace SecuritySystemFileImplement.Implements
{
    public class SecureStorage : ISecureStorage
    {
        private readonly FileDataListSingleton source;
        public SecureStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<SecureViewModel> GetFullList()
        {
            return source.Secures
            .Select(CreateModel)
            .ToList();
        }
        public List<SecureViewModel> GetFilteredList(SecureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Secures
            .Where(rec => rec.SecureName.Contains(model.SecureName))
            .Select(CreateModel)
            .ToList();
        }
        public SecureViewModel GetElement(SecureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var product = source.Secures
            .FirstOrDefault(rec => rec.SecureName == model.SecureName || rec.Id
           == model.Id);
            return product != null ? CreateModel(product) : null;
        }
        public void Insert(SecureBindingModel model)
        {
            int maxId = source.Secures.Count > 0 ? source.Components.Max(rec => rec.Id)
    : 0;
            var element = new Secure
            {
                Id = maxId + 1,
                SecureComponents = new
           Dictionary<int, int>()
            };
            source.Secures.Add(CreateModel(model, element));
        }
        public void Update(SecureBindingModel model)
        {
            var element = source.Secures.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(SecureBindingModel model)
        {
            Secure element = source.Secures.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Secures.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Secure CreateModel(SecureBindingModel model, Secure product)
        {
            product.SecureName = model.SecureName;
            product.Price = model.Price;
            // удаляем убранные
            foreach (var key in product.SecureComponents.Keys.ToList())
            {
                if (!model.SecureComponents.ContainsKey(key))
                {
                    product.SecureComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.SecureComponents)
            {
                if (product.SecureComponents.ContainsKey(component.Key))
                {
                    product.SecureComponents[component.Key] =
                   model.SecureComponents[component.Key].Item2;
                }
                else
                {
                    product.SecureComponents.Add(component.Key,
                   model.SecureComponents[component.Key].Item2);
                }
            }
            return product;
        }
        private SecureViewModel CreateModel(Secure product)
        {
            return new SecureViewModel
            {
                Id = product.Id,
                SecureName = product.SecureName,
                Price = product.Price,
                SecureComponents = product.SecureComponents
     .ToDictionary(recPC => recPC.Key, recPC =>
     (source.Components.FirstOrDefault(recC => recC.Id ==
    recPC.Key)?.ComponentName, recPC.Value))
            };
        }
    }
}

