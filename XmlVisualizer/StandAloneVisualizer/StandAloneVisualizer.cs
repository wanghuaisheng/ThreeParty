﻿// Xml Visualizer v.2
// by Lars Hove Christiansen (larshove@gmail.com)
// http://www.codeplex.com/XmlVisualizer

using System;
using System.Windows.Forms;

namespace XmlVisualizer
{
    public static class StandAloneVisualizer
    {
        [STAThread]
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length == 2)
            {
                try
                {
                    Visualizer.ShowModeless_LoadXmlFromFile(args[1]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Xml Visualizer v.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Visualizer.ShowModeless_LoadXmlFromString("");
            }
        }
    }
}