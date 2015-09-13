﻿using System;
using System.Linq;
using BluffinMuffin.HandEvaluator.Enums;
using BluffinMuffin.HandEvaluator.Exceptions;

namespace BluffinMuffin.HandEvaluator
{
    public class PlayingCard : IComparable<PlayingCard>
    {
        public static readonly string[] VALUES = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        public static readonly string[] SUITS = { "C", "D", "H", "S" };

        public NominalValueEnum Value { get; }
        public SuitEnum Suit { get; set; }

        public PlayingCard(NominalValueEnum value, SuitEnum suit)
        {
            if (!Enum.IsDefined(typeof(NominalValueEnum), value))
                throw new NotInEnumScopeException<NominalValueEnum>(value);

            if (!Enum.IsDefined(typeof(SuitEnum), suit))
                throw new NotInEnumScopeException<SuitEnum>(suit);

            Value = value;
            Suit = suit;
        }

        public PlayingCard(string stringRepresentation)
        {
            if (stringRepresentation.Length < 2 || stringRepresentation.Length > 3)
                throw new InvalidStringRepresentationException(stringRepresentation, "Length");
            var value = stringRepresentation.Remove(stringRepresentation.Length - 1).ToUpper();
            if (!VALUES.Contains(value))
                throw new InvalidStringRepresentationException(stringRepresentation,"Nominal Value");
            Value = (NominalValueEnum) Array.IndexOf(VALUES, value);

            var suit = stringRepresentation.Substring(stringRepresentation.Length - 1, 1).ToUpper();
            if (!SUITS.Contains(suit))
                throw new InvalidStringRepresentationException(stringRepresentation, "Suit");
            Suit = (SuitEnum)Array.IndexOf(SUITS, suit);
        }
        public override string ToString()
        {
            return $"{VALUES[(int) Value]}{SUITS[(int) Suit]}";
        }

        public virtual int CompareTo(PlayingCard other)
        {
            return ((int)Value).CompareTo((int)other.Value);
        }
    }
}
