using System;
using System.Drawing;

namespace Micromons.Simulation
{
    /// <summary>
    /// All the Pokemon types
    /// </summary>
    public enum Type
    {
        NONE        = -1,
        NORMAL      = 0,
        FIRE        = 1,
        WATER       = 2,
        ELECTRIC    = 3,
        GRASS       = 4,
        ICE         = 5,
        FIGHTING    = 6,
        POISON      = 7,
        GROUND      = 8,
        FLYING      = 9,
        PSYCHIC     = 10,
        BUG         = 11,
        ROCK        = 12,
        GHOST       = 13,
        DRAGON      = 14,
        DARK        = 15,
        STEEL       = 16,
        FAIRY       = 17
    }

    /// <summary>
    /// Micromon typing utility class
    /// </summary>
    public static class Typing
    {
        #region Static fields
        /// <summary> 2D type matchup chart </summary>
        private static readonly double[,] matchup;

        /// <summary> Type/Colour associative array </summary>
        private static readonly Color[] colours;

        /// <summary> Random number generator </summary>
        private static readonly Random random;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the data structures for typing and matchup
        /// </summary>
        static Typing()
        {
            //Type matchup chart, vertical are defending types and horizontal attacking types
            matchup = new [,]
            {   // 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17
                { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5, 0.0, 1.0, 1.0, 0.5, 1.0 },   //Normal    
                { 1.0, 0.5, 0.5, 1.0, 2.0, 2.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 0.5, 1.0, 0.5, 1.0, 2.0, 1.0 },   //Fire      
                { 1.0, 2.0, 0.5, 1.0, 0.5, 1.0, 1.0, 1.0, 2.0, 1.0, 1.0, 1.0, 2.0, 1.0, 0.5, 1.0, 1.0, 1.0 },   //Water
                { 1.0, 1.0, 2.0, 0.5, 0.5, 1.0, 1.0, 1.0, 0.0, 2.0, 1.0, 1.0, 1.0, 1.0, 0.5, 1.0, 1.0, 1.0 },   //Electric
                { 1.0, 0.5, 2.0, 1.0, 0.5, 1.0, 1.0, 0.5, 2.0, 0.5, 1.0, 0.5, 2.0, 1.0, 0.5, 1.0, 0.5, 1.0 },   //Grass
                { 1.0, 0.5, 0.5, 1.0, 2.0, 0.5, 1.0, 1.0, 2.0, 2.0, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 0.5, 1.0 },   //Ice
                { 2.0, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 0.5, 1.0, 0.5, 0.5, 0.5, 2.0, 0.0, 1.0, 2.0, 2.0, 0.5 },   //Fighting
                { 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 1.0, 0.5, 0.5, 1.0, 1.0, 1.0, 0.5, 0.5, 1.0, 1.0, 0.0, 2.0 },   //Poison
                { 1.0, 2.0, 1.0, 2.0, 0.5, 1.0, 1.0, 2.0, 1.0, 0.0, 1.0, 0.5, 2.0, 1.0, 1.0, 1.0, 2.0, 1.0 },   //Ground
                { 1.0, 1.0, 1.0, 0.5, 2.0, 1.0, 2.0, 1.0, 1.0, 1.0, 1.0, 2.0, 0.5, 1.0, 1.0, 1.0, 0.5, 1.0 },   //Flying
                { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 2.0, 1.0, 1.0, 0.5, 1.0, 1.0, 1.0, 1.0, 0.0, 0.5, 1.0 },   //Psychic
                { 1.0, 0.5, 1.0, 1.0, 2.0, 1.0, 0.5, 0.5, 1.0, 0.5, 2.0, 1.0, 1.0, 0.5, 1.0, 2.0, 0.5, 0.5 },   //Bug
                { 1.0, 2.0, 1.0, 1.0, 1.0, 2.0, 0.5, 1.0, 0.5, 2.0, 1.0, 2.0, 1.0, 1.0, 1.0, 1.0, 0.5, 1.0 },   //Rock
                { 0.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 1.0, 2.0, 1.0, 0.5, 1.0, 1.0 },   //Ghost
                { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 0.5, 0.0 },   //Dragon
                { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5, 1.0, 1.0, 1.0, 2.0, 1.0, 1.0, 2.0, 1.0, 0.5, 1.0, 0.5 },   //Dark
                { 1.0, 0.5, 0.5, 0.5, 1.0, 2.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 1.0, 1.0, 0.5, 2.0 },   //Steel
                { 1.0, 0.5, 1.0, 1.0, 1.0, 1.0, 2.0, 0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 2.0, 0.5, 1.0 }    //Fairy
            };

            //Colours associated to each type
            colours = new []
            {
                Color.FromArgb(200, 200, 200),  //Normal
                Color.FromArgb(255, 0, 0),      //Fire
                Color.FromArgb(0, 0, 255),      //Water
                Color.FromArgb(255, 255, 0),    //Electric
                Color.FromArgb(0, 255, 0),      //Grass
                Color.FromArgb(100, 100, 255),  //Ice
                Color.FromArgb(153, 37, 33),    //Fighting
                Color.FromArgb(100, 0, 200),    //Poison   
                Color.FromArgb(216, 170, 91),   //Ground
                Color.FromArgb(154, 212, 237),  //Flying
                Color.FromArgb(255, 0, 255),    //Psychic
                Color.FromArgb(150, 255, 100),  //Bug
                Color.FromArgb(142, 110, 45),   //Rock
                Color.FromArgb(41, 26, 79),     //Ghost
                Color.FromArgb(98, 157, 209),   //Dragon
                Color.FromArgb(20, 20, 20),     //Dark
                Color.FromArgb(100, 100, 100),  //Steel
                Color.FromArgb(255, 100, 150)   //Fairy
            };

            random = new Random();
        }
        #endregion

