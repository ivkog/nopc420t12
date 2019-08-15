using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

//*** 2019-08-14-task2 *** 

namespace Nop.Plugin.Widgets.Product_Msg.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }
        
 

        [NopResourceDisplayName("Plugins.Widgets.Product_Msg.Text")]
        public string Text1 { get; set; }
        public bool Text1_OverrideForStore { get; set; }


    }
}
 