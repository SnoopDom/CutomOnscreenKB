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
        private Timer longPressTimer;


        public Form1()
        {
            InitializeComponent();        
        }

        // Initialize the Timer and its properties in the Form_Load event.
        private void Form1_Load(object sender, EventArgs e)
        {
            int initialMacroCount = 4;
            AddMacroButtons(initialMacroCount);

            longPressTimer = new Timer();
            longPressTimer.Interval = 1000; // 1 second for long press
//            longPressTimer.Tick += LongPressTimer_Tick;
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
                macroButtonsPanel.Controls.Add(macroButton);
            }
        }

        // Event handler for the macro buttons' Click event
        private void MacroButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int macroIndex = (int)clickedButton.Tag;
            MacroEditorForm editorForm = new MacroEditorForm(macroIndex);
            editorForm.ShowDialog();
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
    }
}
