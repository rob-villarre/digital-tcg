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
    BasicEnergyCardView cardView = scene.Instantiate<BasicEnergyCardView>();

    cardView.textureFront = ResourceLoader.Load<Texture2D>($"res://assets/cards/{props.CardSet.Id}/images/{props.Id}.png");
    cardView.Card = new BasicEnergyCard(props);

    return cardView;
  }
}