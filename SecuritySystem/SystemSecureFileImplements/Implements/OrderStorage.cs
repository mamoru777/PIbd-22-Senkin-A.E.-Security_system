﻿using System;
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
    public class OrderStorage : IOrderStorage
    {
        private readonly FileDataListSingleton source;
        public OrderStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList()
        {
            return source.Orders.Select(CreateModel).ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Orders.Where(rec => rec.SecureId.ToString().Contains(model.SecureId.ToString())) 
           .Select(CreateModel)
           .ToList();
        }
        
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var order = source.Orders
                .FirstOrDefault(rec => rec.SecureId == model.SecureId || rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }
        public void Insert(OrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
            var element = new Order { Id = maxId + 1 };
            source.Orders.Add(CreateModel(model, element));
        }
        public void Update(OrderBindingModel model)
        {
            var element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Orders.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Order CreateModel(OrderBindingModel model, Order order)
        {
            order.SecureId = model.SecureId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }

        private OrderViewModel CreateModel(Order order)
        {
            string dishName = string.Empty;
            foreach (var dish in source.Secures)
            {
                if (dish.Id == order.SecureId)
                {
                    dishName = dish.SecureName;
                }
            }
            return new OrderViewModel
            {
                Id = order.Id,
                SecureId = order.SecureId,
                SecureName = source.Secures.FirstOrDefault(rec => rec.Id == order.SecureId)?.SecureName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status.ToString(),
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}