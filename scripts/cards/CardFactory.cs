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

  public static Card Create(string id)
  {

    if (!_base1cardData.ContainsKey(id))
    {
      GD.PrintErr("CardFactory: Create: Card ID not found: ", id);
      return null;
    }

    Dictionary cardData = _base1cardData[id].AsGodotDictionary();

    string supertype = cardData["supertype"].AsString();
    return supertype switch
    {
      nameof(CardSupertype.Pokemon) => PokemonCard.Instantiate(CreatePokemonCardProps(cardData)),
      nameof(CardSupertype.Trainer) => InstantiatePokemonCard(cardData),
      nameof(CardSupertype.Energy) => InstantiatePokemonCard(cardData),
      _ => null,
    };
  }

  private static Card InstantiatePokemonCard(Dictionary cardData)
  {
    PackedScene cardScene = GD.Load<PackedScene>("res://scenes/cards/pokemon_card.tscn");
    PokemonCard card = cardScene.Instantiate<PokemonCard>();

    return card;
  }

  private static PokemonCardProps CreatePokemonCardProps(Dictionary cardData)
  {
    Dictionary setData = cardData["set"].AsGodotDictionary();

    // card properties
    string id = cardData["id"].AsString();
    string title = cardData["name"].AsString();
    short number = cardData["number"].AsInt16();
    string supertype = cardData["supertype"].AsString();
    List<string> subtypes = cardData["subtypes"].AsGodotArray<string>().ToList();
    short hitPoints = cardData["hp"].AsInt16();
    List<PokemonType> types = cardData["types"]
      .AsGodotArray<string>()
      .ToList()
      .ConvertAll(str =>
      {
        _ = Enum.TryParse(str, true, out PokemonType type);
        return type;
      });
    string evolvesFrom = cardData.ContainsKey("evolvesFrom") ? cardData["evolvesFrom"].AsString() : null;
    string evolvesTo = cardData.ContainsKey("evolvesTo") ? cardData["evolvesTo"].AsString() : null;
    string rarity = cardData["rarity"].AsString();
    List<Ability> abilities = cardData.ContainsKey("abilities")
      ? cardData["abilities"]
          .AsGodotArray<Dictionary>()
          .ToList()
          .ConvertAll(ability => new Ability
          {
            Name = ability["name"].AsString(),
            Text = ability["text"].AsString(),
            Type = ability["type"].AsString()
          })
      : null;
    List<Attack> attacks = cardData["attacks"]
      .AsGodotArray<Dictionary>()
      .ToList()
      .ConvertAll(attack =>
      {
        Enum.TryParse(attack["damageOperation"].AsString(), true, out Operation operation);
        return new Attack
        {
          Name = attack["name"].AsString(),
          Text = attack["text"].AsString(),
          Cost = attack["cost"]
          .AsGodotArray<string>()
          .ToList()
          .ConvertAll(str =>
          {
            _ = Enum.TryParse(str, true, out Energy energy);
            return energy;
          }),
          ConvertedEnergyCost = attack["convertedEnergyCost"].AsInt16(),
          Damage = attack["damage"].AsString(),
          BaseDamage = attack["baseDamage"].AsInt16(),
          Operation = operation,
        };
      });
    List<Weakness> weaknesss = cardData["weaknesses"]
      .AsGodotArray<Dictionary>()
      .ToList()
      .ConvertAll(weakness => {
        _ = Enum.TryParse(weakness["type"].AsString(), true, out PokemonType type);
        _ = Enum.TryParse(weakness["weaknessOperation"].AsString(), true, out Operation operation);
        return new Weakness
        {
          Type = type,
          Value = weakness["weaknessValue"].AsInt16(),
          Operation = operation,
        };
      });

    // set properties
    string setId = setData["id"].AsString();
    string setName = setData["name"].AsString();
    string setSeries = setData["series"].AsString();
    short setPrintedTotal = setData["printedTotal"].AsInt16();
    short setTotal = setData["total"].AsInt16();
    string setReleaseDate = setData["releaseDate"].AsString();


    return new PokemonCardProps
    {
      Id = id,
      Title = title,
      Number = number,
      Supertype = supertype,
      Subtypes = subtypes,
      HitPoints = hitPoints,
      Types = types,
      EvolvesFrom = evolvesFrom,
      EvolvesTo = evolvesTo,
      Rarity = rarity,
      Abilities = abilities,
      Attacks = attacks,
      Weaknesses = weaknesss,
      CardSet = new CardSet
      {
        Id = setId,
        Name = setName,
        Series = setSeries,
        PrintedTotal = setPrintedTotal,
        Total = setTotal,
        ReleaseDate = setReleaseDate
      },
    };
  }
}
