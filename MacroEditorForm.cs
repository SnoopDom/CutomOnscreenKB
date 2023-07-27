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

        // Custom class to represent the macro sequence
        private class MacroItem
        {
            public string Type { get; set; } // "KeyPress" or "TextString"
            public string Content { get; set; } // Stores the keypress or text string
        }

        public MacroEditorForm(int macroIndex)
        {
            InitializeComponent();
            // Set the title of the form based on the macro index (e.g., "Macro 1")
            this.Text = "Macro " + (macroIndex + 1);
        }

        // Event handler for the Add KeyPress button
        private void btnAddKeyPress_Click(object sender, EventArgs e)
        {
            // Display a custom input dialog asking the user to press a key
            string selectedKey = ShowKeyPressInputDialog();

            // If the user clicked OK and selected a key, create a visual representation in the macroFlowLayout
            if (!string.IsNullOrEmpty(selectedKey))
            {
                AddMacroItemToFlowLayout("KeyPress", selectedKey);
            }
        }

        private string ShowKeyPressInputDialog()
        {
            // Create and show a custom input dialog
            using (var inputDialog = new Form())
            {
                inputDialog.Text = "Press a Key";
                inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputDialog.StartPosition = FormStartPosition.CenterParent;
                inputDialog.MinimizeBox = false;
                inputDialog.MaximizeBox = false;

                // Add a label to instruct the user
                var label = new Label { Text = "Press a key on your keyboard:" };
                label.Dock = DockStyle.Top;
                inputDialog.Controls.Add(label);

                // Add a text box to display the selected key
                var textBox = new TextBox();
                textBox.Dock = DockStyle.Top;
                inputDialog.Controls.Add(textBox);

                // Add an OK button to confirm the selection
                var okButton = new Button { Text = "OK" };
                okButton.Dock = DockStyle.Bottom;
                okButton.Click += (sender, e) => inputDialog.Close();
                inputDialog.Controls.Add(okButton);

                // Set the form's AcceptButton property to the OK button, so pressing Enter will also close the dialog
                inputDialog.AcceptButton = okButton;

                // Show the input dialog as a modal dialog and return the selected key
                return inputDialog.ShowDialog() == DialogResult.OK ? textBox.Text : null;
            }
        }

        // Event handler for the Add TextString button
        private void btnAddTextString_Click(object sender, EventArgs e)
        {
            // Retrieve the user input from the "txtBoxUserInput" textbox
            string userInput = txtBoxUserInput.Text;

            // Create a visual representation in the macroFlowLayout using a label
            AddMacroItemToFlowLayout("TextString", userInput);
        }

        private void AddMacroItemToFlowLayout(string itemType, string content)
        {
            // Create a custom button to represent the macro item
            var btnMacroItem = new Button();
            btnMacroItem.Text = itemType == "KeyPress" ? $"KeyPress: {content}" : $"Text: {content}";
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

            // Add the button to the macroFlowLayout
            macroFlowLayout.Controls.Add(btnMacroItem);
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

    }
}