        #region Static methods
        /// <summary>
        /// Calculates the best effectiveness of a Pokemon against another
        /// </summary>
        /// <param name="attacking">Attacking Pokemon</param>
        /// <param name="defending">Defending Pokemon</param>
        /// <returns>The effectiveness of the best attacking move (ranging from 0 to 4)</returns>
        public static double CalculateEffectiveness(Micromon attacking, Micromon defending)
        {
            //Find the best attacking/defending matchup
            return Math.Max(Matchup(attacking.Type1, defending), Matchup(attacking.Type2, defending));
        }

        /// <summary>
        /// Calculates the effectiveness of a particular attacking type against a Pokemon
        /// </summary>
        /// <param name="attacking">Attacking type</param>
        /// <param name="defending">Defending Pokemon</param>
        /// <returns>The effectiveness of the move (ranging from 0 to 4)</returns>
        private static double Matchup(Type attacking, Micromon defending)
        {
            //If attacking type is none, return 0
            if (attacking == Type.NONE) { return 0d; }
            
            //Calculate matchup for this attacking/defending type setup
            double effectiveness = matchup[(int)attacking, (int)defending.Type1];
            if (defending.Type2 != Type.NONE) { effectiveness *= matchup[(int)attacking, (int)defending.Type2]; }
            return effectiveness;
        }

        /// <summary>
        /// Returns a set of new random types for a new Micromon
        /// </summary>
        /// <returns>One or two randomly selected Pokemon types</returns>
        public static TypePair RandomTyping()
        {
            //Generate a random Type for the first type and leave the second blank
            Type t1 = (Type)random.Next(17), t2 = Type.NONE;

            //Give a secondary type around three quarters of the time
            if (random.NextDouble() < 0.75)
            {
                //Generate a random type until it is different from the first one
                do
                {
                    t2 = (Type)random.Next(17);
                }
                while (t1 == t2);

                //Order the types by number (prevents having semi dupplicate setups such as T1/T2 and T2/T1)
                if (t1 > t2)
                {
                    Type t = t1;
                    t1 = t2;
                    t2 = t;
                }
            }

            //Return resulting TypePair
            return new TypePair(t1, t2);
        }

        /// <summary>
        /// Gets the colour associated to the given types
        /// </summary>
        /// <param name="t1">First Pokemon type</param>
        /// <param name="t2">Second Pokemon type</param>
        /// <returns>Colour obtained from the types. If the Micromon is multityped, the colour is averaged</returns>
        public static Color TypeColour(Type t1, Type t2)
        {
            //Blend colours if dual type
            Color c1 = colours[(int)t1];
            return t2 != Type.NONE ? Blend(c1, colours[(int)t2]) : c1;
        }

        /// <summary>
        /// Does an average blend of two colours
        /// </summary>
        /// <param name="a">First colour to blend</param>
        /// <param name="b">Second colour to blend</param>
        /// <returns>Blended colour</returns>
        private static Color Blend(Color a, Color b)
        {
            //Simple average of each component of the colours
            return Color.FromArgb((int)((a.R + b.R) / 2d), (int)((a.G + b.G) / 2d), (int)((a.B + b.B) / 2d));
        }
        #endregion
    }
}

