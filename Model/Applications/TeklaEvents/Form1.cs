using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tekla.Structures.Model;

namespace TeklaEvents
{
    /// <summary>
    /// Represents the main form of the Tekla Structures event handling application.
    /// </summary>
    public partial class Form1 : Form
    {
        private Events ModelEvents { get; set; }
        private object _changedObjectHandlerLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the click event of the "Activate Events" button.
        /// Activates Tekla Structures events and displays a message box.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialize and register Tekla Structures events
                ModelEvents = new Events();
                ModelEvents.SelectionChange += this.ModelEvents_SelectionChanged;
                ModelEvents.ModelObjectChanged += this.ModelEvents_ModelObjectChanged;
                ModelEvents.ModelSave += this.ModelEvents_ModelSave;
                ModelEvents.Interrupted += this.OnInterrupted;
                ModelEvents.TeklaStructuresExit += this.ModelEvents_TeklaExit;

                ModelEvents.Register();
                MessageBox.Show("Events activated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handles the ModelObjectChanged event of Tekla Structures.
        /// Displays a message box with information about the changed model object.
        /// </summary>
        /// <param name="changes">The list of changes.</param>
        private void ModelEvents_ModelObjectChanged(List<ChangeData> changes)
        {
            lock (_changedObjectHandlerLock)
            {
                // Start a new task to show a message box asynchronously
                new System.Threading.Tasks.Task(() =>
                {
                    MessageBox.Show($"ModelObject changed: {changes[0].Type} Id: {changes[0].Object.Identifier.ID} type: {changes[0].Object.GetType()}");
                }).Start();
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of Tekla Structures.
        /// Displays a message box indicating that the selection has changed.
        /// </summary>
        private void ModelEvents_SelectionChanged()
        {
            MessageBox.Show("SelectionChanged");
        }

        /// <summary>
        /// Handles the ModelSave event of Tekla Structures.
        /// Displays a message box indicating that the model has been saved.
        /// </summary>
        private void ModelEvents_ModelSave()
        {
            MessageBox.Show("ModelSave");
        }

        /// <summary>
        /// Handles the Interrupted event of Tekla Structures.
        /// Displays a message box indicating that the operation has been interrupted.
        /// </summary>
        private void OnInterrupted()
        {
            MessageBox.Show("Interrupted");
        }

        /// <summary>
        /// Handles the TeklaStructuresExit event of Tekla Structures.
        /// Unregisters Tekla Structures events and exits the application.
        /// </summary>
        private void ModelEvents_TeklaExit()
        {
            ModelEvents.UnRegister();
            Application.Exit();
        }

        /// <summary>
        /// Handles the click event of the "Deactivate Events" button.
        /// Deactivates Tekla Structures events and displays a message box.
        /// </summary>
        private void buttonDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                ModelEvents.UnRegister();
                MessageBox.Show("Events deactivated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the form.
        /// Ensures that Tekla Structures events are properly unregistered when the form is closing.
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ModelEvents.UnRegister();
        }
    }
}
