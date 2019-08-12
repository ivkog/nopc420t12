using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.ProductMsg.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.ProductMsg.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsProductMsgController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        public WidgetsProductMsgController(ILocalizationService localizationService,
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
            var ProductMsgSettings = _settingService.LoadSetting<ProductMsgSettings>(storeScope);
            var model = new ConfigurationModel
            {
                Picture1Id = ProductMsgSettings.Picture1Id,
                Text1 = ProductMsgSettings.Text1,
                Link1 = ProductMsgSettings.Link1,
                AltText1 = ProductMsgSettings.AltText1,
                Picture2Id = ProductMsgSettings.Picture2Id,
                Text2 = ProductMsgSettings.Text2,
                Link2 = ProductMsgSettings.Link2,
                AltText2 = ProductMsgSettings.AltText2,
                Picture3Id = ProductMsgSettings.Picture3Id,
                Text3 = ProductMsgSettings.Text3,
                Link3 = ProductMsgSettings.Link3,
                AltText3 = ProductMsgSettings.AltText3,
                Picture4Id = ProductMsgSettings.Picture4Id,
                Text4 = ProductMsgSettings.Text4,
                Link4 = ProductMsgSettings.Link4,
                AltText4 = ProductMsgSettings.AltText4,
                Picture5Id = ProductMsgSettings.Picture5Id,
                Text5 = ProductMsgSettings.Text5,
                Link5 = ProductMsgSettings.Link5,
                AltText5 = ProductMsgSettings.AltText5,
                ActiveStoreScopeConfiguration = storeScope
            };

            if (storeScope > 0)
            {
                model.Picture1Id_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Picture1Id, storeScope);
                model.Text1_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Text1, storeScope);
                model.Link1_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Link1, storeScope);
                model.AltText1_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.AltText1, storeScope);
                model.Picture2Id_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Picture2Id, storeScope);
                model.Text2_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Text2, storeScope);
                model.Link2_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Link2, storeScope);
                model.AltText2_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.AltText2, storeScope);
                model.Picture3Id_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Picture3Id, storeScope);
                model.Text3_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Text3, storeScope);
                model.Link3_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Link3, storeScope);
                model.AltText3_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.AltText3, storeScope);
                model.Picture4Id_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Picture4Id, storeScope);
                model.Text4_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Text4, storeScope);
                model.Link4_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Link4, storeScope);
                model.AltText4_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.AltText4, storeScope);
                model.Picture5Id_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Picture5Id, storeScope);
                model.Text5_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Text5, storeScope);
                model.Link5_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.Link5, storeScope);
                model.AltText5_OverrideForStore = _settingService.SettingExists(ProductMsgSettings, x => x.AltText5, storeScope);
            }

            return View("~/Plugins/Widgets.ProductMsg/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var ProductMsgSettings = _settingService.LoadSetting<ProductMsgSettings>(storeScope);

            //get previous picture identifiers
            var previousPictureIds = new[] 
            {
                ProductMsgSettings.Picture1Id,
                ProductMsgSettings.Picture2Id,
                ProductMsgSettings.Picture3Id,
                ProductMsgSettings.Picture4Id,
                ProductMsgSettings.Picture5Id
            };

            ProductMsgSettings.Picture1Id = model.Picture1Id;
            ProductMsgSettings.Text1 = model.Text1;
            ProductMsgSettings.Link1 = model.Link1;
            ProductMsgSettings.AltText1 = model.AltText1;
            ProductMsgSettings.Picture2Id = model.Picture2Id;
            ProductMsgSettings.Text2 = model.Text2;
            ProductMsgSettings.Link2 = model.Link2;
            ProductMsgSettings.AltText2 = model.AltText2;
            ProductMsgSettings.Picture3Id = model.Picture3Id;
            ProductMsgSettings.Text3 = model.Text3;
            ProductMsgSettings.Link3 = model.Link3;
            ProductMsgSettings.AltText3 = model.AltText3;
            ProductMsgSettings.Picture4Id = model.Picture4Id;
            ProductMsgSettings.Text4 = model.Text4;
            ProductMsgSettings.Link4 = model.Link4;
            ProductMsgSettings.AltText4 = model.AltText4;
            ProductMsgSettings.Picture5Id = model.Picture5Id;
            ProductMsgSettings.Text5 = model.Text5;
            ProductMsgSettings.Link5 = model.Link5;
            ProductMsgSettings.AltText5 = model.AltText5;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Picture1Id, model.Picture1Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Text1, model.Text1_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Link1, model.Link1_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.AltText1, model.AltText1_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Picture2Id, model.Picture2Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Text2, model.Text2_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Link2, model.Link2_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.AltText2, model.AltText2_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Picture3Id, model.Picture3Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Text3, model.Text3_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Link3, model.Link3_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.AltText3, model.AltText3_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Picture4Id, model.Picture4Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Text4, model.Text4_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Link4, model.Link4_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.AltText4, model.AltText4_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Picture5Id, model.Picture5Id_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Text5, model.Text5_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.Link5, model.Link5_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(ProductMsgSettings, x => x.AltText5, model.AltText5_OverrideForStore, storeScope, false);

            //now clear settings cache
            _settingService.ClearCache();
            
            //get current picture identifiers
            var currentPictureIds = new[]
            {
                ProductMsgSettings.Picture1Id,
                ProductMsgSettings.Picture2Id,
                ProductMsgSettings.Picture3Id,
                ProductMsgSettings.Picture4Id,
                ProductMsgSettings.Picture5Id
            };

            //delete an old picture (if deleted or updated)
            foreach (var pictureId in previousPictureIds.Except(currentPictureIds))
            { 
                var previousPicture = _pictureService.GetPictureById(pictureId);
                if (previousPicture != null)
                    _pictureService.DeletePicture(previousPicture);
            }

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}