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
        private int macroIndex; // This variable will store the index of the macro button being edited.

        public MacroEditorForm(int index)
        {
            InitializeComponent();
            macroIndex = index;
            // Additional initialization code, if needed...
        }
    }
}
