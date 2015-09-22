using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartQuant;

namespace MenuLoader
{
    /// <summary>
    /// 通过Provider来进行预先加载
    /// </summary>
    /*
     <Provider><TypeName>MenuLoader.MLProvider, MenuLoader</TypeName><X64>false</X64></Provider>
     */
    public class MLProvider : Provider, IProvider
    {
        private static MenuLoader menuLoader = new MenuLoader();

        private string _menuPath;
        public MLProvider(Framework framework)
            : base(framework)
        {
            // 不能与已有的重复
            base.id = 55;

            base.name = "MenuLoader";
            base.description = "QuantBox Menu Loader";
            base.url = "www.quantbox.cn";

            _menuPath = Path.Combine(Installation.ConfigDir.FullName, "menu.json");

            menuLoader.LoadConfig(_menuPath);
            menuLoader.LoadOnce();
        }
    }
}
