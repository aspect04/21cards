// See https://aka.ms/new-console-template for more information

using System;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Numerics;
using System.Net.Security;
using System.Reflection.PortableExecutable;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Specialized;
using System.Collections;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;


CardList dealersCards = new CardList();
CardList playersCards = new CardList();

string deckid = "new";
int numberofcardsdrawn = 4;
string playAgain = "";
bool dealing = true;

Console.WriteLine("Welcome to Blackjack!");
Console.WriteLine("Let's Play!");

do 
{
    do 
    {
        testing();
        await cardsapi(numberofcardsdrawn);
        getCards(dealing);
        testing();
        if (WinLose(playersCards) > 0)
        {
            Console.Write("PLAYER WIN!! ");
            break;
        }
        else if (WinLose(playersCards) < 0)
        {
            Console.Write("PLAYER BUST!! ");
            break;
        }
        else if (WinLose(playersCards) == 1)
        {
            Console.Write("PLAYER BLACKJACK!! ");
            break;
        }
        if (WinLose(dealersCards) > 0)
        {
            Console.Write("DEALER WIN!! ");
            break;
        }
        else if (WinLose(dealersCards) < 0)
        {
            Console.Write("DEALER BUST!! ");
            break;
        }
        
        Console.Write("Do you want to hit or stand: ");

        playAgain = Console.ReadLine();

        numberofcardsdrawn = 1;

    } while (playAgain != "stand");

    dealing = false;

    // dealer plays if player didnt win
    if (playAgain == "stand")
    {
        gameLogic();
        getCards(dealing);
        standWinLose();
        
    }
    playersCards.Reset();
    dealersCards.Reset();
    Console.Write("Do you want to play again (s to stop): ");

} while(Console.ReadLine() != "s");


async Task cardsapi(int cardsToDraw) 
{
        
    string url = $"https://www.deckofcardsapi.com/api/deck/{deckid}/draw/?count={cardsToDraw}";
   
    using HttpClient client = new HttpClient();

    try
    {
        HttpResponseMessage response = await client.GetAsync(url);
        
        // Ensure the response is successful (status code 200-299)
        response.EnsureSuccessStatusCode();

        // Read the response content as a string
        string jsonResponse = await response.Content.ReadAsStringAsync();

        Cardsapi cards = JsonSerializer.Deserialize<Cardsapi>(jsonResponse);
        
        // Output the deserialized object and add to decks
        deckid = cards.DeckId;
        
        // if hit only add to players
        addCards(playAgain, cards);
        
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
    
}

// runs when player choses to stand, dealer hand is under 17 so dealer draws another card until at/above 17
async Task gameLogic()
{
    // gamelogic for blackjack dealer bot hit or stand
    while (dealersCards.Sum() < 17)
    await cardsapi(numberofcardsdrawn);
}

//
int WinLose(CardList list) 
{
    if (list.Sum().CompareTo(21) == 0) return 1;
    else if (list.Sum().CompareTo(21) > 0) return -1;
    else return 0;
    
}

// return true if player wins, false if dealer wins
void standWinLose()
{

    if (dealersCards.Sum() > 21) 
    {   
        Console.WriteLine("DEALER BUST!!");
    }
    else if (dealersCards.Sum() == 21) 
    {
        Console.WriteLine("DEALER BLACKJACK!!");
    }
    int dealerdiff = 21 - dealersCards.Sum();
    int playerdiff = 21 - playersCards.Sum();
    if (playerdiff == dealerdiff) Console.WriteLine("TIE");
    else if (dealerdiff > playerdiff) Console.WriteLine("PLAYER WINS!!");
    else Console.WriteLine("DEALER WINS!!");
}

string getDealersCards(bool dealing) 
{
    var joinedNames = "";
    for (int i = 0; i < dealersCards.Size(); i++)
    {
        if (i == 0 && dealing == true) joinedNames += "Hidden Card";
        else if (i == 0 && dealing == false) joinedNames += printCorrectCard(dealersCards[i]);
        else joinedNames += ", " + printCorrectCard(dealersCards[i]);
        
    }
    return joinedNames;
}

string getPlayersCards() 
{
    var joinedNames = "";
    for (int i = 0; i < playersCards.Size(); i++)
    {
        if (i == 0) joinedNames += printCorrectCard(playersCards[i]);
        else joinedNames += ", " + printCorrectCard(playersCards[i]);

    }
    return joinedNames;
}

string printCorrectCard(string code)
{
    string cardName = "";
    for (int i = 0; i < code.Length; i++) 
    {
        if (CardValues.cardNames.ContainsKey(code[i])) cardName += CardValues.cardNames[code[i]];
        else cardName += code[0].ToString();
        
    }
    return cardName;
}

void getCards(bool dealing)
{
    Console.Write("Dealer's Cards: ");

    Console.WriteLine(getDealersCards(dealing));

    Console.Write("Your Cards: ");

    Console.WriteLine(getPlayersCards());
}
void addCards(string playagain, Cardsapi cards)
{
    if (playagain == "hit")
    {
        playersCards[playersCards.Size()] = cards.Cards[0].Code;
    }
    else if (playagain == "stand")
    {
        dealersCards[dealersCards.Size()] = cards.Cards[0].Code;
    }
    else
    {
        for (int i = 0; i < cards.Cards.Length; i++) 
        {
            playersCards[playersCards.Size()] = cards.Cards[i].Code;
            i++;
            dealersCards[dealersCards.Size()] = cards.Cards[i].Code;
        }
    }
}
void testing()
{
    Console.WriteLine("playersum: "+playersCards.Sum());
    Console.WriteLine("dealersum: "+dealersCards.Sum());
}
