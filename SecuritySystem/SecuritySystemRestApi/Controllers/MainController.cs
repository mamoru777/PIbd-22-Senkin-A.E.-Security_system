using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.BuisnessLogicsContracts;
using SecuritySystemContracts.ViewModels;
using Microsoft.Extensions.Logging;

namespace SecuritySystemRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly ISecureLogic _secure;
        private readonly ILogger<MainController> logger;
        public MainController(IOrderLogic order, ISecureLogic secure)
        {
            _order = order;
            _secure = secure;
        }
        [HttpGet]
        public List<SecureViewModel> GetSecureList() => _secure.Read(null)?.ToList();
        [HttpGet]
        public SecureViewModel GetSecure(int secureId) => _secure.Read(new SecureBindingModel { Id = secureId })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _order.CreateOrder(model);
    }
}
