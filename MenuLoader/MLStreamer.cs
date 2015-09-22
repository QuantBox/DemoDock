using SmartQuant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuLoader
{
    /// <summary>
    /// 通过Streamer来进行预先加载
    /// </summary>
    /*
     <Streamer>
      <TypeName>MenuLoader.MLStreamer, MenuLoader</TypeName>
    </Streamer>
     */
    public class MLStreamer:ObjectStreamer
    {
        private static MenuLoader menuLoader = new MenuLoader();
        private string _menuPath;
        public MLStreamer():base()
        {
            // 不能与已有的重复
            base.typeId = 255;

            _menuPath = Path.Combine(Installation.ConfigDir.FullName, "menu.json");

            menuLoader.LoadConfig(_menuPath);
            //menuLoader.SaveConfig(_menuPath);

            menuLoader.LoadOnce();
        }
    }
}
