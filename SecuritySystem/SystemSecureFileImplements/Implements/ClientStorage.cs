﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.StoragesContracts;
using SecuritySystemContracts.ViewModels;
using SecuritySystemFileImplements.Models;
using SecuritySystemFileImplement;

namespace SecuritySystemFileImplements.Implements
{
    public class ClientStorage : IClientStorage
    {
        private readonly FileDataListSingleton source;
        public ClientStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<ClientViewModel> GetFullList()
        {
            return source.Clients
            .Select(CreateModel)
            .ToList();
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Clients
            .Where(rec => rec.Email.Equals(model.Email) && rec.Password.Equals(model.Password))
            .Select(CreateModel)
            .ToList();
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var client = source.Clients
            .FirstOrDefault(rec => rec.Email == model.Email || rec.Id == model.Id);
            return client != null ? CreateModel(client) : null;
        }

        public void Insert(ClientBindingModel model)
        {
            int maxId = source.Clients.Count > 0 ? source.Clients.Max(rec => rec.Id) : 0;
            var element = new Client { Id = maxId + 1 };
            source.Clients.Add(CreateModel(model, element));
        }

        public void Update(ClientBindingModel model)
        {
            var element = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        public void Delete(ClientBindingModel model)
        {
            Client element = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Clients.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Client CreateModel(ClientBindingModel model, Client client)
        {
            client.ClientFLM = model.ClientFLM;
            client.Email = model.Email;
            client.Password = model.Password;
            return client;
        }

        private ClientViewModel CreateModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                ClientFLM = client.ClientFLM,
                Email = client.Email,
                Password = client.Password
            };
        }
    }
}