using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartQuant.Shared;
using System.Reflection;

namespace MenuLoader
{
    /// <summary>
    /// 菜单加载器
    /// 
    /// http://www.jb51.net/article/71104.htm
    /// </summary>
    public class MenuLoader
    {
        private static bool _bMenuAdded;
        private List<string> MenuList;

        public Form GetMainForm()
        {
            // 加载时间如果比较靠后，这里将会正常
            Form mainForm = Global.MainForm;
            if (mainForm != null)
                return mainForm;

            // 这里最好放在线程中循环判断
            if (Application.OpenForms.Count > 0 && Application.OpenForms[0].Name == "MainForm")
                return Application.OpenForms[0];

            return null;
        }

        public void LoadConfig(string path)
        {
            MenuList = new List<string>();
            MenuList = (List<string>)JsonConfig.Load(path, MenuList);
        }

        public void SaveConfig(string path)
        {
            JsonConfig.Save(path, MenuList);
        }

        public void LoadOnce()
        {
            if (!_bMenuAdded)
            {
                try
                {
                    // DOS窗口时没有问题，非DOS下异常，所以可以利用一下
                    Console.Clear();
                }
                catch
                {
                    Console.WriteLine("create custom menu!");
                    // 只有DOS窗口时要注意
                    System.Threading.ThreadPool.QueueUserWorkItem(delegate
                    {
                        Form mainForm = GetMainForm();
                        while (mainForm == null)
                        {
                            System.Threading.Thread.Sleep(5000);
                            mainForm = GetMainForm();
                        }

                        try
                        {
                            mainForm.SafeInvoke(() => { LoadMenu(mainForm); });
                        }
                        catch (Exception e)
                        {
                            // 奇怪，调试的时候总是会出错
                            Console.WriteLine(e);
                        }
                    });
                }
                _bMenuAdded = true;
            }
        }

        public void AddMenuByType(Type type, Form mainForm)
        {
            if (type == null)
            {
                Console.WriteLine("Can not create menu type " + type.ToString());
            }
            else
            {
                MenuPlugin obj = (MenuPlugin)Activator.CreateInstance(type, new object[] { });
                if (obj == null)
                {
                    Console.WriteLine("Can not create menu object " + type.ToString());
                }
                else
                {
                    obj.AddMenu(mainForm);

                    ////需要 using System.Reflection;
                    //var m = type.GetMethod("AddMenu",BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    ////找到具有指定特性的函数，进行调用
                    //m.Invoke(obj, new object[] { mainForm });
                }
            }
        }

        public void AddMenuByString(string str, Form mainForm)
        {
            Type type = Type.GetType(str);
            AddMenuByType(type, mainForm);
        }

        private void LoadMenu(Form mainForm)
        {
            foreach(var menu in MenuList)
            {
                AddMenuByString(menu, mainForm);
            }
        }

        private void AddMenuViewItem(Form mainForm)
        {
            MenuStrip mainMenuStrip = mainForm.MainMenuStrip;
            ToolStripMenuItem menuView = mainMenuStrip.Items[2] as ToolStripMenuItem;
            ToolStripSeparator menu_Sepearator = new ToolStripSeparator();
            menuView.DropDownItems.Add(menu_Sepearator);
            ToolStripMenuItem menuView_Script = new ToolStripMenuItem("Script Explorer");
            menuView.DropDownItems.Add(menuView_Script);

            menuView_Script.Click += menuView_Script_Click;
        }

        private void AddMenuViewItem(Form mainForm,int t)
        {
            MenuStrip mainMenuStrip = mainForm.MainMenuStrip;

            //mainMenuStrip.Items[0]

            ToolStripMenuItem menuView = mainMenuStrip.Items[2] as ToolStripMenuItem;
            ToolStripSeparator menu_Sepearator = new ToolStripSeparator();
            menuView.DropDownItems.Add(menu_Sepearator);
            ToolStripMenuItem menuView_Script = new ToolStripMenuItem("Script Explorer");
            menuView.DropDownItems.Add(menuView_Script);

            menuView_Script.Click += menuView_Script_Click;
        }

        private void menuView_Script_Click(object sender, EventArgs e)
        {
            //Global.DockManager.Open(typeof(DemoTabWindow), new Random().Next());
        }
    }
}
