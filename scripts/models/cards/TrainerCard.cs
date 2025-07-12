using System.Collections.Generic;

public class TrainerCardProps : CardProps
{
  public List<string> Rules { get; init; } = [];
  public short? HitPoints { get; init; }

}

public class TrainerCard : Card
{
  public List<string> Rules { get; init; } = [];
  public short? HitPoints { get; init; }

  public TrainerCard(TrainerCardProps props) : base(props)
  {
    Rules = props.Rules;
    HitPoints = props.HitPoints;
  }
}