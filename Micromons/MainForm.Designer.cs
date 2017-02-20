using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Micromons.Tools;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable RedundantExplicitArrayCreation

namespace Micromons
{
    public partial class MainForm
    {
        #region Fields
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        #endregion

        #region Overrides
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.displayPanel = new Panel();
            this.simulationImage = new PictureBox();
            this.menuBar = new MenuStrip();
            this.menuStrip = new ToolStripMenuItem();
            this.aboutMenu = new ToolStripMenuItem();
            this.exitMenu = new ToolStripMenuItem();
            this.progressBar = new ProgressBar();
            this.simulateButton = new Button();
            this.startSimulationButtom = new Button();
            this.stopSimulationButton = new Button();
            this.simulationBox = new GroupBox();
            this.newSimulationButton = new Button();
            this.rankingView = new ListView();
            this.rankColumn = new ColumnHeader();
            this.amountColumn = new ColumnHeader();
            this.typesColumn = new ColumnHeader();
            this.frameLabel = new Label();
            this.newSimWorker = new AbortableWorker();
            this.simOnceWorker = new AbortableWorker();
            this.simContinuousWorker = new AbortableWorker();
            this.displayPanel.SuspendLayout();
            ((ISupportInitialize)this.simulationImage).BeginInit();
            this.menuBar.SuspendLayout();
            this.simulationBox.SuspendLayout();
            SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.BorderStyle = BorderStyle.Fixed3D;
            this.displayPanel.Controls.Add(this.simulationImage);
            this.displayPanel.Location = new Point(12, 31);
            this.displayPanel.MaximumSize = new Size(610, 570);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new Size(610, 570);
            this.displayPanel.TabIndex = 1;
            // 
            // simulationImage
            // 
            this.simulationImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.simulationImage.Location = new Point(3, 3);
            this.simulationImage.Name = "simulationImage";
            this.simulationImage.Size = new Size(600, 560);
            this.simulationImage.SizeMode = PictureBoxSizeMode.Zoom;
            this.simulationImage.TabIndex = 0;
            this.simulationImage.TabStop = false;
            // 
            // menuBar
            // 
            this.menuBar.ImageScalingSize = new Size(20, 20);
            this.menuBar.Items.AddRange(new ToolStripItem[] { this.menuStrip });
            this.menuBar.Location = new Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new Size(982, 28);
            this.menuBar.TabIndex = 2;
            // 
            // menuStrip
            // 
            this.menuStrip.DropDownItems.AddRange(new ToolStripItem[] { this.aboutMenu, this.exitMenu });
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new Size(58, 24);
            this.menuStrip.Text = "Menu";
            // 
            // aboutMenu
            // 
            this.aboutMenu.Name = "aboutMenu";
            this.aboutMenu.Size = new Size(181, 26);
            this.aboutMenu.Text = "About";
            this.aboutMenu.Click += aboutMenu_Click;
            // 
            // exitMenu
            // 
            this.exitMenu.Name = "exitMenu";
            this.exitMenu.ShortcutKeys = Keys.Alt | Keys.F4;
            this.exitMenu.Size = new Size(181, 26);
            this.exitMenu.Text = "Exit";
            this.exitMenu.Click += exitMenu_Click;
            // 
            // progressBar
            // 
            this.progressBar.Location = new Point(12, 607);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new Size(610, 23);
            this.progressBar.Step = 1;
            this.progressBar.Style = ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 4;
            // 
            // simulateButton
            // 
            this.simulateButton.Location = new Point(6, 21);
            this.simulateButton.Name = "simulateButton";
            this.simulateButton.Size = new Size(140, 60);
            this.simulateButton.TabIndex = 5;
            this.simulateButton.Text = "Simulate once";
            this.simulateButton.UseVisualStyleBackColor = true;
            this.simulateButton.Click += simulateButton_Click;
            // 
            // startSimulationButtom
            // 
            this.startSimulationButtom.Location = new Point(152, 21);
            this.startSimulationButtom.Name = "startSimulationButtom";
            this.startSimulationButtom.Size = new Size(140, 28);
            this.startSimulationButtom.TabIndex = 6;
            this.startSimulationButtom.Text = "Start simlation";
            this.startSimulationButtom.UseVisualStyleBackColor = true;
            this.startSimulationButtom.Click += startSimulationButtom_Click;
            // 
            // stopSimulationButton
            // 
            this.stopSimulationButton.Enabled = false;
            this.stopSimulationButton.Location = new Point(152, 53);
            this.stopSimulationButton.Name = "stopSimulationButton";
            this.stopSimulationButton.Size = new Size(140, 28);
            this.stopSimulationButton.TabIndex = 7;
            this.stopSimulationButton.Text = "Stop simulation";
            this.stopSimulationButton.UseVisualStyleBackColor = true;
            this.stopSimulationButton.Click += stopSimulationButton_Click;
            // 
            // simulationBox
            // 
            this.simulationBox.Controls.Add(this.startSimulationButtom);
            this.simulationBox.Controls.Add(this.stopSimulationButton);
            this.simulationBox.Controls.Add(this.simulateButton);
            this.simulationBox.Enabled = false;
            this.simulationBox.Location = new Point(157, 636);
            this.simulationBox.Name = "simulationBox";
            this.simulationBox.Size = new Size(301, 89);
            this.simulationBox.TabIndex = 8;
            this.simulationBox.TabStop = false;
            this.simulationBox.Text = "Simulation controls";
            // 
            // newSimulationButton
            // 
            this.newSimulationButton.Location = new Point(12, 636);
            this.newSimulationButton.Name = "newSimulationButton";
            this.newSimulationButton.Size = new Size(139, 89);
            this.newSimulationButton.TabIndex = 9;
            this.newSimulationButton.Text = "Create new simulation";
            this.newSimulationButton.UseVisualStyleBackColor = true;
            this.newSimulationButton.Click += newSimulationButton_Click;
            // 
            // rankingView
            // 
            this.rankingView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.rankingView.Columns.AddRange(new ColumnHeader[] { this.rankColumn, this.amountColumn, this.typesColumn });
            this.rankingView.FullRowSelect = true;
            this.rankingView.GridLines = true;
            this.rankingView.Location = new Point(628, 31);
            this.rankingView.MultiSelect = false;
            this.rankingView.Name = "rankingView";
            this.rankingView.Size = new Size(342, 711);
            this.rankingView.TabIndex = 10;
            this.rankingView.UseCompatibleStateImageBehavior = false;
            this.rankingView.View = View.Details;
            // 
            // rankColumn
            // 
            this.rankColumn.Text = "Rank";
            this.rankColumn.Width = 45;
            // 
            // amountColumn
            // 
            this.amountColumn.Text = "Amount";
            this.amountColumn.Width = 80;
            // 
            // typesColumn
            // 
            this.typesColumn.Text = "Types";
            this.typesColumn.Width = 173;
            // 
            // frameLabel
            // 
            this.frameLabel.AutoSize = true;
            this.frameLabel.Location = new Point(464, 657);
            this.frameLabel.Name = "frameLabel";
            this.frameLabel.Size = new Size(111, 34);
            this.frameLabel.TabIndex = 11;
            this.frameLabel.Text = "Frame: \r\nLast frame (ms):\r\n";
            // 
            // newSimWorker
            // 
            this.newSimWorker.WorkerReportsProgress = true;
            this.newSimWorker.DoWork += CreateNewSimulation;
            this.newSimWorker.ProgressChanged += UpdateProgress;
            this.newSimWorker.RunWorkerCompleted += newSimWorker_RunWorkerCompleted;
            // 
            // simOnceWorker
            // 
            this.simOnceWorker.WorkerReportsProgress = true;
            this.simOnceWorker.DoWork += SimulateOnce;
            this.simOnceWorker.ProgressChanged += UpdateProgress;
            this.simOnceWorker.RunWorkerCompleted += simOnceWorker_RunWorkerCompleted;
            // 
            // simContinuousWorker
            // 
            this.simContinuousWorker.WorkerReportsProgress = true;
            this.simContinuousWorker.DoWork += SimulateContinuous;
            this.simContinuousWorker.ProgressChanged += UpdateProgress;
            this.simContinuousWorker.RunWorkerCompleted += simContinuousWorker_RunWorkerCompleted;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(982, 753);
            this.Controls.Add(this.frameLabel);
            this.Controls.Add(this.rankingView);
            this.Controls.Add(this.newSimulationButton);
            this.Controls.Add(this.simulationBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.displayPanel);
            this.Controls.Add(this.menuBar);
            this.MainMenuStrip = this.menuBar;
            this.MinimumSize = new Size(1000, 800);
            this.Name = "MainForm";
            this.SizeGripStyle = SizeGripStyle.Show;
            this.Text = "Micromons Simulation";
            FormClosing += MainForm_FormClosing;
            this.displayPanel.ResumeLayout(false);
            ((ISupportInitialize)this.simulationImage).EndInit();
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.simulationBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        #region Form components
        private Panel displayPanel;
        private MenuStrip menuBar;
        private ToolStripMenuItem menuStrip;
        private ToolStripMenuItem aboutMenu;
        private ToolStripMenuItem exitMenu;
        private PictureBox simulationImage;
        private ProgressBar progressBar;
        private Button simulateButton;
        private Button startSimulationButtom;
        private Button stopSimulationButton;
        private GroupBox simulationBox;
        private Button newSimulationButton;
        private ListView rankingView;
        private ColumnHeader rankColumn;
        private ColumnHeader amountColumn;
        private ColumnHeader typesColumn;
        private Label frameLabel;
        private AbortableWorker newSimWorker;
        private AbortableWorker simOnceWorker;
        private AbortableWorker simContinuousWorker;
        #endregion
    }
}

