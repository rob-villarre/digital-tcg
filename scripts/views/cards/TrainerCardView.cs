using System.Reflection;
using Godot;

#nullable enable

public partial class TrainerCardView : CardView
{

  public static TrainerCardView Instantiate(TrainerCardProps props)
  {
    PackedScene scene = GD.Load<PackedScene>("res://scenes/cards/trainer_card.tscn");
    TrainerCardView card = scene.Instantiate<TrainerCardView>();

    card.textureFront = ResourceLoader.Load<Texture2D>($"res://assets/cards/{props.CardSet.Id}/images/{props.Id}.png");

    // foreach (PropertyInfo prop in typeof(TrainerCardProps).GetProperties())
    // {
    //   PropertyInfo? cardProp = typeof(TrainerCard).GetProperty(prop.Name);
    //   if (cardProp != null && cardProp.CanWrite)
    //   {
    //     cardProp.SetValue(card, prop.GetValue(props));
    //   }
    // }

    return card;
  }
}