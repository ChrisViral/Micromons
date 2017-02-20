using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using Micromons.Tools;

namespace Micromons.Simulation
{
    public class Grid
    {
        /// <summary>
        /// Container value type for the results of probing the surrounding Micromons for potential targets
        /// </summary>
        private struct ProbeResults
        {
            #region Fields
            /// <summary> Inflicted damage </summary>
            public readonly double damage;
            /// <summary> Targeted Micromon </summary>
            public readonly Micromon target;
            #endregion

            #region Constructors
            /// <summary>
            /// Creates a new ProbeResult of the given damage to the specified target
            /// </summary>
            /// <param name="damage">Total damage</param>
            /// <param name="target">Targeted Micromon</param>
            public ProbeResults(double damage, Micromon target)
            {
                this.damage = damage;
                this.target = target;
            }
            #endregion

            #region Overrides
            #if DEBUG
            /// <summary>
            /// Debug info string about this ProbeResult
            /// </summary>
            /// <returns>Debug info</returns>
            public override string ToString()
            {
                return $"D: {this.damage}, X: {this.target.X}, Y: {this.target.Y}";
            }
            #endif
            #endregion
        }

        /// <summary>
        /// Statistics container object for simulation grid analysis
        /// </summary>
        public class TypeStats : IComparable<TypeStats>
        {
            #region Properties
            /// <summary>
            /// Analyzed typing
            /// </summary>
            public TypePair Pair { get; }

            /// <summary>
            /// Total amount present
            /// </summary>
            public int Amount { get; private set; }
            #endregion

            #region Constructors
            /// <summary>
            /// Creates a new TypeStats for the given typing
            /// </summary>
            /// <param name="pair">TypePair analyzed</param>
            public TypeStats(TypePair pair)
            {
                this.Pair = pair;
                this.Amount = 0;    //Set starting amount to zero
            }
            #endregion

            #region Methods
            /// <summary>
            /// Increments the amount present of the current typing
            /// </summary>
            public void Increment() => this.Amount++;

            /// <summary>
            /// Sorting method, compares by total amount
            /// </summary>
            /// <param name="other">Other TypeStats object to compare to</param>
            /// <returns>-1 is this object comes before, 1 if it comes after, or 0 if both are equal</returns>
            public int CompareTo(TypeStats other) => other.Amount.CompareTo(this.Amount);
            #endregion
        }

        #region Fields
        /// <summary> 2D array structure containing the simulation </summary>
        private readonly Micromon[,] grid;
        /// <summary> LinkedList containing the ordered Micromons by attack order (speed) </summary>
        private readonly LinkedList<Micromon> attackList;
        #endregion

        #region Properties
        /// <summary>
        /// Size of one side of the simulation Grid
        /// </summary>
        public int Size { get; }
        #endregion

        #region Indexers
        /// <summary>
        /// Finds Micromons by coordinates in the Grid
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>The Micromon at the specified coordinates</returns>
        public Micromon this[int x, int y] => this.grid[y, x];
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new Grid of the specified size
        /// </summary>
        /// <param name="size">Size of the simulation Grid</param>
        /// <param name="worker">BackgroundWorker to report progress to</param>
        public Grid(int size, BackgroundWorker worker)
        {
            //Initialize data structures
            this.Size = size;
            this.grid = new Micromon[size, size];
            List<Micromon> list = new List<Micromon>(size * size);

            //Loop through all coordinates
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    //Add new Micromon to each location
                    Micromon mon = new Micromon(x, y);
                    this.grid[y, x] = mon;
                    list.Add(mon);
                    worker.ReportProgress(0);
                }
            }

