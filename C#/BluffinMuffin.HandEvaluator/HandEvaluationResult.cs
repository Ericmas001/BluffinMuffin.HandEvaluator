﻿using System;
using System.Collections.Generic;
using System.Linq;
using BluffinMuffin.HandEvaluator.Enums;

namespace BluffinMuffin.HandEvaluator
{
    public class HandEvaluationResult : IComparable<HandEvaluationResult>
    {
        public HandEnum Hand { get; }

        public List<PlayingCard[]> Cards { get; }

        public Func<HandEvaluationResult, string> ToStringFunc { get; } 

        public HandEvaluationResult(AbstractHandEvaluator evaluator)
        {
            Hand = evaluator.HandType;
            Cards = new List<PlayingCard[]>();
            ToStringFunc = evaluator.ResultToString;
        }
        public int CompareTo(HandEvaluationResult other)
        {
            if (Cards == null || !Cards.Any())
                return -1;

            if (other?.Cards == null || !other.Cards.Any())
                return 1;

            if (Hand != other.Hand)
                return ((int) Hand).CompareTo((int) other.Hand);

            for (var i = 0; i < Math.Max(Cards.Count, other.Cards.Count); ++i)
            {
                if (i >= Cards.Count)
                    return -1;

                if (i >= other.Cards.Count)
                    return 1;

                var equality = Cards[i][0].CompareTo(other.Cards[i][0]);
                if(equality != 0)
                    return equality;
            }
            return 0;
        }

        public override string ToString()
        {
            return ToStringFunc(this);
        }
    }
}
