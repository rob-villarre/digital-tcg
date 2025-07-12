using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class Hand : HBoxContainer
{
  const short INIT_HAND_SIZE = 7;
  private List<Card> _cards = [];

  [Export]
  public int CardWidth = 100;

  [Export]
  public int NormalSeparation = 10;

  [Export]
  public int OverlapSeparation = -3;

  [Export]
  public int HoverOffset = 20;

  public override void _Ready()
  {
    for (int i = 1; i <= 60; i++)
    {
      Card card = CardFactory.Create($"base1-{i}");
      AddChild(card);
      card.MouseEntered += () => OnCardHover(card);
      card.MouseExited += () => OnCardUnhover(card);
    }
    UpdateSeparation();
  }
  
  private void OnCardHover(Control card)
  {
    // Animate lift up
    var tween = GetTree().CreateTween();
    tween.TweenProperty(card, "position:y", card.Position.Y - HoverOffset, 0.2);
  }

  private void OnCardUnhover(Control card)
  {
    // Animate lift down
    var tween = GetTree().CreateTween();
    tween.TweenProperty(card, "position:y", 0, 0.2);
  }

  private void UpdateSeparation()
  {
    int cardCount = GetChildCount();
    if (cardCount == 0) return;

    // Calculate total width needed with normal separation
    int totalWidthNormal = CardWidth * cardCount + NormalSeparation * (cardCount - 1);

    // Get the available width of the container
    int availableWidth = (int)Size.X;

    // If cards overflow container, set negative separation to overlap
    if (totalWidthNormal > availableWidth)
    {
      GD.Print((availableWidth - (CardWidth * cardCount)) / (cardCount - 1));
      AddThemeConstantOverride("separation", (availableWidth - (CardWidth * cardCount)) / (cardCount - 1));
    }
    else
    {
      AddThemeConstantOverride("separation", NormalSeparation);
    }
  }
}
