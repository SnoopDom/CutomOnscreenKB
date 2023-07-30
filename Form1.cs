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
    public partial class Form1 : Form
    {
        private int longPressButtonIndex = -1;
        private DateTime mouseDownTime; // Store the time when MouseDown event occurs

        public Form1()
        {
            InitializeComponent();

            longPressTimer.Interval = 1000; // 1 second for long press
            longPressTimer.Tick += LongPressTimer_Tick; // Subscribe to the Tick event
        }


        private void Form1_Load_1(object sender, EventArgs e)
        {
            int initialMacroCount = 5;
            AddMacroButtons(initialMacroCount);
        }

        private void AddMacroButtons(int count)
        {
            int existingMacroCount = macroButtonsPanel.Controls.Count;
            int delta = count - existingMacroCount;

            if (delta > 0)
            {
                // Add the specified number of buttons (macro buttons) to the macroButtonsPanel
                for (int i = 0; i < delta; i++)
                {
                    Button macroButton = new Button();
                    macroButton.Text = "Macro " + (existingMacroCount + i + 1);
                    macroButton.Tag = existingMacroCount + i; // Store the macro index in the Tag property for later reference
                    macroButton.Click += MacroButton_Click; // Attach the click event handler
                    macroButton.MouseDown += MacroButton_MouseDown; // Attach the MouseDown event handler
                    macroButton.MouseUp += MacroButton_MouseUp; // Attach the MouseUp event handler
                    macroButtonsPanel.Controls.Add(macroButton);
                }
            }
            else if (delta < 0)
            {
                // Remove excess buttons
                for (int i = 0; i < -delta; i++)
                {
                    int indexToRemove = macroButtonsPanel.Controls.Count - 1;
                    macroButtonsPanel.Controls.RemoveAt(indexToRemove);
                }
            }
        }

        // Event handler for the macro buttons' Click event
        private void MacroButton_Click(object sender, EventArgs e)
        {
            
        }


        private void btnDown_Click(object sender, EventArgs e)
        {
            // Ensure there's at least one macro button before decreasing
            if (macroButtonsPanel.Controls.Count > 1)
            {
                // Decrease the number of macro buttons by 1
                AddMacroButtons(macroButtonsPanel.Controls.Count - 1);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            // Increase the number of macro buttons by 1
            AddMacroButtons(macroButtonsPanel.Controls.Count + 1);
        }


        private void btnSave_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Profile Files (*.profile)|*.profile|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Save the current profile to the selected file
                SaveProfile(saveFileDialog.FileName);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Profile Files (*.profile)|*.profile|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load the selected profile from the file
                LoadProfile(openFileDialog.FileName);
            }
        }

        private void SaveProfile(string fileName)
        {
            // Your code to save the profile to a file goes here
        }

        private void LoadProfile(string fileName)
        {
            // Your code to load the profile from a file goes here
        }

        private void macroButtonsPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void MacroButton_MouseDown(object sender, MouseEventArgs e)
        {
            longPressButtonIndex = (int)((Button)sender).Tag;
            mouseDownTime = DateTime.Now; // Store the current time on MouseDown

            // Start the long press timer on MouseDown
            longPressTimer.Start();
        }

        private void MacroButton_MouseUp(object sender, MouseEventArgs e)
        {
            longPressTimer.Stop(); // Stop the timer on MouseUp

            // Calculate the duration of the press
            TimeSpan pressDuration = DateTime.Now - mouseDownTime;
            longPressButtonIndex = -1;

            // Determine if it's a long-press or single-click based on the press duration
            if (pressDuration.TotalMilliseconds >= 1000) // 1000 milliseconds for 1 second long-press
            {
                LongPressTimer_Tick(null, EventArgs.Empty); // Call the tick event manually
            }
            else
            {
                // It's a short press, perform the action
                int macroIndex = (int)((Button)sender).Tag;
                MessageBox.Show($"Perform Action: {macroButtonsPanel.Controls[macroIndex].Text}");
            }
        }

        private void LongPressTimer_Tick(object sender, EventArgs e)
        {
            longPressTimer.Stop();

            if (longPressButtonIndex >= 0 && longPressButtonIndex < macroButtonsPanel.Controls.Count)
            {
                int macroIndex = longPressButtonIndex;
                MacroEditorForm editorForm = new MacroEditorForm();
                editorForm.MacroName = macroButtonsPanel.Controls[macroIndex].Text;

                if (editorForm.ShowDialog() == DialogResult.OK)
                {
                    // If the MacroEditorForm is closed with OK result, update the button text
                    macroButtonsPanel.Controls[macroIndex].Text = editorForm.UpdatedMacroName; // Use UpdatedMacroName property
                }
            }

            longPressButtonIndex = -1;
        }
    }
}