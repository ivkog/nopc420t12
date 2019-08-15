using System;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.Product_Msg.Infrastructure.Cache;
using Nop.Plugin.Widgets.Product_Msg.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

/*** 2019-08-14-task2 ***/

namespace Nop.Plugin.Widgets.Product_Msg.Components
{
    [ViewComponent(Name = "WidgetsProduct_Msg")]
    public class WidgetsProduct_MsgViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;
        private readonly IWebHelper _webHelper;

        public WidgetsProduct_MsgViewComponent(IStoreContext storeContext, 
            IStaticCacheManager cacheManager, 
            ISettingService settingService, 
            IPictureService pictureService,
            IWebHelper webHelper)
        {
            _storeContext = storeContext;
            _cacheManager = cacheManager;
            _settingService = settingService;
            //*** _pictureService = pictureService;
            _webHelper = webHelper;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var product_MsgSettings = _settingService.LoadSetting<Product_MsgSettings>(_storeContext.CurrentStore.Id);

            var model = new PublicInfoModel
            {
                Text1 = product_MsgSettings.Text1
            };

            if (string.IsNullOrEmpty(model.Text1))
                return Content("");


            return View("~/Plugins/Widgets.Product_Msg/Views/PublicInfo.cshtml", model);
        }

    }
}
