using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromons
{
    internal partial class AboutForm
    {
        #region Fields
        /// <summary>
        /// Required designer variable.
        /// </summary>
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private IContainer components = null;
        #endregion

        #region Overrides
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.components?.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(AboutForm));
            this.logoPictureBox = new PictureBox();
            this.infoLabel = new Label();
            this.okButton = new Button();
            this.userLink = new LinkLabel();
            this.opLink = new LinkLabel();
            this.sourceLink = new LinkLabel();
            this.licenseImage = new PictureBox();
            this.versionLabel = new Label();
            ((ISupportInitialize)this.logoPictureBox).BeginInit();
            ((ISupportInitialize)this.licenseImage).BeginInit();
            SuspendLayout();
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Dock = DockStyle.Left;
            this.logoPictureBox.Image = (Image)resources.GetObject("logoPictureBox.Image");
            this.logoPictureBox.Location = new Point(12, 11);
            this.logoPictureBox.Margin = new Padding(4);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new Size(175, 312);
            this.logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 13;
            this.logoPictureBox.TabStop = false;
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new Point(194, 11);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new Size(223, 102);
            this.infoLabel.TabIndex = 14;
            this.infoLabel.Text = "This simulation and software\r\nwas created by Christophe\r\nSavard and is licensed u" +
                                  "nder\r\nCC-BY-SA 3.0 Unported. The\r\noriginal simulation code and\r\nidea goes all to" +
                                  " /u/Morning_Fresh.";
            // 
            // okButton
            // 
            this.okButton.Location = new Point(258, 300);
            this.okButton.Name = "okButton";
            this.okButton.Size = new Size(98, 23);
            this.okButton.TabIndex = 15;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += okButton_Click;
            // 
            // userLink
            // 
            this.userLink.AutoSize = true;
            this.userLink.Location = new Point(197, 206);
            this.userLink.Name = "userLink";
            this.userLink.Size = new Size(119, 17);
            this.userLink.TabIndex = 16;
            this.userLink.TabStop = true;
            this.userLink.Text = "/u/Morning_Fresh";
            this.userLink.LinkClicked += userLink_LinkClicked;
            // 
            // opLink
            // 
            this.opLink.AutoSize = true;
            this.opLink.Location = new Point(197, 223);
            this.opLink.Name = "opLink";
            this.opLink.Size = new Size(102, 17);
            this.opLink.TabIndex = 17;
            this.opLink.TabStop = true;
            this.opLink.Text = "Original thread";
            this.opLink.LinkClicked += opLink_LinkClicked;
            // 
            // sourceLink
            // 
            this.sourceLink.AutoSize = true;
            this.sourceLink.Location = new Point(197, 240);
            this.sourceLink.Name = "sourceLink";
            this.sourceLink.Size = new Size(53, 17);
            this.sourceLink.TabIndex = 18;
            this.sourceLink.TabStop = true;
            this.sourceLink.Text = "Source";
            this.sourceLink.LinkClicked += sourceLink_LinkClicked;
            // 
            // licenseImage
            // 
            this.licenseImage.Cursor = Cursors.Hand;
            this.licenseImage.Image = (Image)resources.GetObject("licenseImage.Image");
            this.licenseImage.Location = new Point(197, 260);
            this.licenseImage.Name = "licenseImage";
            this.licenseImage.Size = new Size(88, 31);
            this.licenseImage.SizeMode = PictureBoxSizeMode.AutoSize;
            this.licenseImage.TabIndex = 19;
            this.licenseImage.TabStop = false;
            this.licenseImage.DoubleClick += licenseImage_Click;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new Point(190, 123);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new Size(60, 17);
            this.versionLabel.TabIndex = 20;
            this.versionLabel.Text = "Version:";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(430, 334);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.licenseImage);
            this.Controls.Add(this.sourceLink);
            this.Controls.Add(this.opLink);
            this.Controls.Add(this.userLink);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.logoPictureBox);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Margin = new Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new Padding(12, 11, 12, 11);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "About Micromons";
            ((ISupportInitialize)this.logoPictureBox).EndInit();
            ((ISupportInitialize)this.licenseImage).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        #endregion

        #region Form components
        private PictureBox logoPictureBox;
        private Label infoLabel;
        private Button okButton;
        private LinkLabel userLink;
        private LinkLabel opLink;
        private LinkLabel sourceLink;
        private PictureBox licenseImage;
        private Label versionLabel;
        #endregion
    }
}
