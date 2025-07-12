using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;

#nullable enable

public enum SpecialEnergy
{
  DoubleColorless,
}

public class SpecialEnergyCardProps : CardProps
{
  public SpecialEnergy Type { get; init; }
  public List<string> Rules { get; init; } = [];
}

public partial class SpecialEnergyCard : Card
{

  public SpecialEnergy Type { get; init; }
  public List<string> Rules { get; init; } = [];
  
  public static SpecialEnergyCard Instantiate(SpecialEnergyCardProps props)
  {
    PackedScene scene = GD.Load<PackedScene>("res://scenes/cards/special_energy_card.tscn");
    SpecialEnergyCard card = scene.Instantiate<SpecialEnergyCard>();

    card.textureFront = ResourceLoader.Load<Texture2D>($"res://assets/cards/{props.CardSet.Id}/images/{props.Id}.png");

    foreach (PropertyInfo prop in typeof(BasicEnergyCardProps).GetProperties())
    {
      PropertyInfo? cardProp = typeof(SpecialEnergyCard).GetProperty(prop.Name);
      if (cardProp != null && cardProp.CanWrite)
      {
        cardProp.SetValue(card, prop.GetValue(props));
      }
    }

    return card;
  }
}