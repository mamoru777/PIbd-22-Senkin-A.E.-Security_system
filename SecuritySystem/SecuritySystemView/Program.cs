using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecuritySystemContracts.BuisnessLogicsContracts;
using SecuritySystemContracts.StoragesContracts;
using SecuritySystemBusinessLogic.BusinessLogics;
using SecuritySystemDatabaseImplement.Implements;
using SecuritySystemBusinessLogic.OfficePackage;
using SecuritySystemBusinessLogic.OfficePackage.Implements;
using SecuritySystemContracts.BindingModels;
using SecuritySystemBusinessLogic.MailWorker;
using Unity;
using Unity.Lifetime;
using System.Configuration;
using System.Threading;
using SecuritySystemContracts.Attributes;

namespace SecuritySystemView
{
    static class Program
    {
        private static IUnityContainer container = null;
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            var mailSender = Container.Resolve<AbstractMailWorker>();
            mailSender.MailConfig(new MailConfigBindingModel
            {
                MailLogin = "senkinsaha@yandex.ru",//ConfigurationManager.AppSettings["MailLogin"],
                MailPassword = "lcejqnqoowphuxch",//ConfigurationManager.AppSettings["MailPassword"],
                SmtpClientHost = "smtp.yandex.ru",//ConfigurationManager.AppSettings["SmtpClientHost"],
                SmtpClientPort = 587,//Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]),
                PopHost = "pop.yandex.ru",//ConfigurationManager.AppSettings["PopHost"],
                PopPort = 995//Convert.ToInt32(ConfigurationManager.AppSettings["PopPort"])
            });
            var timer = new System.Threading.Timer(new TimerCallback(MailCheck), null, 0, 50000);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<FormMain>());    
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IComponentStorage,
            ComponentStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientStorage, ClientStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISecureStorage, SecureStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IComponentLogic, ComponentLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISecureLogic, SecureLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientLogic, ClientLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportLogic, ReportLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToExcel, SaveToExcel>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToWord, SaveToWord>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToPdf, SaveToPdf>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IImplementerLogic, ImplementerLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IImplementerStorage, ImplementerStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkProcess, WorkModeling>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMessageInfoStorage, MessageInfoStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMessageInfoLogic, MessageInfoLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractMailWorker, MailKitWorker>(new SingletonLifetimeManager());
            currentContainer.RegisterType<IBackUpInfo, BackUpInfo>(new SingletonLifetimeManager());
            currentContainer.RegisterType<IBackUpLogic, BackUpLogic>(new SingletonLifetimeManager());
            return currentContainer;
        }
        private static void MailCheck(object obj) => Container.Resolve<AbstractMailWorker>().MailCheck();
        public static void ConfigGrid<T>(List<T> data, DataGridView grid)
        {
            var type = typeof(T);
            var config = new List<string>();
            grid.Columns.Clear();
            foreach (var prop in type.GetProperties())
            {
                var attributes = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (attributes != null && attributes.Length > 0)
                {
                    foreach (var attr in attributes)
                    {
                        if (attr is ColumnAttribute columnAttr)
                        {
                            config.Add(prop.Name);
                            var column = new DataGridViewTextBoxColumn
                            {
                                Name = prop.Name,
                                ReadOnly = true,
                                HeaderText = columnAttr.Title,
                                Visible = columnAttr.Visible,
                                Width = columnAttr.Width
                            };
                            if (columnAttr.GridViewAutoSize != GridViewAutoSize.None)
                            {
                                column.AutoSizeMode = (DataGridViewAutoSizeColumnMode)Enum
                                    .Parse(typeof(DataGridViewAutoSizeColumnMode), columnAttr.GridViewAutoSize.ToString());
                            }
                            grid.Columns.Add(column);
                        }
                    }
                }
            }
            foreach (var elem in data)
            {
                var objs = new List<object>();
                foreach (var conf in config)
                {
                    var value = elem.GetType().GetProperty(conf).GetValue(elem);
                    objs.Add(value);
                }
                grid.Rows.Add(objs.ToArray());
            }
        }
    }
}
