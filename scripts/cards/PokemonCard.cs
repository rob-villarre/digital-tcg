using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;
using Godot.Collections;

#nullable enable

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
  public List<Energy> Cost { get; init; } = [];
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

public class PokemonCardProps
{
  public string Id { get; init; } = string.Empty;
  public string Title { get; init; } = string.Empty;
  public short Number { get; init; }
  public string Supertype { get; init; } = string.Empty;
  public List<string> Subtypes { get; init; } = [];
  public CardSet CardSet { get; init; } = new CardSet();

  public short HitPoints { get; init; }
  public List<PokemonType> Types { get; init; } = [];
  public string? EvolvesFrom { get; init; }
  public string? EvolvesTo { get; init; }
  public string Rarity { get; init; } = string.Empty;
  public List<Ability> Abilities { get; init; } = [];
  public List<Attack> Attacks { get; init; } = [];
  public List<Weakness> Weaknesses { get; init; } = [];
}

public partial class PokemonCard : Card
{

  public string Id { get; init; } = string.Empty;
  public string Title { get; init; } = string.Empty;
  public short Number { get; init; }
  public string Supertype { get; init; } = string.Empty;
  public List<string> Subtypes { get; init; } = [];
  public CardSet CardSet { get; init; } = new CardSet();

  public short HitPoints { get; init; }
  public List<PokemonType> Types { get; init; } = [];
  public string? EvolvesFrom { get; init; }
  public string? EvolvesTo { get; init; }
  public string Rarity { get; init; } = string.Empty;
  public List<Ability> Abilities { get; init; } = [];
  public List<Attack> Attacks { get; init; } = [];
  public List<Weakness> Weaknesses { get; init; } = [];

  public override string ToString()
  {
    return $"PokemonCard(Id: {Id}, Title: {Title})";
  }

  public static PokemonCard Instantiate(PokemonCardProps props)
  {
    PackedScene scene = GD.Load<PackedScene>("res://scenes/cards/pokemon_card.tscn");
    PokemonCard card = scene.Instantiate<PokemonCard>();

    card.textureFront = ResourceLoader.Load<Texture2D>($"res://assets/cards/{props.CardSet.Id}/images/{props.Id}.png");

    foreach (PropertyInfo prop in typeof(PokemonCardProps).GetProperties())
    {
      PropertyInfo? cardProp = typeof(PokemonCard).GetProperty(prop.Name);
      if (cardProp != null && cardProp.CanWrite)
      {
        cardProp.SetValue(card, prop.GetValue(props));
      }
    }

    return card;
  }

}
