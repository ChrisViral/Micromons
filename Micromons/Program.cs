using System;
using System.Windows.Forms;

/* This Micromons simulation was created by Christophe Savard (stupid_chris) and is licensed
 * licensed under CC-BY-SA 3.0 Unported. The entire credit for the original idea and simulation
 * code goes to Reddit user /u/Morning_Fresh, none of this would have been possible without
 * his original work and idea. */

namespace Micromons
{
    internal static class Program
    {
        #region Main
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        #endregion
    }
}