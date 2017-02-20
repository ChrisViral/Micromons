using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Micromons.Simulation;
using Micromons.Tools;
using TypeStats = Micromons.Simulation.Grid.TypeStats;

namespace Micromons
{
    /// <summary>
    /// Primary UI Form
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constants
        /// <summary>
        /// Size of the side of the simulation
        /// </summary>
        internal const int size = 200;
        /// <summary>
        /// Size of the side of the simulation image
        /// </summary>
        internal const int imageSize = 600;
        /// <summary>
        /// Effective pixel width of every Micromon
        /// </summary>
        internal const int pixelWidth = 3;
        /// <summary>
        /// Size of the image's byte array
        /// </summary>
        internal const int dataSize = 1080000;
        #endregion

        #region Fields
        /// <summary> Current simulation </summary>
        private Grid simulation;
        /// <summary> Current frame type statistics, ordered by amount </summary>
        private List<TypeStats> currentStats;
        /// <summary> State of the continuous simulation </summary>
        private bool simulate;
        /// <summary> Current frame number </summary>
        private int frame;
        /// <summary> Timing block event </summary>
        private readonly ManualResetEvent block;
        /// <summary> Simulation image reference </summary>
        private readonly Bitmap image;
        /// <summary> List of all BackgroundWorkers </summary>
        private readonly List<AbortableWorker> wokers;
        /// <summary> Simulation timer </summary>
        private readonly Stopwatch timer;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates and initializes a new MainForm
        /// </summary>
        internal MainForm()
        {
            //Initialize form
            InitializeComponent();
            this.progressBar.Maximum = size * size;

            //Setup helpers
            this.block = new ManualResetEvent(true);
            this.wokers = new List<AbortableWorker>(3) { this.newSimWorker, this.simOnceWorker, this.simContinuousWorker };
            this.timer = new Stopwatch();

            //Initialize image to blank white
            this.image = new Bitmap(imageSize, imageSize, PixelFormat.Format24bppRgb);
            BitmapData data = this.image.LockBits(new Rectangle(0, 0, imageSize, imageSize), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            byte[] bytes = new byte[dataSize];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = byte.MaxValue;
            }
            Marshal.Copy(bytes, 0, data.Scan0, bytes.Length);
            this.image.UnlockBits(data);
            this.simulationImage.Image = this.image;
        }
        #endregion

        #region Events
        /// <summary>
        /// Menu exit button event
        /// </summary>
        private void exitMenu_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// Form closing event
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //If any workers are running, display a prompt
            AbortableWorker worker = this.wokers.Find(w => w.IsBusy);
            if (worker != null)
            {
                //Display prompt
                DialogResult prompt = MessageBox.Show(this, "The simulation is still running in the background, are you sure you want to close the form now?",
                                                      "Simulation running!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                switch (prompt)
                {
                    //If user continues to close, abort working thread
                    case DialogResult.Yes:
                        worker.Abort(); break;

                    //Else cancel closing event
                    case DialogResult.No:
                        e.Cancel = true; break;
                }
            }
        }

        /// <summary>
        /// Menu about button event
        /// </summary>
        private void aboutMenu_Click(object sender, EventArgs e) => AboutForm.Instance.Show(this);

        /// <summary>
        /// New Simulation button click event
        /// </summary>
        private void newSimulationButton_Click(object sender, EventArgs e)
        {
            this.newSimWorker.RunWorkerAsync();

            this.UseWaitCursor = true;
            this.simulationBox.SetEnabled(false);
            this.newSimulationButton.SetEnabled(false);
        }

        /// <summary>
        /// Simulate Once button click event
        /// </summary>
        private void simulateButton_Click(object sender, EventArgs e)
        {
            this.simOnceWorker.RunWorkerAsync();

            this.UseWaitCursor = true;
            this.simulationBox.SetEnabled(false);
            this.newSimulationButton.SetEnabled(false);
        }

        /// <summary>
        /// Start Simulation button click event
        /// </summary>
        private void startSimulationButtom_Click(object sender, EventArgs e)
        {
            this.simulate = true;
            this.simContinuousWorker.RunWorkerAsync();

            this.simulateButton.SetEnabled(false);
            this.newSimulationButton.SetEnabled(false);
            this.startSimulationButtom.SetEnabled(false);
            this.stopSimulationButton.SetEnabled(true);
        }

        /// <summary>
        /// Stop simulation button click event
        /// </summary>
        private void stopSimulationButton_Click(object sender, EventArgs e)
        {
            this.simulate = false;
            this.stopSimulationButton.SetEnabled(false);
        }
        #endregion

