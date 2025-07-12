using System.Collections.Generic;

public enum SpecialEnergy
{
  DoubleColorless,
}

public class SpecialEnergyCardProps : CardProps
{
  public SpecialEnergy Type { get; init; }
  public List<string> Rules { get; init; } = [];
}

public class SpecialEnergyCard : Card
{
  public SpecialEnergy Type { get; init; }
  public List<string> Rules { get; init; } = [];

  public SpecialEnergyCard(SpecialEnergyCardProps props) : base(props)
  {
    Type = props.Type;
    Rules = props.Rules;
  }
}