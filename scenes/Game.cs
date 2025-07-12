using Godot;
using System;

public partial class Game : CanvasLayer
{

  public override void _Ready()
  {
    for (int i = 1; i <= 102; i++)
    {
      Card card = CardFactory.Create($"base1-{i}");
      AddChild(card);
    }
    // Card card = CardFactory.Create("base1-1");
    // Card card = CardFactory.Create("base1-98");
    // Card card = CardFactory.Create("base1-96");
    
  }

}
