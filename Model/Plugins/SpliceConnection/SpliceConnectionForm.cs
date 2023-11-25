using System;
using System.Windows.Forms;
using Tekla.Structures.Dialog;

namespace SpliceConn
{
    public partial class SpliceConnectionForm : PluginFormBase
    {
        public SpliceConnectionForm()
        {
            InitializeComponent();
        }

        private void okApplyModifyGetOnOffCancel1_ApplyClicked(object sender, EventArgs e)
        {
            this.Apply();
        }

        private void okApplyModifyGetOnOffCancel1_CancelClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okApplyModifyGetOnOffCancel1_GetClicked(object sender, EventArgs e)
        {
            this.Get();
        }

        private void okApplyModifyGetOnOffCancel1_ModifyClicked(object sender, EventArgs e)
        {
            this.Modify();
        }

        private void okApplyModifyGetOnOffCancel1_OkClicked(object sender, EventArgs e)
        {
            this.Apply();
            this.Close();
        }

        private void okApplyModifyGetOnOffCancel1_OnOffClicked(object sender, EventArgs e)
        {
            this.ToggleSelection();
        }

        private Control GetEnableCheckBox(string attributeName, Control parent)
        {
            Control foundCheckBox = null;

            for (var i = 0; i < parent.Controls.Count && foundCheckBox == null; i++)
            {
                var control = parent.Controls[i];

                if (control.GetType() == typeof(CheckBox))
                {
                    var checkBox = control as CheckBox;

                    if (attributeName == structuresExtender.GetAttributeName(checkBox))
                    {
                        foundCheckBox = checkBox;
                    }
                }
                else
                {
                    foundCheckBox = GetEnableCheckBox(attributeName, control);
                }
            }

            return foundCheckBox;
        }

        private void SetThisControlEnableCheckBoxChecked(object sender)
        {
            var thisControl = sender as Control;

            if (thisControl != null)
            {
                var control = GetEnableCheckBox(structuresExtender.GetAttributeName(thisControl), this);
                var enableCheckBox = control as CheckBox;

                if (enableCheckBox != null)
                {
                    enableCheckBox.Checked = true;
                }
            }
        }

        private void anyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetThisControlEnableCheckBoxChecked(sender);
        }

        private void anyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetThisControlEnableCheckBoxChecked(sender);
        }

        // Use the following method if UI contains ImageListComboBox controls

        //private void ImageListComboBoxSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ComboBox comboBox = sender as ComboBox;

        //    if (comboBox != null)
        //    {
        //        ImageListComboBox imageListComboBox = comboBox.Parent as ImageListComboBox;

        //        if (imageListComboBox != null)
        //        {
        //            SetThisControlEnableCheckBoxChecked(imageListComboBox);
        //        }
        //    }
        //}
    }
}
