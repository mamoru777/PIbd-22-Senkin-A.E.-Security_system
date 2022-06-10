using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.StoragesContracts;
using SecuritySystemContracts.ViewModels;
using SecuritySystemDatabaseImplement.Models;

namespace SecuritySystemDatabaseImplement.Implements
{
    public class SecureStorage : ISecureStorage
    {
        public List<SecureViewModel> GetFullList()
        {
            using var context = new SecureSystemDatabase();
            return context.Secures
            .Include(rec => rec.SecureComponents)
            .ThenInclude(rec => rec.Component)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<SecureViewModel> GetFilteredList(SecureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new SecureSystemDatabase();
            return context.Secures
            .Include(rec => rec.SecureComponents)
            .ThenInclude(rec => rec.Component)
            .Where(rec => rec.SecureName.Contains(model.SecureName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public SecureViewModel GetElement(SecureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new SecureSystemDatabase();
            var secure = context.Secures
            .Include(rec => rec.SecureComponents)
            .ThenInclude(rec => rec.Component)
            .FirstOrDefault(rec => rec.SecureName == model.SecureName || rec.Id == model.Id);
            return secure != null ? CreateModel(secure) : null;
        }
        public void Insert(SecureBindingModel model)
        {
            using var context = new SecureSystemDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Secures.Add(CreateModel(model, new Secure(), context));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(SecureBindingModel model)
        {
            using var context = new SecureSystemDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Secures.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(SecureBindingModel model)
        {
            using var context = new SecureSystemDatabase();
            Secure element = context.Secures.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Secures.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Secure CreateModel(SecureBindingModel model, Secure secure,
       SecureSystemDatabase context)
        {
            secure.SecureName = model.SecureName;
            secure.Price = model.Price;
            if (model.Id.HasValue)
            {
                var secureComponents = context.SecureComponents.Where(rec =>
               rec.SecureId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.SecureComponents.RemoveRange(secureComponents.Where(rec =>
               !model.SecureComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in secureComponents)
                {
                    updateComponent.Count =
                   model.SecureComponents[updateComponent.ComponentId].Item2;
                    model.SecureComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.SecureComponents)
            {
                context.SecureComponents.Add(new SecureComponent
                {
                    SecureId = secure.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            return secure;
        }
        private static SecureViewModel CreateModel(Secure secure)
        {
            return new SecureViewModel
            {
                Id = secure.Id,
                SecureName = secure.SecureName,
                Price = secure.Price,
                SecureComponents = secure.SecureComponents
            .ToDictionary(recPC => recPC.ComponentId,
            recPC => (recPC.Component?.ComponentName, recPC.Count))
            };
        }
    }

}

