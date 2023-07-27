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

        public Form1()
        {
            InitializeComponent();

            longPressTimer.Interval = 1000; // 1 second for long press
            longPressTimer.Tick += LongPressTimer_Tick;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            int initialMacroCount = 5;
            AddMacroButtons(initialMacroCount);

        }

        private void AddMacroButtons(int count)
        {
            // Clear any existing buttons from the macroButtonsPanel
            macroButtonsPanel.Controls.Clear();

            // Add the specified number of buttons (macro buttons) to the macroButtonsPanel
            for (int i = 0; i < count; i++)
            {
                Button macroButton = new Button();
                macroButton.Text = "Macro " + (i + 1);
                macroButton.Tag = i; // Store the macro index in the Tag property for later reference
                macroButton.Click += MacroButton_Click; // Attach the click event handler
                macroButton.MouseDown += MacroButton_MouseDown; // Attach the MouseDown event handler
                macroButton.MouseUp += MacroButton_MouseUp; // Attach the MouseUp event handler
                macroButtonsPanel.Controls.Add(macroButton);
            }
        }

        // Event handler for the macro buttons' Click event
        private void MacroButton_Click(object sender, EventArgs e)
        {
            // This event will only handle single-click action, not long-press
            Button clickedButton = (Button)sender;
            int macroIndex = (int)clickedButton.Tag;
            // Handle the single-click action here (if needed)
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


        private void btnSave_Click(object sender, EventArgs e)
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
            longPressTimer.Start();
        }

        private void MacroButton_MouseUp(object sender, MouseEventArgs e)
        {
            longPressTimer.Stop();
            longPressButtonIndex = -1;
        }

        private void LongPressTimer_Tick(object sender, EventArgs e)
        {
            longPressTimer.Stop();

            if (longPressButtonIndex >= 0 && longPressButtonIndex < macroButtonsPanel.Controls.Count)
            {
                int macroIndex = longPressButtonIndex;
                MacroEditorForm editorForm = new MacroEditorForm(macroIndex);
                editorForm.ShowDialog();
            }

            longPressButtonIndex = -1;
        }

        private void longPressTimer_Tick_1(object sender, EventArgs e)
        {

        }

        
    }
}
