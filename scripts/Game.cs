using Godot;
using System;

public partial class Game : CanvasLayer
{

  public override void _Ready()
  {
    CardView card = CardFactory.Create($"base1-55");
    AddChild(card);
  }

}
