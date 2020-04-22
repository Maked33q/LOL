using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace LOL_Chat
{
    public partial class FormMain : MaterialForm
    {
        private static FormMain _instance;

        public static FormMain Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FormMain();
                    return _instance;
                }
                else
                {
                    return null;
                }
            }
        }
        
        public FormMain()
        {
            InitializeComponent();
            MaterialSkinManager manager = MaterialSkinManager.Instance;
            manager.AddFormToManage(this);
            manager.Theme = MaterialSkinManager.Themes.DARK;
            manager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey700, Primary.Grey800, Accent.LightBlue200, TextShade.WHITE);
        }
    }
}
