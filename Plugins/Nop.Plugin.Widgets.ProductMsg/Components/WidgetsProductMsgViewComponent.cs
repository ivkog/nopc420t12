using System;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.ProductMsg.Infrastructure.Cache;
using Nop.Plugin.Widgets.ProductMsg.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.ProductMsg.Components
{
    [ViewComponent(Name = "WidgetsProductMsg")]
    public class WidgetsProductMsgViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;
        private readonly IWebHelper _webHelper;

        public WidgetsProductMsgViewComponent(IStoreContext storeContext, 
            IStaticCacheManager cacheManager, 
            ISettingService settingService, 
            IPictureService pictureService,
            IWebHelper webHelper)
        {
            _storeContext = storeContext;
            _cacheManager = cacheManager;
            _settingService = settingService;
            _pictureService = pictureService;
            _webHelper = webHelper;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var ProductMsgSettings = _settingService.LoadSetting<ProductMsgSettings>(_storeContext.CurrentStore.Id);

            var model = new PublicInfoModel
            {
                Picture1Url = GetPictureUrl(ProductMsgSettings.Picture1Id),
                Text1 = ProductMsgSettings.Text1,
                Link1 = ProductMsgSettings.Link1,
                AltText1 = ProductMsgSettings.AltText1,

                Picture2Url = GetPictureUrl(ProductMsgSettings.Picture2Id),
                Text2 = ProductMsgSettings.Text2,
                Link2 = ProductMsgSettings.Link2,
                AltText2 = ProductMsgSettings.AltText2,

                Picture3Url = GetPictureUrl(ProductMsgSettings.Picture3Id),
                Text3 = ProductMsgSettings.Text3,
                Link3 = ProductMsgSettings.Link3,
                AltText3 = ProductMsgSettings.AltText3,

                Picture4Url = GetPictureUrl(ProductMsgSettings.Picture4Id),
                Text4 = ProductMsgSettings.Text4,
                Link4 = ProductMsgSettings.Link4,
                AltText4 = ProductMsgSettings.AltText4,

                Picture5Url = GetPictureUrl(ProductMsgSettings.Picture5Id),
                Text5 = ProductMsgSettings.Text5,
                Link5 = ProductMsgSettings.Link5,
                AltText5 = ProductMsgSettings.AltText5
            };

            if (string.IsNullOrEmpty(model.Picture1Url) && string.IsNullOrEmpty(model.Picture2Url) &&
                string.IsNullOrEmpty(model.Picture3Url) && string.IsNullOrEmpty(model.Picture4Url) &&
                string.IsNullOrEmpty(model.Picture5Url))
                //no pictures uploaded
                return Content("");

            return View("~/Plugins/Widgets.ProductMsg/Views/PublicInfo.cshtml", model);
        }

        protected string GetPictureUrl(int pictureId)
        {
            var cacheKey = string.Format(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY, 
                pictureId, _webHelper.IsCurrentConnectionSecured() ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);

            return _cacheManager.Get(cacheKey, () =>
            {
                //little hack here. nulls aren't cacheable so set it to ""
                var url = _pictureService.GetPictureUrl(pictureId, showDefaultPicture: false) ?? "";
                return url;
            });
        }
    }
}
