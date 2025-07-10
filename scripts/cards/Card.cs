using System;
using Godot;

public enum CardSupertype
{
  Pokemon,
  Trainer,
  Energy
}

// public class Set
// {
//   private 
// }


public partial class Card : TextureButton
{

  [Export]
  protected Texture2D textureFront = ResourceLoader.Load<Texture2D>("res://assets/cards/other/back.png");

  [Export]
  protected Texture2D textureBack = ResourceLoader.Load<Texture2D>("res://assets/cards/other/back.png");

  [Export]
  protected string title;

  [Export]
  protected CardSupertype type;

  [Export]
  protected Boolean isFaceUp = true;

  protected string setId;
  protected string number;
  protected string rarity;
  protected string flavorText;

  public float YRot
  {
    get
    {
      ShaderMaterial shader = (ShaderMaterial)Material;
      return (float)shader.GetShaderParameter("y_rot");
    }
    set
    {
      ShaderMaterial shader = (ShaderMaterial)Material;
      shader.SetShaderParameter("y_rot", value);
    }
  }

  public override void _Ready()
  {
    TextureNormal = isFaceUp ? textureFront : textureBack;
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
    isFaceUp = !isFaceUp;
    TextureNormal = isFaceUp ? textureFront : textureBack;
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
