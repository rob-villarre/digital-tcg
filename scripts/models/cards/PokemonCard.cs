using System.Collections.Generic;

# nullable enable

public class PokemonCardProps : CardProps
{
  public short HitPoints { get; init; }
  public List<PokemonType> Types { get; init; } = [];
  public string? EvolvesFrom { get; init; }
  public string? EvolvesTo { get; init; }
  public List<Ability> Abilities { get; init; } = [];
  public List<Attack> Attacks { get; init; } = [];
  public List<Weakness>? Weaknesses { get; init; }
}

public class PokemonCard : Card
{
  public short HitPoints { get; init; }
  public List<PokemonType> Types { get; init; } = [];
  public string? EvolvesFrom { get; init; }
  public string? EvolvesTo { get; init; }
  public List<Ability> Abilities { get; init; } = [];
  public List<Attack> Attacks { get; init; } = [];
  public List<Weakness>? Weaknesses { get; init; }

  public PokemonCard(PokemonCardProps props) : base(props)
  {
    HitPoints = props.HitPoints;
    Types = props.Types;
    EvolvesFrom = props.EvolvesFrom;
    EvolvesTo = props.EvolvesTo;
    Abilities = props.Abilities;
    Attacks = props.Attacks;
    Weaknesses = props.Weaknesses;
  }

  
}