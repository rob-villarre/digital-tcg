using Godot;
using System;
using System.Collections.Generic;

#nullable enable

public enum CardSupertype
{
  Pokemon,
  Trainer,
  Energy,
}

public enum CardSubtype
{
  BREAK,
  Baby,
  Basic,
  EX,
  GX,
  GoldenrodGameCorner,
  Item,
  LEGEND,
  LevelUp,
  MEGA,
  PokemonTool,
  PokemonToolF,
  RapidStrike,
  Restored,
  RocketsSecretMachine,
  SingleStrike,
  Special,
  Stadium,
  Stage1,
  Stage2,
  Supporter,
  TAGTEAM,
  TechnicalMachine,
  V,
  VMAX,
}

public enum PokemonType
{
  Colorless,
  Darkness,
  Dragon,
  Fairy,
  Fighting,
  Fire,
  Grass,
  Lightning,
  Metal,
  Psychic,
  Water
}

public enum Operation
{
  None,
  Plus,
  Minus,
  Multiply
}

public class Attack
{
  public string Name { get; init; } = string.Empty;
  public string? Text = null;
  public List<BasicEnergy> Cost { get; init; } = [];
  public short ConvertedEnergyCost { get; init; }
  public string Damage { get; init; } = string.Empty;
  public short BaseDamage { get; init; }
  public Operation Operation { get; init; }
}

public class Ability
{
  public string Name { get; init; } = string.Empty;
  public string Text { get; init; } = string.Empty;
  public string Type { get; init; } = string.Empty;
  
}

public class Weakness
{
  public PokemonType Type { get; init; }
  public short Value { get; init; }
  public Operation Operation { get; init; }
}

public class CardProps
{
  public string Id { get; init; } = string.Empty;
  public string Title { get; init; } = string.Empty;
  public short Number { get; init; }
  public string Supertype { get; init; } = string.Empty;
  public List<string>? Subtypes { get; init; }
  public CardSet CardSet { get; init; } = new CardSet();
  public string? Rarity { get; init; }
}

public partial class Card
{
  public string Id { get; init; } = string.Empty;
  public string Title { get; init; } = string.Empty;
  public short Number { get; init; }
  public string Supertype { get; init; } = string.Empty;
  public List<string>? Subtypes { get; init; }
  public CardSet CardSet { get; init; } = new CardSet();
  public string? Rarity { get; init; }

  public Card(CardProps props)
  {
    Id = props.Id;
    Title = props.Title;
    Number = props.Number;
    Supertype = props.Supertype;
    Subtypes = props.Subtypes;
    CardSet = props.CardSet;
    Rarity = props.Rarity;    
  }

}
