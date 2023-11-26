using System;
using System.Linq;
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

        private void anyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetThisControlEnableCheckBoxChecked(sender);
        }

        private void anyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetThisControlEnableCheckBoxChecked(sender);
        }

        private void SetThisControlEnableCheckBoxChecked(object sender)
        {
            if (!(sender is Control thisControl)) return;

            var attributeName = structuresExtender.GetAttributeName(thisControl);

            var foundCheckBox = thisControl
                .FindAllChildrenByType<CheckBox>()
                .FirstOrDefault(c => attributeName == structuresExtender.GetAttributeName(c));

            if (foundCheckBox == null) return;

            foundCheckBox.Checked = true;
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
