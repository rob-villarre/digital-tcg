using System;
using Godot;

enum CardType {
  Pokemon,
  Trainer,
  Energy
}

public partial class Card : TextureButton
{

  [Export]
  private Texture2D TextureFront;

  [Export]
  private Texture2D TextureBack = ResourceLoader.Load<Texture2D>("res://assets/cards/other/back.png");

  [Export]
  private String CardName;

  [Export]
  private CardType Type;

  [Export]
  private Boolean IsFaceUp = true;

  [Export]

  private float YRot {
    get
    {
      ShaderMaterial shader = (ShaderMaterial)Material;
      return (float)shader.GetShaderParameter("y_rot");
    }
    set {
      ShaderMaterial shader = (ShaderMaterial)Material;
      shader.SetShaderParameter("y_rot", value);
    }
  }
  public override void _Ready()
  {
    TextureNormal = IsFaceUp ? TextureFront : TextureBack;
  }

  private void OnGuiInput(InputEvent @event)
  {
    if (@event is InputEventMouseMotion)
    {
      // TiltCard();
    }
  }

  private void OnPressed()
  {
    // FlipCard();
    AnimationPlayer player = GetNode<AnimationPlayer>("FlipCardAnimationPlayer");
    player.Play("card_flip_animation");
  }

  public void FlipCard()
  {
    IsFaceUp = !IsFaceUp;
    TextureNormal = IsFaceUp ? TextureFront : TextureBack;
  }

  private void TiltCard()
  {
    Vector2 localMouse = GetLocalMousePosition();
    Vector2 center = Size / 2f;
    Vector2 offset = (localMouse - center) / center;
    const float MaxRotation = 5f;

    float yRot = offset.X * MaxRotation;
    float xRot = -offset.Y * MaxRotation;

    ShaderMaterial shader = (ShaderMaterial)Material;
    shader.SetShaderParameter("y_rot", yRot);
    shader.SetShaderParameter("x_rot", xRot);
  }

  private void ResetCardTilt()
  {
    ShaderMaterial shader = (ShaderMaterial)Material;
    shader.SetShaderParameter("y_rot", 0.0);
    shader.SetShaderParameter("x_rot", 0.0);
  }

  private void OnMouseExited()
  {
    // ResetCardTilt();
  }
}
