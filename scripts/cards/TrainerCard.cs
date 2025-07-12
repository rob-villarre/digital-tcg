using System.Collections.Generic;
using System.Reflection;
using Godot;

#nullable enable

public class TrainerCardProps : CardProps
{
  public List<string> Rules { get; init; } = [];
  public short? HitPoints { get; init; }

}

public partial class TrainerCard : Card
{

  public List<string> Rules { get; init; } = [];

  public static TrainerCard Instantiate(TrainerCardProps props)
  {
    PackedScene scene = GD.Load<PackedScene>("res://scenes/cards/trainer_card.tscn");
    TrainerCard card = scene.Instantiate<TrainerCard>();

    card.textureFront = ResourceLoader.Load<Texture2D>($"res://assets/cards/{props.CardSet.Id}/images/{props.Id}.png");

    foreach (PropertyInfo prop in typeof(TrainerCardProps).GetProperties())
    {
      PropertyInfo? cardProp = typeof(TrainerCard).GetProperty(prop.Name);
      if (cardProp != null && cardProp.CanWrite)
      {
        cardProp.SetValue(card, prop.GetValue(props));
      }
    }

    return card;
  }
}