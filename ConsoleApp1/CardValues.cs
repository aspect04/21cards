using System;
using System.Collections.Generic;

public static class CardValues
{
    public static readonly Dictionary<char, short> value = new Dictionary<char, short>(capacity: 5)
    {
        { '0', 10 },
        { 'J', 11 },
        { 'Q', 12 },
        { 'K', 13 },
        { 'A', 14 }
    };
    public static readonly Dictionary<Char, String> cardNames = new Dictionary<Char, String>(capacity: 9)
    {
        { '0', "10" },
        { 'J', "JACK" },
        { 'Q', "QUEEN" },
        { 'K', "KING" },
        { 'A', "ACE" },
        { 'H', " of HEARTS"},
        { 'S', " of SPADES"},
        { 'D', " of DIAMONDS"},
        { 'C', " of CLUBS"},
    };
}

    