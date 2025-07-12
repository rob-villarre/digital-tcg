using Godot;


public partial class PokemonCardView : CardView
{
  public static PokemonCardView Instantiate(PokemonCardProps props)
  {
    PackedScene scene = GD.Load<PackedScene>("res://scenes/cards/pokemon_card.tscn");
    PokemonCardView cardView = scene.Instantiate<PokemonCardView>();

    cardView.textureFront = ResourceLoader.Load<Texture2D>($"res://assets/cards/{props.CardSet.Id}/images/{props.Id}.png");
    cardView.Card = new PokemonCard(props);

    return cardView;
  }

}
