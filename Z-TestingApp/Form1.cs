using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Z_TestingApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void anyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetThisControlEnableCheckBoxChecked(sender);
        }

        private void SetThisControlEnableCheckBoxChecked(object sender)
        {
            if (!(sender is Control thisControl)) return;

            //var attributeName = structuresExtender.GetAttributeName(thisControl);

            var children = this.FindAllChildrenByType<CheckBox>();

            MessageBox.Show($@"{children.Count}");

            //var foundCheckBox = thisControl
            //    .FindAllChildrenByType<CheckBox>()
            //    .FirstOrDefault(c => attributeName == structuresExtender.GetAttributeName(c));

            //if (foundCheckBox == null) return;

            //foundCheckBox.Checked = true;
        }
    }
}
