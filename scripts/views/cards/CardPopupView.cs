using Godot;
using System;

public partial class CardPopupView : Control
{
  private CardView _displayCard;
  
  public CardView CardView 
  { 
    get => _displayCard;
    set 
    {
      if (_displayCard != null)
        _displayCard.QueueFree();
        
      _displayCard = CardFactory.Create(value.Card.Id);
      _displayCard.Scale = Vector2.One * 2.75f; // Make it larger
      AddChild(_displayCard);
    }
  }

  public void Show()
  {
    Visible = true;
  }

  public void Hide()
  {
    Visible = false;
  }

  public override void _Ready()
  {
    Visible = false;
  }
}

