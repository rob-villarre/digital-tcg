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
  private String CardName;

  [Export]
  private CardType Type;

  public override void _Ready()
  {

  }


  private void OnGuiInput(InputEvent @event)
  {
    if (@event is InputEventMouseMotion)
    {
      TiltCard();
    }
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
    ResetCardTilt();
  }
}
