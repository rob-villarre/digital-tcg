
public enum BasicEnergy
{
  Colorless,
  Grass,
  Fire,
  Water,
  Lightning,
  Psychic,
  Fighting,
}

public class BasicEnergyCardProps : CardProps
{
  public BasicEnergy Type { get; init; }
}

public class BasicEnergyCard : Card
{
  public BasicEnergy Type { get; init; }

  public BasicEnergyCard(BasicEnergyCardProps props) : base(props)
  {
    Type = props.Type;
  }
}