            //Sort Micromons by speed and setup LinkedList
            list.Sort();
            this.attackList = new LinkedList<Micromon>(list);
            Micromon.SetupLinkedList(this.attackList);
        }
        #endregion
        
        #region Static methods
        /// <summary>
        /// Calculates potential damage whith a specified attacking and defending Micromon
        /// </summary>
        /// <param name="attacker">Attacking Micromon</param>
        /// <param name="defender">Defending Micromon</param>
        /// <returns>The results of the simulated attack</returns>
        private static ProbeResults CalculateDamage(Micromon attacker, Micromon defender)
        {
            //Damage shoud not be any lower than 1
            return new ProbeResults(Math.Max(1d, (attacker.Attack - (defender.Defense / 2d)) * Typing.CalculateEffectiveness(attacker, defender)), defender);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Simulates a full round of attack and defending throughout the whole simulation Grid
        /// </summary>
        /// <param name="worker">BackgroundWorker to report the progress to</param>
        public void Simulate(BackgroundWorker worker)
        {
            //Loop through the full list
            for (LinkedListNode<Micromon> n = this.attackList.First; n != null; n = n.Next)
            {
                Micromon attacker = n.Value;

                //Find weakest neighbouring Micromon to attack
                ProbeResults result = FindWeakestNeighbour(attacker);
                Micromon target = result.target;

                //If there is a potential target, attack it, if it succumbs, take over it's spot
                if (target != null && !target.Defend(result.damage)) { n = target.TakenOver(attacker); }
                
                //Report progress (increment bar)
                worker.ReportProgress(0);
            }
        }

        /// <summary>
        /// Find weakest neighbouring Micromon
        /// </summary>
        /// <param name="attacker">Attacking Micromon</param>
        /// <returns>The attack results on the weakest neighbouring Micromon, if all neighbours are of the same type, the default value is passed</returns>
        private ProbeResults FindWeakestNeighbour(Micromon attacker)
        {
            //Get all four potential targets
            Micromon[] targets = new Micromon[4];
            targets[0] = this[Utils.Clamp(0, this.Size, attacker.X - 1), attacker.Y];
            targets[1] = this[Utils.Clamp(0, this.Size, attacker.X + 1), attacker.Y];
            targets[2] = this[attacker.X, Utils.Clamp(0, this.Size, attacker.Y - 1)];
            targets[3] = this[attacker.X, Utils.Clamp(0, this.Size, attacker.Y + 1)];

            //Filter out invalid targets
            List<Micromon> valid = new List<Micromon>(targets.Where(m => attacker.Pair != m.Pair));

            //If no valid targets available, return default value
            if (valid.Count == 0) { return new ProbeResults(); }

            //Find the valid target with the highest possible damage
            ProbeResults highest = CalculateDamage(attacker, valid[0]);
            for (int i = 1; i < valid.Count; i++)
            {
                ProbeResults current = CalculateDamage(attacker, valid[i]);
                if (current.damage > highest.damage) { highest = current; }
            }

            //Return the highest damage
            return highest;
        }

        /// <summary>
        /// Updates the passed Bitmap to the current simulation state
        /// </summary>
        /// <param name="image">Image to update</param>
        public void UpdateImage(Bitmap image)
        {
            //Lock image data for writing
            BitmapData data = image.LockBits(new Rectangle(0, 0, MainForm.imageSize, MainForm.imageSize), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            //Initialize data array
            byte[] bytes = new byte[MainForm.dataSize];

            //Loop through all Micromons and set pixels
            int index = 0;
            for (int y = 0; y < this.Size; y++)
            {
                for (int j = 0; j < MainForm.pixelWidth; j++)
                {
                    for (int x = 0; x < this.Size; x++)
                    {
                        Color pixel = this[x, y].Colour;
                        for (int i = 0; i < MainForm.pixelWidth; i++)
                        {
                            //Pixels are inverted (BGR, not RGB)
                            bytes[index++] = pixel.B;
                            bytes[index++] = pixel.G;
                            bytes[index++] = pixel.R;
                        }
                    }
                }
            }

            //Copy new image data to Bitmap data location, then unlock image
            Marshal.Copy(bytes, 0, data.Scan0, bytes.Length);
            image.UnlockBits(data);
        }

        /// <summary>
        /// Analyze simulation Grid for typing statistics
        /// </summary>
        /// <returns>The list of all TypeStats, sorted by incidence amount</returns>
        public List<TypeStats> AnalyzeGrid()
        {
            //Setup lookup dictionnary
            Dictionary<TypePair, TypeStats> data = new Dictionary<TypePair, TypeStats>();

            //Loop through all Micromons in the simulation
            foreach (Micromon m in this.attackList)
            {
                //Get or create TypeStats dictionary value
                TypeStats stats;
                if (!data.TryGetValue(m.Pair, out stats))
                {
                    stats = new TypeStats(m.Pair);
                    data.Add(m.Pair, stats);
                }

                //Increment incidence
                stats.Increment();
            }

            //Keep only TypeStats values, sort them, then return them
            List<TypeStats> results = new List<TypeStats>(data.Values);
            results.Sort();
            return results;
        }
        #endregion
    }
}
