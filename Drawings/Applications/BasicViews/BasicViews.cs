using System;
using System.Windows.Forms;

namespace BasicViews
{
    public partial class BasicViews : Form
    {
        private AppLogic _appLogic = new AppLogic();

        public BasicViews()
        {
            InitializeComponent();
        }

        private void Create_click(object sender, EventArgs e)
        {
            var formData = new FormData
            {
                OpenDrawing = openDrawings.Checked,
                EndView = createEndView.Checked,
                FrontView = createFrontView.Checked,
                TopView = createTopView.Checked,
                RotatedView = create3dView.Checked
            };

            _appLogic.Run(formData);
        }
   }
}