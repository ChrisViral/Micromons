using System.Threading;
using System.Windows.Forms;

namespace Micromons.Tools
{
    /// <summary>
    /// Various utility methods and class method extensions
    /// </summary>
    public static class Utils
    {
        #region Static methods
        /// <summary>
        /// Clamps an int value between a min (inclusively) and a max (exclusively)
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <param name="n">Value to clamp</param>
        /// <returns>Clamped value</returns>
        public static int Clamp(int min, int max, int n) => n >= min ? (n < max ? n : min) : max - 1;
        #endregion

        #region Extensions
        /// <summary>
        /// Takes an uppercase string and switches all but the first letter to lowercase
        /// </summary>
        /// <param name="s">String to switch to Capital Case</param>
        /// <returns>The string in Capital Case</returns>
        public static string ToCapitalCase(this string s) => s[0] + s.Substring(1).ToLower();

        /// <summary>
        /// Sets the Enabled state of the specified Control object
        /// </summary>
        /// <param name="control">Object to set the state of</param>
        /// <param name="enabled">New Enabled state</param>
        public static void SetEnabled(this Control control, bool enabled) => control.Enabled = enabled;
        #endregion
    }
}
