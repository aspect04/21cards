using System;
using System.Collections.Generic;

class CardList
{
    private string[] listOfCards;
    private bool Ace = false;
    private int HandSize;
    private int sumOfHand;

    public CardList()
    {
        listOfCards = new string[7];
        HandSize = 0;
        sumOfHand = 0;
    }

    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= listOfCards.Count())
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }
            return listOfCards[index];
        }
        set
        {
            if (index < 0 || index >= listOfCards.Count())
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }
            HandSize++;
            sumOfHand += getValueofCard(value);
            listOfCards[index] = value;
        }
    }

    public void Add(string card)
    {
        if (card[0] == 'A') Ace = true;
        listOfCards[HandSize] = card;
        HandSize++;
        sumOfHand += getValueofCard(card);

    }
    public void Get(string card)
    {
        if (card[0] == 'A') Ace = true;
        listOfCards[HandSize] = card;
        HandSize++;

    }
    public void Set(int index, string card)
    {   
        if (listOfCards[index] == "A") Ace = false;
        if (card[0] == 'A') Ace = true;
        listOfCards[index] = card;
        if (index == HandSize) HandSize++;
        sumOfHand += getValueofCard(card);

    }
    public void Reset()
    {
        listOfCards = new string[7];
        HandSize = 0;
        sumOfHand = 0;
    }
    public int Size()
    {
        return HandSize;
    }
    public bool ContainsAce()
    {
        return Ace;
    }
    public int getValueofCard(string card)
    {
        if (CardValues.value.ContainsKey(card[0])) return CardValues.value[card[0]];
        else return card[0] - '0';
    }

    public int Sum()
    {
        return sumOfHand;
    }
    
}