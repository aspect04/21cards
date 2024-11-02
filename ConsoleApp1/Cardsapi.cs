using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public partial class Cardsapi
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
        

    [JsonPropertyName("deck_id")]
    public string DeckId { get; set; }

    [JsonPropertyName("cards")]
    public Card[] Cards { get; set; }

    [JsonPropertyName("remaining")]
    public long Remaining { get; set; }
}

public partial class Card
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("image")]
    public Uri Image { get; set; }

    [JsonPropertyName("images")]
    public Images Images { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("suit")]
    public string Suit { get; set; }
}

public partial class Images
{
    [JsonPropertyName("svg")]
    public Uri Svg { get; set; }

    [JsonPropertyName("png")]
    public Uri Png { get; set; }
}