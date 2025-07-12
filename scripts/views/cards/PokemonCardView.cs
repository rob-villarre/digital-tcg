using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;
using Godot.Collections;

#nullable enable

public partial class PokemonCardView : CardView
{

  public static PokemonCardView Instantiate(PokemonCardProps props)
  {
    PackedScene scene = GD.Load<PackedScene>("res://scenes/cards/pokemon_card.tscn");
    PokemonCardView card = scene.Instantiate<PokemonCardView>();

    card.textureFront = ResourceLoader.Load<Texture2D>($"res://assets/cards/{props.CardSet.Id}/images/{props.Id}.png");

    // foreach (PropertyInfo prop in typeof(PokemonCardProps).GetProperties())
    // {
    //   PropertyInfo? cardProp = typeof(PokemonCardView).GetProperty(prop.Name);
    //   if (cardProp != null && cardProp.CanWrite)
    //   {
    //     cardProp.SetValue(card, prop.GetValue(props));
    //   }
    // }

    return card;
  }

}
