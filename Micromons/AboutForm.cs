using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

/* This Micromons simulation was created by Christophe Savard (stupid_chris) and is licensed
 * licensed under CC-BY-SA 3.0 Unported. The entire credit for the original idea and simulation
 * code goes to Reddit user /u/Morning_Fresh, none of this would have been possible without
 * his original work and idea. */

namespace Micromons
{
    /// <summary>
    /// About UI Form
    /// </summary>
    internal partial class AboutForm : Form
    {
        #region Instance
        /// <summary>
        /// Singleton instance of the About form
        /// </summary>
        public static AboutForm Instance { get; } = new AboutForm();
        #endregion

        #region Constructors
        /// <summary>
        /// Creates and initializes a new AboutForm
        /// </summary>
        private AboutForm()
        {
            //Initialize form
            InitializeComponent();

            //Setup version text info
            Assembly assembly = Assembly.GetExecutingAssembly();
            string fullVersion = assembly.GetName().Version.ToString();
            string assemblyVersion = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
            this.versionLabel.Text = $"Version: {assemblyVersion}\n\nBuild Version: {fullVersion}";
        }
        #endregion

        #region Events
        /// <summary>
        /// Ok button click event
        /// </summary>
        private void okButton_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// User link clicked event
        /// </summary>
        private void userLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.reddit.com/user/Morning_Fresh");
        }

        /// <summary>
        /// License image clicked event
        /// </summary>
        private void licenseImage_Click(object sender, EventArgs e)
        {
            Process.Start("https://creativecommons.org/licenses/by-sa/3.0/");
        }

        /// <summary>
        /// OP link clicked event
        /// </summary>
        private void opLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.reddit.com/r/dataisbeautiful/comments/5tfcym/a_simulation_of_360000_1_pixel_pokemon_fighting/");
        }

        /// <summary>
        /// Source link clicked event
        /// </summary>
        private void sourceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/StupidChris/Micromons");
        }
        #endregion
    }
}