        #region BackgroundWorker events
        /// <summary>
        /// Create new simulation background thread
        /// </summary>
        private void CreateNewSimulation(object sender, DoWorkEventArgs e)
        {
            //Create new Grid and update display accordingly
            this.simulation = new Grid(size, this.newSimWorker);
            this.simulation.UpdateImage(this.image);
            this.currentStats = this.simulation.AnalyzeGrid();
        }

        /// <summary>
        /// Simulate once background thread
        /// </summary>
        private void SimulateOnce(object sender, DoWorkEventArgs e) => Simulate(this.simOnceWorker);

        /// <summary>
        /// Simulates continuously background thread
        /// </summary>
        private void SimulateContinuous(object sender, DoWorkEventArgs e) => Simulate(this.simContinuousWorker);

        /// <summary>
        /// Worker progress
        /// </summary>
        private void UpdateProgress(object sender, ProgressChangedEventArgs e) => this.progressBar.PerformStep();

        /// <summary>
        /// Create new simulation completed
        /// </summary>
        private void newSimWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Setup new simulation display
            this.frame = 0;
            this.simulationBox.SetEnabled(true);
            UpdateDisplay();
            WorkerCompleted();
        }

        /// <summary>
        /// Simulate once completed
        /// </summary>
        private void simOnceWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.simulationBox.SetEnabled(true);
            UpdateDisplay();
            WorkerCompleted();
        }

        /// <summary>
        /// Simulate continuously completed
        /// </summary>
        private void simContinuousWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateDisplay();
            if (this.simulate) { this.simContinuousWorker.RunWorkerAsync(); }
            else
            {
                this.simulateButton.SetEnabled(true);
                this.startSimulationButtom.SetEnabled(true);
                WorkerCompleted();
            }
        }
        #endregion

        #region Static methods
        #if DEBUG
        /// <summary>
        /// Logs an object message to the standard debug output log, with a correct time stamp
        /// </summary>
        /// <param name="message">Object to log</param>
        public static void Log(object message) => Log(message.ToString());

        /// <summary>
        /// Logs a string message to the standard debug output log, with a correct time stamp
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Log(string message) => Debug.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss.ffff")}]: {message}");
        #endif
        #endregion

        #region Methods
        /// <summary>
        /// Runs a single frame of simulation while reporting progress to the passed BackgroundWorker
        /// </summary>
        /// <param name="worker">BackgroundWorker to report progress on</param>
        private void Simulate(BackgroundWorker worker)
        {
            if (worker == this.simContinuousWorker) { Thread.Sleep(100); }
            this.timer.Restart();

            this.simulation.Simulate(worker);
            this.simulation.UpdateImage(this.image);
            this.currentStats = this.simulation.AnalyzeGrid();

            this.timer.Stop();
        }

        /// <summary>
        /// Operations executed everytime a worker completes it's task
        /// </summary>
        private void WorkerCompleted()
        {
            this.newSimulationButton.SetEnabled(true);
            this.UseWaitCursor = false;

            #if DEBUG
            Log("Lock reset.");
            #endif

            this.block.Reset();
        }

        /// <summary>
        /// Updates the UI display of the simulation
        /// </summary>
        private void UpdateDisplay()
        {
            #if DEBUG
            Log("Updating display.");
            #endif

            //Update image
            this.simulationImage.Image = this.image;
            //Reset progressbar
            this.progressBar.Value = 0;
            //Update rankings display
            UpdateRanking();
            //Update frame text display
            IncrementFrame();

            #if DEBUG
            Log("Releasing lock.");
            #endif
            
            //Release display update lock
            this.block.Set();
        }

        /// <summary>
        /// Updates the string display information about the previous frame
        /// </summary>
        private void IncrementFrame() => this.frameLabel.Text = $"Frame: {++this.frame}\nLast frame (ms): {this.timer.ElapsedMilliseconds}";

        /// <summary>
        /// Updates the display of the rankings
        /// </summary>
        private void UpdateRanking()
        {
            //Clear items
            this.rankingView.BeginUpdate();
            this.rankingView.Items.Clear();
            
            //Add items for all types present
            for (int i = 0; i < this.currentStats.Count; )
            {
                Grid.TypeStats stats = this.currentStats[i++];
                this.rankingView.Items.Add(new ListViewItem(new[] { i.ToString(), stats.Amount.ToString(), stats.Pair.ToString() }));
            }

            //Release display
            this.rankingView.EndUpdate();
        }
        #endregion
    }
}