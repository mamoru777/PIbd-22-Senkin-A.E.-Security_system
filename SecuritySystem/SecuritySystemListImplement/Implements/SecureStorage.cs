﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.StoragesContracts;
using SecuritySystemContracts.ViewModels;
using SecuritySystemListImplement.Models;


namespace SecuritySystemListImplement.Implements
{
    public class SecureStorage : ISecureStorage
    {
        private readonly DataListSingleton source;
        public SecureStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SecureViewModel> GetFullList()
        {
            var result = new List<SecureViewModel>();
            foreach (var component in source.Secures)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<SecureViewModel> GetFilteredList(SecureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<SecureViewModel>();
            foreach (var secure in source.Secures)
            {
                if (secure.SecureName.Contains(model.SecureName))
                {
                    result.Add(CreateModel(secure));
                }
            }
            return result;
        }
        public SecureViewModel GetElement(SecureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var secure in source.Secures)
            {
                if (secure.Id == model.Id || secure.SecureName ==
                model.SecureName)
                {
                    return CreateModel(secure);
                }
            }
            return null;
        }
        public void Insert(SecureBindingModel model)
        {
            var tempSecure = new Secure
            {
                Id = 1,
                SecureComponents = new
            Dictionary<int, int>()
            };
            foreach (var secure in source.Secures)
            {
                if (secure.Id >= tempSecure.Id)
                {
                    tempSecure.Id = secure.Id + 1;
                }
            }
            source.Secures.Add(CreateModel(model, tempSecure));
        }
        public void Update(SecureBindingModel model)
        {
            Secure tempSecure = null;
            foreach (var secure in source.Secures)
            {
                if (secure.Id == model.Id)
                {
                    tempSecure = secure;
                }
            }
            if (tempSecure == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempSecure);
        }
        public void Delete(SecureBindingModel model)
        {
            for (int i = 0; i < source.Secures.Count; ++i)
            {
                if (source.Secures[i].Id == model.Id)
                {
                    source.Secures.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private static Secure CreateModel(SecureBindingModel model, Secure
        secure)
        {
            secure.SecureName = model.SecureName;
            secure.Price = model.Price;
            // удаляем убранные
            foreach (var key in secure.SecureComponents.Keys.ToList())
            {
                if (!model.SecureComponents.ContainsKey(key))
                {
                    secure.SecureComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.SecureComponents)
            {
                if (secure.SecureComponents.ContainsKey(component.Key))
                {
                    secure.SecureComponents[component.Key] =
                    model.SecureComponents[component.Key].Item2;
                }
                else
                {
                    secure.SecureComponents.Add(component.Key,
                    model.SecureComponents[component.Key].Item2);
                }
            }
            return secure;
        }
        private SecureViewModel CreateModel(Secure secure)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
            var secureComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in secure.SecureComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }

                }
                secureComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new SecureViewModel
            {
                Id = secure.Id,
                SecureName = secure.SecureName,
                Price = secure.Price,
                SecureComponents = secureComponents
            };
        }
    }
}
