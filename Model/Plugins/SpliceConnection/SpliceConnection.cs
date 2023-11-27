using System;
using System.Linq;
using System.Windows.Forms;
using SpliceConn;
using Tekla.Structures.Dialog;


public partial class SpliceConnection : PluginFormBase
{
    public SpliceConnection()
    {
        InitializeComponent();
    }

    protected override string LoadValuesPath(string fileName)
    {
        SetAttributeValue(PlateLengthTextBox, 300.0);
        SetAttributeValue(BoltStandardTextBox, "A325N");
        SetAttributeValue(UpDirectionComboBox, 7);
        SetAttributeValue(ClassTextBox, "99");
        SetAttributeValue(ConnectionCodeTextBox, "0");

        var result = base.LoadValuesPath(fileName);

        Apply();

        return result;
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

        var children = this.FindAllChildrenByType<CheckBox>();

        //MessageBox.Show($@"{children.Count}, {attributeName}");

        var foundCheckBox = children
            .FirstOrDefault(c => attributeName == structuresExtender.GetAttributeName(c));

        //MessageBox.Show($@"{foundCheckBox?.Name}, {attributeName}");

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
