using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;

#nullable enable


public partial class BasicEnergyCardView : CardView
{

  public static BasicEnergyCardView Instantiate(BasicEnergyCardProps props)
  {
    PackedScene scene = GD.Load<PackedScene>("res://scenes/cards/basic_energy_card.tscn");
    BasicEnergyCardView card = scene.Instantiate<BasicEnergyCardView>();

    card.textureFront = ResourceLoader.Load<Texture2D>($"res://assets/cards/{props.CardSet.Id}/images/{props.Id}.png");

    // foreach (PropertyInfo prop in typeof(BasicEnergyCardProps).GetProperties())
    // {
    //   PropertyInfo? cardProp = typeof(BasicEnergyCardView).GetProperty(prop.Name);
    //   if (cardProp != null && cardProp.CanWrite)
    //   {
    //     cardProp.SetValue(card, prop.GetValue(props));
    //   }
    // }

    return card;
  }
}