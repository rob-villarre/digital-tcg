using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;

#nullable enable

public partial class SpecialEnergyCardView : CardView
{
  
  public static SpecialEnergyCardView Instantiate(SpecialEnergyCardProps props)
  {
    PackedScene scene = GD.Load<PackedScene>("res://scenes/cards/special_energy_card.tscn");
    SpecialEnergyCardView card = scene.Instantiate<SpecialEnergyCardView>();

    card.textureFront = ResourceLoader.Load<Texture2D>($"res://assets/cards/{props.CardSet.Id}/images/{props.Id}.png");

    // foreach (PropertyInfo prop in typeof(BasicEnergyCardProps).GetProperties())
    // {
    //   PropertyInfo? cardProp = typeof(SpecialEnergyCardView).GetProperty(prop.Name);
    //   if (cardProp != null && cardProp.CanWrite)
    //   {
    //     cardProp.SetValue(card, prop.GetValue(props));
    //   }
    // }

    return card;
  }
}