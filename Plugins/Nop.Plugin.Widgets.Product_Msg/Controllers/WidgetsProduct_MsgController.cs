using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.Product_Msg.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

/*** 2019-08-09-task2 ***/

namespace Nop.Plugin.Widgets.Product_Msg.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsProduct_MsgController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        public WidgetsProduct_MsgController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService, 
            IPictureService pictureService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var product_MsgSettings = _settingService.LoadSetting<Product_MsgSettings>(storeScope);
            var model = new ConfigurationModel
            {
                Text1 = product_MsgSettings.Text1,
                ActiveStoreScopeConfiguration = storeScope
            };


            if (storeScope > 0)
            {
                model.Text1_OverrideForStore = _settingService.SettingExists(product_MsgSettings, x => x.Text1, storeScope);
            }

            return View("~/Plugins/Widgets.Product_Msg/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var product_MsgSettings = _settingService.LoadSetting<Product_MsgSettings>(storeScope);
            product_MsgSettings.Text1 = model.Text1;

            _settingService.SaveSettingOverridablePerStore(product_MsgSettings, x => x.Text1, model.Text1_OverrideForStore, storeScope, false);

            //now clear settings cache
            _settingService.ClearCache();
            
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}
 
 