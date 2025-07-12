using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public static class CardDataParser
{
  public static T ParseEnum<T>(string value) where T : struct, Enum =>
    Enum.TryParse(value, true, out T result) ? result : default;

  public static List<PokemonType> ParsePokemonTypes(Variant types)
  {
    return types
      .AsGodotArray<string>()
      .ToList()
      .ConvertAll(ParseEnum<PokemonType>);
  }

  public static List<Ability> ParsePokemonAbilities(Variant abilities)
  {
    return abilities
      .AsGodotArray<Dictionary>()
      .ToList()
      .ConvertAll(ability => new Ability
      {
        Name = ability["name"].AsString(),
        Text = ability["text"].AsString(),
        Type = ability["type"].AsString()
      });
  }

  public static List<Attack> ParsePokemonAttacks(Variant attacks)
  {
    return attacks
    .AsGodotArray<Dictionary>()
    .ToList()
    .ConvertAll(attack =>
      new Attack
      {
        Name = attack["name"].AsString(),
        Text = attack["text"].AsString(),
        Cost = attack["cost"]
        .AsGodotArray<string>()
        .ToList()
        .ConvertAll(ParseEnum<BasicEnergy>),
        ConvertedEnergyCost = attack["convertedEnergyCost"].AsInt16(),
        Damage = attack["damage"].AsString(),
        BaseDamage = attack["baseDamage"].AsInt16(),
        Operation = ParseEnum<Operation>(attack["damageOperation"].AsString()),
      });
  }

  public static List<Weakness> ParsePokemonWeaknesses(Variant weaknesses)
  {
    return weaknesses
      .AsGodotArray<Dictionary>()
      .ToList()
      .ConvertAll(weakness =>
        new Weakness
        {
          Type = ParseEnum<PokemonType>(weakness["type"].AsString()),
          Value = weakness["weaknessValue"].AsInt16(),
          Operation = ParseEnum<Operation>(weakness["weaknessOperation"].AsString()),
        }
      );
  }

  public static CardSet ParseCardSet(Variant set)
  {
    Dictionary setData = set.AsGodotDictionary();
    string setId = setData["id"].AsString();
    string setName = setData["name"].AsString();
    string setSeries = setData["series"].AsString();
    short setPrintedTotal = setData["printedTotal"].AsInt16();
    short setTotal = setData["total"].AsInt16();
    string setReleaseDate = setData["releaseDate"].AsString();
    return new CardSet
    {
      Id = setId,
      Name = setName,
      Series = setSeries,
      PrintedTotal = setPrintedTotal,
      Total = setTotal,
      ReleaseDate = setReleaseDate
    };
  }
}