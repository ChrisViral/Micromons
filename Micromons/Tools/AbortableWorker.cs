using System.ComponentModel;
using System.Threading;

/* This Micromons simulation was created by Christophe Savard (stupid_chris) and is licensed
 * licensed under CC-BY-SA 3.0 Unported. The entire credit for the original idea and simulation
 * code goes to Reddit user /u/Morning_Fresh, none of this would have been possible without
 * his original work and idea. */

namespace Micromons.Tools
{
    /// <summary>
    /// An abortable version of the regular BackgroundWorker
    /// </summary>
    public class AbortableWorker : BackgroundWorker
    {
        #region Fields
        /// <summary> Current working thread </summary>
        private Thread currentThread;
        #endregion

        #region Overrides
        /// <summary>
        /// Regular BackgroundWorker work method, listens to ThreadAbortExceptions and acts as a cancellation
        /// </summary>
        /// <param name="e">Work event arguments</param>
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            //Set current working thread
            this.currentThread = Thread.CurrentThread;

            try
            {
                //Do work as usual
                base.OnDoWork(e);
            }
            catch (ThreadAbortException)    //If thread aborted
            {
                //Cancel worker and prevent abort propagation
                e.Cancel = true;
                Thread.ResetAbort();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Forces the abortion of the BackgroundWorker thread
        /// </summary>
        public void Abort()
        {
            //If currently working
            if (this.currentThread != null && this.IsBusy)
            {
                //Abort thread and set to null
                this.currentThread.Abort();
                this.currentThread = null;
            }
        }
        #endregion
    }
}
