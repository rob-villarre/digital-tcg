using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class HandView : Control
{

  [Export]
  public Curve SpreadCurve { get; set; }

  [Export]
  public int HoverOffset = 40;

  private CardPopupView _popupView;

  private Control _cards;
  
  private Dictionary<Control, Tween> _tweens = new();

  public override void _Ready()
  {

    _popupView = GetNode<CardPopupView>("CardPopup");
    _cards = GetNode<Control>("Cards");
    for (int i = 1; i <= 7; i++)
    {
      CardView card = CardFactory.Create($"base1-{i}");
      card.MouseEntered += async () => await OnCardHoverAsync(card);
      card.MouseExited += async () => await OnCardUnhoverAsync(card);
      _cards.AddChild(card);
    }
    // UpdateSeparation();
    UpdateHand();
  }

  private void UpdateHand()
  {
    foreach (Control card in _cards.GetChildren())
    {
      Vector2 position = card.Position;
      float cardWidth = card.Size.X;
      float handRatio = _cards.GetChildCount() > 1 ? HandRatio(card) : 0.5f;
      float handSize = _cards.GetChildCount()*cardWidth < _cards.Size.X ? _cards.GetChildCount()*cardWidth : _cards.Size.X;
      float spreadOffset = SpreadCurve.Sample(handRatio) * (handSize - cardWidth);
      if (_cards.GetChildCount()*cardWidth < _cards.Size.X) {
        spreadOffset += (_cards.Size.X - _cards.GetChildCount()*cardWidth) / 2;
      }      position.X = spreadOffset;
      card.SetPosition(position);
    }
  }

  private float HandRatio(Control card)
  {
    return card.GetIndex() / ((float)_cards.GetChildCount() - 1);
  }

  private async Task OnCardHoverAsync(Control card)
  {
    Vector2 initPostion = card.Position;
    Vector2 size = card.Size;
    size.Y += HoverOffset;
    card.SetSize(size);

    // Animate lift up
    var tween = GetTree().CreateTween();
    tween.TweenProperty(card, "position:y", card.Position.Y - HoverOffset, 0.2);
    _tweens[card] = tween;

    await ToSignal(tween, Tween.SignalName.Finished);
    await Task.Delay(300);

    if (_tweens.ContainsKey(card))
    {
      float handRatio = HandRatio(card);
      _popupView.CardView = (CardView)card;
      Vector2 popupSize = _popupView.CardView.Size * _popupView.CardView.Scale;
      float xPos = SpreadCurve.Sample(handRatio) < 0.5 ? card.Position.X : card.Position.X - popupSize.X + card.Size.X;
      float yPos = initPostion.Y - popupSize.Y - HoverOffset;
      _popupView.Position = new Vector2(xPos, yPos);
      _popupView.Visible = true;
    }
  }

  private async Task OnCardUnhoverAsync(Control card)
  {
    if (_tweens.ContainsKey(card))
    {
      _tweens[card].Kill();
      _tweens.Remove(card);
    }
    
    _popupView.Visible = false;
    Vector2 size = card.Size;
    size.Y -= HoverOffset;
    card.SetSize(size);

    // Animate lift down
    var tween = GetTree().CreateTween();
    tween.TweenProperty(card, "position:y", 0, 0.2);

    await ToSignal(tween, Tween.SignalName.Finished);
  
  }
}
