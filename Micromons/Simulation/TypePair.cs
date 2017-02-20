using System;
using Micromons.Tools;

namespace Micromons.Simulation
{
    /// <summary>
    /// Represents a pair of Micromon Types
    /// </summary>
    public struct TypePair : IEquatable<TypePair>
    {
        #region Fields
        /// <summary>  First type </summary>
        public readonly Type t1;
        /// <summary> Second type </summary>
        public readonly Type t2;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new TypePair from two Types
        /// </summary>
        /// <param name="t1">First type</param>
        /// <param name="t2">Second type</param>
        public TypePair(Type t1, Type t2)
        {
            this.t1 = t1;
            this.t2 = t2;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Gets a correctly formatted representation of the Pair
        /// </summary>
        /// <returns>String representation of the TypePair</returns>
        public override string ToString()
        {
            string result = this.t1.ToString().ToCapitalCase();
            if (this.t2 != Type.NONE) { result += "/" + this.t2.ToString().ToCapitalCase(); }

            return result;
        }

        /// <summary>
        /// Gets the HashCode for this TypePair, based on the underlying values of the Types
        /// </summary>
        /// <returns>HashCode of this instance</returns>
        public override int GetHashCode() => (int)this.t1 + 1 + (((int)this.t2 + 1) * 18);

        /// <summary>
        /// Tests to see if the passed object is a TypePair and equal to the instance
        /// </summary>
        /// <param name="obj">Object to test for equality</param>
        /// <returns>True if they are of the same type and equal</returns>
        public override bool Equals(object obj) => obj is TypePair && Equals((TypePair)obj);

        /// <summary>
        /// Tests to see if both TypePairs are equal and have the same Types
        /// </summary>
        /// <param name="other">TypePair to test for Equality</param>
        /// <returns>True if both TypePairs are equal</returns>
        public bool Equals(TypePair other) => this == other;
        #endregion

        #region Operators
        /// <summary>
        /// Tests the equality of both TypePair objects
        /// </summary>
        /// <param name="a">Left object</param>
        /// <param name="b">Right object</param>
        /// <returns>True if both TypePairs are equal</returns>
        public static bool operator ==(TypePair a, TypePair b) => a.t1 == b.t1 && a.t2 == b.t2;

        /// <summary>
        /// Tests the inequality of both TypePair objects
        /// </summary>
        /// <param name="a">Left object</param>
        /// <param name="b">Right object</param>
        /// <returns>True if both are equal</returns>
        public static bool operator !=(TypePair a, TypePair b) => a.t1 != b.t1 || a.t2 != b.t2;
        #endregion
    }
}