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
    public class ClientController : ControllerBase
    {
        private readonly IClientLogic _logic;
        private readonly IMessageInfoLogic logicMI;
        private readonly ILogger<MainController> logger;
        public ClientController(IClientLogic logic, IMessageInfoLogic _logicMI)
        {
            _logic = logic;
            logicMI = _logicMI;
        }
        [HttpGet]
        public ClientViewModel Login(string login, string password)
        {
            var list = _logic.Read(new ClientBindingModel
            {
                Email = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }
        [HttpPost]
        public void Register(ClientBindingModel model) =>
        _logic.CreateOrUpdate(model);
        [HttpPost]
        public void UpdateData(ClientBindingModel model) =>
        _logic.CreateOrUpdate(model);
        [HttpGet]
        public List<MessageInfoViewModel> GetMessageInfos(int clientId) =>
            logicMI.Read(new MessageInfoBindingModel { ClientId = clientId });
    }
}
