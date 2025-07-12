using System.Reflection;
using Godot;

#nullable enable

public partial class TrainerCardView : CardView
{

  public static TrainerCardView Instantiate(TrainerCardProps props)
  {
    PackedScene scene = GD.Load<PackedScene>("res://scenes/cards/trainer_card.tscn");
    TrainerCardView cardView = scene.Instantiate<TrainerCardView>();

    cardView.textureFront = ResourceLoader.Load<Texture2D>($"res://assets/cards/{props.CardSet.Id}/images/{props.Id}.png");
    cardView.Card = new TrainerCard(props);

    return cardView;
  }
}