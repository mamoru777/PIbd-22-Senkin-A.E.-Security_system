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
using SecuritySystemBusinessLogic.MailWorker;

namespace SecuritySystemBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        private readonly IClientStorage clientStorage;
        private readonly AbstractMailWorker abstractMailWorker;
        public OrderLogic(IOrderStorage orderStorage, IClientStorage _clientStorage, AbstractMailWorker _abstractMailWorker)
        {
            _orderStorage = orderStorage;
            clientStorage = _clientStorage;
            abstractMailWorker = _abstractMailWorker;
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                
                SecureId = model.SecureId,
                ClientId = model.ClientId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
            abstractMailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = clientStorage.GetElement(new ClientBindingModel
                {
                    Id = model.ClientId
                })?.Email,
                Subject = $"Создан новый заказ",
                Text = $"Заказ от {DateTime.Now} на сумму {model.Sum:N2} принят."
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (!order.Status.Equals("Принят"))
            {
                throw new Exception("Заказ не находится в статусе \"Принят\" ");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                SecureId = order.SecureId,
                ImplementerId = model.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                ClientId = order.ClientId,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Выполняется
            });
            abstractMailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = clientStorage.GetElement(new ClientBindingModel
                {
                    Id = order.ClientId
                })?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} передан в работу."
            });
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (!order.Status.Equals("Выполняется"))
            {
                throw new Exception("Заказ не находится в статусе \"Выполняется\" ");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                ClientId = order.ClientId,
                ImplementerId = order.ImplementerId,
                Id = order.Id,
                SecureId = order.SecureId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Готов
            });
            abstractMailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = clientStorage.GetElement(new ClientBindingModel
                {
                    Id = order.ClientId
                })?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} выполнен."
            });
        }
        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (!order.Status.Equals("Готов"))
            {
                throw new Exception("Заказ не находится в статусе \"Готов\" ");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                SecureId = order.SecureId,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Выдан
            });
            abstractMailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = clientStorage.GetElement(new ClientBindingModel
                {
                    Id = order.ClientId
                })?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} выдан."
            });
        }
    }
}
