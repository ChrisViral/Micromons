using System;
using System.Collections.Generic;
using System.Drawing;

namespace Micromons.Simulation
{
    /// <summary>
    /// A pixel wide Pokemon simulation element
    /// </summary>
    public class Micromon : IComparable<Micromon>
    {
        #region Static fields
        /// <summary> Random number generator for Micromon generation </summary>
        private static readonly Random random = new Random();
        /// <summary> Attack order LinkedList for the latest simulation </summary>
        private static LinkedList<Micromon> list;
        #endregion

        #region Fields
        /// <summary> LinkedList node of the current Micromon </summary>
        private LinkedListNode<Micromon> node;
        #endregion

        #region Properties
        /// <summary>
        ///Total remaining health
        /// </summary>
        public double Health { get; private set; }

        /// <summary>
        /// Attack value
        /// </summary>
        public double Attack { get; private set; }

        /// <summary>
        /// Defense value
        /// </summary>
        public double Defense { get; private set; }

        /// <summary>
        /// Attack speed
        /// </summary>
        public byte Speed { get; private set; }

        /// <summary>
        /// Primary type
        /// </summary>
        public Type Type1 { get; private set; }

        /// <summary>
        /// Secondary type
        /// </summary>
        public Type Type2 { get; private set; }

        /// <summary>
        /// Current TypePair
        /// </summary>
        public TypePair Pair { get; private set; }

        /// <summary>
        /// Types Colour
        /// </summary>
        public Color Colour { get; private set; }

        /// <summary>
        /// Simulation grid X coordinate
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Simulation grid Y coordinate
        /// </summary>
        public int Y { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new Micromon at the passed location in the simulation Grid
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Micromon(int x, int y)
        {
            //Set coordinates
            this.X = x;
            this.Y = y;

            //Set Micromon typing
            this.Pair = Typing.RandomTyping();
            this.Type1 = this.Pair.t1;
            this.Type2 = this.Pair.t2;
            this.Colour = Typing.TypeColour(this.Type1, this.Type2);

            //Set Micrmon stats
            byte[] stats = new byte[3];
            random.NextBytes(stats);
            Array.Sort(stats);
            this.Speed = stats[0];
            this.Attack = stats[1] - this.Speed;
            this.Defense = stats[2] - this.Attack;
            this.Health = 355d - this.Attack;       //100 is added to the base health

        }
        #endregion

        #region Static methods
        /// <summary>
        /// Initializes the LinkedList setup and sets each Micrmon's LinkedList Node correctly.
        /// </summary>
        /// <param name="attackList">Speed sorted LinkedList of the simulation</param>
        internal static void SetupLinkedList(LinkedList<Micromon> attackList)
        {
            //Setup list and assign to each Micromon it's node
            list = attackList;
            for (LinkedListNode<Micromon> n = list.First; n != null; n = n.Next)
            {
                n.Value.node = n;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lets a Micrmon be taken over by an attacker and transforms it into that attacker
        /// </summary>
        /// <param name="attacker">Attacking Micromon</param>
        /// <returns>The new "current" Node after displacement</returns>
        public LinkedListNode<Micromon> TakenOver(Micromon attacker)
        {
            //Buff attacker's health and attack values
            attacker.Health += 100d;
            attacker.Attack++;

            //Copy attacker's stats and typing, and reduce defense
            this.Health = attacker.Health;
            this.Attack = attacker.Attack;
            this.Defense--;
            this.Speed = attacker.Speed;
            this.Type1 = attacker.Type1;
            this.Type2 = attacker.Type2;
            this.Pair = attacker.Pair;
            this.Colour = attacker.Colour;

            //Move the overtaken Micromon's node right after the attacker and return it
            list.Remove(this.node);
            list.AddAfter(attacker.node, this.node);
            return this.node;
        }

        /// <summary>
        /// Endures an attack of the given total damage
        /// </summary>
        /// <param name="damage">Damage to inflict</param>
        /// <returns>True if the Micromon survives, false otherwise</returns>
        public bool Defend(double damage)
        {
            this.Health -= damage;
            return this.Health > 0d;
        }
        
        /// <summary>
        /// Sorting method, compares by Micromon speed
        /// </summary>
        /// <param name="other">Other Micromon object to compare to</param>
        /// <returns>-1 is this Micromon is faster, 1 if it's slower, or 0 if both are equal</returns>
        public int CompareTo(Micromon other) => other.Speed.CompareTo(this.Speed);
        #endregion

        #region Overrides
        #if DEBUG
        /// <summary>
        /// Debug info string about this Micromon
        /// </summary>
        /// <returns>Debug info</returns>
        public override string ToString()
        {
            return $"S: {this.Speed}, H: {this.Health}, A: {this.Attack}, D: {this.Defense}, T1: {this.Type1}, T2: {this.Type2}";
        }
        #endif
        #endregion
    }
}
