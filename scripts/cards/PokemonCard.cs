using System;
using System.Collections.Generic;
using Godot;

#nullable enable

public enum DamageModifier
{
  None,
  Plus,
  Multiply
}

public class Attack
{
  private string _name;
  private string? _text = null;
  private List<EnergyType> _cost;
  private Int16 _convertedEnergyCost;

  // TODO: look into turning 30x, 40+ damage to ints
  private Int16 _baseDamage;

  private DamageModifier _damageModifier;


  public Attack(string name, string? text, List<EnergyType> cost, Int16 convertedEnergyCost, Int16 baseDamage, DamageModifier damageModifier)
  {
    _name = name;
    _text = text;
    _cost = cost;
    _convertedEnergyCost = convertedEnergyCost;
    _baseDamage = baseDamage;
    _damageModifier = damageModifier;
  }
}

public partial class PokemonCard : Card
{
  [Export]
  private Int16 _hitPoints;

  private List<String> _subtypes;

  private List<String> _types;

  private string? _evolvesFrom = null;

  private string? _evolvesTo = null;

  private List<Attack> _attacks;

  private List<EnergyType> _attachedEnergy;

  public PokemonCard()
  {
  }

}
