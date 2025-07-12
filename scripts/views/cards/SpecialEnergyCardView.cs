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
    SpecialEnergyCardView cardView = scene.Instantiate<SpecialEnergyCardView>();

    cardView.textureFront = ResourceLoader.Load<Texture2D>($"res://assets/cards/{props.CardSet.Id}/images/{props.Id}.png"); 
    cardView.Card = new SpecialEnergyCard(props);

    return cardView;
  }
}