public static class CardFactory
{
  public static Card Create(CardType type)
  {
    return type switch
    {
      CardType.None => new Card(),
      CardType.Pokemon => new PokemonCard(),
      _ => null,
    };
  }
}