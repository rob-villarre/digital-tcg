using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public static class CardFactory
{

  private static readonly Dictionary _base1cardData;

  static CardFactory()
  {
    _base1cardData = GD.Load<Json>("res://assets/cards/base1/data.json").Data.AsGodotDictionary();
  }
  
  private static readonly System.Collections.Generic.Dictionary<string, Func<Dictionary, CardView>> _cardCreators = new()
  {
    [nameof(CardSupertype.Pokemon)] = data => PokemonCardView.Instantiate(CreatePokemonCardProps(data)),
    [nameof(CardSupertype.Trainer)] = data => TrainerCardView.Instantiate(CreateTrainerCardProps(data)),
    ["Energy.Basic"] = data => BasicEnergyCardView.Instantiate(CreateBasicEnergyCardProps(data)),
    ["Energy.Special"] = data => SpecialEnergyCardView.Instantiate(CreateSpecialEnergyCardProps(data))
  };

  private static string GetCardKey(Dictionary cardData)
  {
    string supertype = cardData["supertype"].AsString();
    if (supertype == nameof(CardSupertype.Energy))
    {
      var subtypes = cardData["subtypes"].AsGodotArray<string>();
      return subtypes.Contains("Basic") ? "Energy.Basic" : "Energy.Special";
    }
    return supertype;
  }

  public static CardView Create(string id)
  {
    var cardData = _base1cardData[id].AsGodotDictionary();
    string key = GetCardKey(cardData);
    return _cardCreators.TryGetValue(key, out var creator) ? creator(cardData) : null;
  }

  private static CardProps CreateBaseCardProps(Dictionary cardData)
  {
    return new CardProps
    {
      Id = cardData["id"].AsString(),
      Title = cardData["name"].AsString(),
      Number = cardData["number"].AsInt16(),
      Supertype = cardData["supertype"].AsString(),
      CardSet = CardDataParser.ParseCardSet(cardData["set"]),
      Subtypes = cardData.ContainsKey("subtypes") ? cardData["subtypes"].AsGodotArray<string>().ToList() : null,
      Rarity = cardData.ContainsKey("rarity") ? cardData["rarity"].AsString() : null,
    };
  }

  private static TrainerCardProps CreateTrainerCardProps(Dictionary cardData)
  {
    CardProps baseCardProps = CreateBaseCardProps(cardData);
    List<string> rules = cardData["rules"].AsGodotArray<string>().ToList();
    short? hitPoints = cardData.ContainsKey("hp") ? cardData["hp"].AsInt16() : null;

    return new TrainerCardProps
    {
      Id = baseCardProps.Id,
      Title = baseCardProps.Title,
      Number = baseCardProps.Number,
      Supertype = baseCardProps.Supertype,
      CardSet = baseCardProps.CardSet,
      Rarity = baseCardProps.Rarity,
      Rules = rules,
      HitPoints = hitPoints,
    };
  }

  private static SpecialEnergyCardProps CreateSpecialEnergyCardProps(Dictionary cardData)
  {
    CardProps baseCardProps = CreateBaseCardProps(cardData);
    string name = cardData["name"].AsString();
    SpecialEnergy type = CardDataParser.ParseEnum<SpecialEnergy>(name[..name.LastIndexOf(' ')].Replace(" ", ""));
    List<string> rules = cardData["rules"].AsGodotArray<string>().ToList();

    return new SpecialEnergyCardProps
    {
      Id = baseCardProps.Id,
      Title = baseCardProps.Title,
      Number = baseCardProps.Number,
      Supertype = baseCardProps.Supertype,
      Subtypes = baseCardProps.Subtypes,
      CardSet = baseCardProps.CardSet,
      Type = type,
      Rules = rules,
    };
  }

  private static BasicEnergyCardProps CreateBasicEnergyCardProps(Dictionary cardData)
  {
    CardProps baseCardProps = CreateBaseCardProps(cardData);
    string name = cardData["name"].AsString();
    BasicEnergy type = CardDataParser.ParseEnum<BasicEnergy>(name[..name.LastIndexOf(' ')].Replace(" ", ""));

    return new BasicEnergyCardProps
    {
      Id = baseCardProps.Id,
      Type = type,
      Title = baseCardProps.Title,
      Number = baseCardProps.Number,
      Supertype = baseCardProps.Supertype,
      Subtypes = baseCardProps.Subtypes,
      CardSet = baseCardProps.CardSet,
      Rarity = baseCardProps.Rarity,
    };
  }

  private static PokemonCardProps CreatePokemonCardProps(Dictionary cardData)
  {
    CardProps baseCardProps = CreateBaseCardProps(cardData);
    short hitPoints = cardData["hp"].AsInt16();
    List<PokemonType> types = CardDataParser.ParsePokemonTypes(cardData["types"]);
    string evolvesFrom = cardData.ContainsKey("evolvesFrom") ? cardData["evolvesFrom"].AsString() : null;
    string evolvesTo = cardData.ContainsKey("evolvesTo") ? cardData["evolvesTo"].AsString() : null;
    List<Ability> abilities = cardData.ContainsKey("abilities") ? CardDataParser.ParsePokemonAbilities(cardData["abilities"]) : null;
    List<Attack> attacks = CardDataParser.ParsePokemonAttacks(cardData["attacks"]);
    List<Weakness> weaknesses = cardData.ContainsKey("weaknesses") ? CardDataParser.ParsePokemonWeaknesses(cardData["weaknesses"]) : null;

    return new PokemonCardProps
    {
      Id = baseCardProps.Id,
      Title = baseCardProps.Title,
      Number = baseCardProps.Number,
      Supertype = baseCardProps.Supertype,
      Subtypes = baseCardProps.Subtypes,
      HitPoints = hitPoints,
      Types = types,
      EvolvesFrom = evolvesFrom,
      EvolvesTo = evolvesTo,
      Rarity = baseCardProps.Rarity,
      Abilities = abilities,
      Attacks = attacks,
      Weaknesses = weaknesses,
      CardSet = baseCardProps.CardSet,
    };
  }
}
