using Godot;
using System;

public partial class Game : CanvasLayer
{

  public override void _Ready()
  {
    Card card = CardFactory.Create("base1-1");
    AddChild(card);
  }

}
