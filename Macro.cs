using System.Collections.Generic;

namespace CutomOnscreenKB
{
    public class Macro
    {
        public string Name { get; set; }
        public List<MacroStep> Steps { get; set; } = new List<MacroStep>();
    }

    public class MacroStep
    {
        public string Type { get; set; }
        public string Content { get; set; }
    }
}