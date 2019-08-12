using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

using System;

namespace Nop.Plugin.Widgets.ProductMsg
{
    public class ProductMsgPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly INopFileProvider _fileProvider;

        public ProductMsgPlugin(ILocalizationService localizationService,
            IPictureService pictureService,
            ISettingService settingService,
            IWebHelper webHelper,
            INopFileProvider fileProvider)
        {
            _localizationService = localizationService;
            _pictureService = pictureService;
            _settingService = settingService;
            _webHelper = webHelper;
            _fileProvider = fileProvider;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            //*** 2019-08-06 *** return new List<string> { PublicWidgetZones.HomepageTop };
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsProductMsg/Configure";
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsProductMsg";
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //pictures
            var sampleImagesPath = _fileProvider.MapPath("~/Plugins/Widgets.ProductMsg/Content/productmsg/sample-images/");

            //settings
            var settings = new ProductMsgSettings
            {
                Picture1Id = _pictureService.InsertPicture(_fileProvider.ReadAllBytes(_fileProvider.Combine(sampleImagesPath, "banner1.jpg")), MimeTypes.ImagePJpeg, "banner_1").Id,
                Text1 = "",
                Link1 = _webHelper.GetStoreLocation(false),
                Picture2Id = _pictureService.InsertPicture(_fileProvider.ReadAllBytes(_fileProvider.Combine(sampleImagesPath, "banner2.jpg")), MimeTypes.ImagePJpeg, "banner_2").Id,
                Text2 = "",
                Link2 = _webHelper.GetStoreLocation(false)
                //Picture3Id = _pictureService.InsertPicture(File.ReadAllBytes(_fileProvider.Combine(sampleImagesPath,"banner3.jpg")), MimeTypes.ImagePJpeg, "banner_3").Id,
                //Text3 = "",
                //Link3 = _webHelper.GetStoreLocation(false),
            };
            _settingService.SaveSetting(settings);


            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture1", "Picture 1");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture2", "Picture 2");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture3", "Picture 3");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture4", "Picture 4");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture5", "Picture 5");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture", "Picture");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture.Hint", "Upload picture.");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.Text", "Comment");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.Text.Hint", "Enter comment for picture. Leave empty if you don't want to display any text.");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.Link", "URL");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.Link.Hint", "Enter URL. Leave empty if you don't want this picture to be clickable.");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.AltText", "Image alternate text");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMsg.AltText.Hint", "Enter alternate text that will be added to image.");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<ProductMsgSettings>();

            //locales
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture1");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture2");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture3");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture4");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture5");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.Picture.Hint");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.Text");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.Text.Hint");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.Link");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.Link.Hint");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.AltText");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ProductMsg.AltText.Hint");

            base.Uninstall();
        }

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;
    }
}
