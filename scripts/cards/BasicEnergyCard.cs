using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;

#nullable enable

public enum BasicEnergy
{
  Colorless,
  Grass,
  Fire,
  Water,
  Lightning,
  Psychic,
  Fighting,
}

public class BasicEnergyCardProps : CardProps
{
  public BasicEnergy Type { get; init; }
}

public partial class BasicEnergyCard : Card
{
  
  public BasicEnergy Type { get; init; }

  public static BasicEnergyCard Instantiate(BasicEnergyCardProps props)
  {
    PackedScene scene = GD.Load<PackedScene>("res://scenes/cards/basic_energy_card.tscn");
    BasicEnergyCard card = scene.Instantiate<BasicEnergyCard>();

    card.textureFront = ResourceLoader.Load<Texture2D>($"res://assets/cards/{props.CardSet.Id}/images/{props.Id}.png");

    foreach (PropertyInfo prop in typeof(BasicEnergyCardProps).GetProperties())
    {
      PropertyInfo? cardProp = typeof(BasicEnergyCard).GetProperty(prop.Name);
      if (cardProp != null && cardProp.CanWrite)
      {
        cardProp.SetValue(card, prop.GetValue(props));
      }
    }

    return card;
  }
}