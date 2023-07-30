using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CutomOnscreenKB
{
    public partial class MacroEditorForm : Form
    {
        private List<MacroItem> macroItems = new List<MacroItem>();
        private string macroName;

        // Property to store the updated macro name
        public string UpdatedMacroName { get; private set; }
        // Custom class to represent the macro sequence

        private class MacroItem
        {
            public string Type { get; set; } // "KeyPress" or "TextString"
            public string Content { get; set; } // Stores the keypress or text string
        }
        // Property to set/get the macro name
        public string MacroName 
        {
            get { return macroName; }
            set
            {
                macroName = value;
                txtBoxMacroName.Text = macroName;
            }
        }
        public MacroEditorForm(int macroIndex)
        {
            InitializeComponent();
            // Set the title of the form based on the macro index (e.g., "Macro 1")
            this.Text = "Macro " + (macroIndex + 1);
        }
        public MacroEditorForm()
        {
            InitializeComponent();
        }

        // Event handler for the Add Button button
        private void btnAddButton_Click(object sender, EventArgs e)
        {
            // Get the button label from the txtBoxUserInput
            string buttonLabel = txtBoxUserInput.Text;

            // If the user entered a button label, create a visual representation in the macroFlowLayout
            if (!string.IsNullOrEmpty(buttonLabel))
            {
                AddMacroItemToFlowLayout("Button", buttonLabel);
            }
        }

        // Event handler for the Add KeyPress button
        private void btnAddKeyPress_Click(object sender, EventArgs e)
        {
            // Get the key press from the txtBoxUserInput
            string selectedKey = txtBoxUserInput.Text;

            // If the user entered a key press, create a visual representation in the macroFlowLayout
            if (!string.IsNullOrEmpty(selectedKey))
            {
                AddMacroItemToFlowLayout("KeyPress", selectedKey);
            }
        }

        // Event handler for the Add TextString button
        private void btnAddTextString_Click(object sender, EventArgs e)
        {
            // Get the text string from the txtBoxUserInput
            string userInput = txtBoxUserInput.Text;

            // If the user entered a text string, create a visual representation in the macroFlowLayout
            if (!string.IsNullOrEmpty(userInput))
            {
                AddMacroItemToFlowLayout("TextString", userInput);
            }
        }

        private void AddMacroItemToFlowLayout(string itemType, string content)
        {
            // Create a custom button to represent the macro item
            var btnMacroItem = new Button();
            btnMacroItem.Text = itemType == "KeyPress" ? $"KeyPress: {content}" : (itemType == "TextString" ? $"Text: {content}" : content);
            btnMacroItem.AutoSize = true;
            btnMacroItem.FlatStyle = FlatStyle.Flat;
            btnMacroItem.BackColor = Color.FromArgb(70, 130, 180); // Customize the button appearance
            btnMacroItem.ForeColor = Color.White;
            btnMacroItem.Margin = new Padding(5);
            btnMacroItem.Padding = new Padding(10, 5, 10, 5);

            // Store the macro item information in the button's Tag property
            var macroItem = new MacroItem { Type = itemType, Content = content };
            btnMacroItem.Tag = macroItem;
            macroItems.Add(macroItem);

            // Attach a click event handler to the button to allow users to edit or delete the macro item
            btnMacroItem.Click += BtnMacroItem_Click;

            // Enable drag and drop for the button
            btnMacroItem.MouseDown += BtnMacroItem_MouseDown;

            // Add the button to the macroFlowLayout
            macroFlowLayout.Controls.Add(btnMacroItem);
        }

        private void BtnMacroItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ((Button)sender).DoDragDrop(sender, DragDropEffects.Move);
            }
        }

        private void macroFlowLayout_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Button)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void macroFlowLayout_DragDrop(object sender, DragEventArgs e)
        {
            Button draggedButton = (Button)e.Data.GetData(typeof(Button));
            int newIndex = macroFlowLayout.Controls.GetChildIndex(macroFlowLayout.GetChildAtPoint(macroFlowLayout.PointToClient(new Point(e.X, e.Y))));
            macroFlowLayout.Controls.SetChildIndex(draggedButton, newIndex);
        }

        private void BtnMacroItem_Click(object sender, EventArgs e)
        {
            // Handle the click event for the macro item button
            var clickedButton = (Button)sender;
            var macroItem = (MacroItem)clickedButton.Tag;

            // Show a message box to allow users to edit or delete the macro item
            var result = MessageBox.Show($"Edit or Delete this Macro Item?\n\n{macroItem.Type}: {macroItem.Content}", "Macro Item", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Handle edit (Open a dialog or implement any other logic to modify the macro item)
            }
            else if (result == DialogResult.No)
            {
                // Handle delete
                macroFlowLayout.Controls.Remove(clickedButton);
                macroItems.Remove(macroItem);
            }
        }

        // Implement the btnSave_Click and btnCancel_Click as described in the previous steps.

        // Event handler for the Save button
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Save the entered macro name to the public property
            UpdatedMacroName = txtBoxMacroName.Text;

            // Set the DialogResult to OK to indicate that the form was closed with Save
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void txtBoxMacroName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAutoClicker_Click(object sender, EventArgs e)
        {

        }

        // Event handler for the Add Delay button
        private void btnAddDelay_Click(object sender, EventArgs e)
        {
            // Get the delay time from the txtBoxUserInput
            if (int.TryParse(txtBoxUserInput.Text, out int delayTime) && delayTime > 0)
            {
                // If the user entered a valid delay time, create a visual representation in the macroFlowLayout
                AddMacroItemToFlowLayout("Delay", delayTime.ToString());
            }
            else
            {
                MessageBox.Show("Please enter a valid delay time (a positive integer).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int ShowDelayInputDialog()
        {
            // Create and show a custom input dialog
            using (var inputDialog = new Form())
            {
                inputDialog.Text = "Enter Delay (in milliseconds)";
                inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputDialog.StartPosition = FormStartPosition.CenterParent;
                inputDialog.MinimizeBox = false;
                inputDialog.MaximizeBox = false;

                // Add a label to instruct the user
                var label = new Label { Text = "Enter the delay time (in milliseconds):" };
                label.Dock = DockStyle.Top;
                inputDialog.Controls.Add(label);

                // Add a numeric up-down control to input the delay time
                var numericUpDown = new NumericUpDown { Dock = DockStyle.Top, Minimum = 1 };
                inputDialog.Controls.Add(numericUpDown);

                // Add an OK button to confirm the delay input
                var okButton = new Button { Text = "OK" };
                okButton.Dock = DockStyle.Bottom;
                okButton.Click += (sender, e) => inputDialog.Close();
                inputDialog.Controls.Add(okButton);

                // Set the form's AcceptButton property to the OK button, so pressing Enter will also close the dialog
                inputDialog.AcceptButton = okButton;

                // Show the input dialog as a modal dialog and return the entered delay time
                return inputDialog.ShowDialog() == DialogResult.OK ? (int)numericUpDown.Value : 0;
            }
        }


        private string ShowButtonInputDialog()
        {
            // Create and show a custom input dialog
            using (var inputDialog = new Form())
            {
                inputDialog.Text = "Enter Button Label";
                inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputDialog.StartPosition = FormStartPosition.CenterParent;
                inputDialog.MinimizeBox = false;
                inputDialog.MaximizeBox = false;

                // Add a label to instruct the user
                var label = new Label { Text = "Enter the button label:" };
                label.Dock = DockStyle.Top;
                inputDialog.Controls.Add(label);

                // Add a text box to input the button label
                var textBox = new TextBox { Dock = DockStyle.Top };
                inputDialog.Controls.Add(textBox);

                // Add an OK button to confirm the button label input
                var okButton = new Button { Text = "OK" };
                okButton.Dock = DockStyle.Bottom;
                okButton.Click += (sender, e) => inputDialog.Close();
                inputDialog.Controls.Add(okButton);

                // Set the form's AcceptButton property to the OK button, so pressing Enter will also close the dialog
                inputDialog.AcceptButton = okButton;

                // Show the input dialog as a modal dialog and return the entered button label
                return inputDialog.ShowDialog() == DialogResult.OK ? textBox.Text : null;
            }
        }

    }
}

