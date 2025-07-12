using System.Collections.Generic;
using Godot;

#nullable enable

public enum CardSupertype
{
  Pokemon,
  Trainer,
  Energy,
}

public enum CardSubtype
{
  BREAK,
  Baby,
  Basic,
  EX,
  GX,
  GoldenrodGameCorner,
  Item,
  LEGEND,
  LevelUp,
  MEGA,
  PokemonTool,
  PokemonToolF,
  RapidStrike,
  Restored,
  RocketsSecretMachine,
  SingleStrike,
  Special,
  Stadium,
  Stage1,
  Stage2,
  Supporter,
  TAGTEAM,
  TechnicalMachine,
  V,
  VMAX,
}

public class CardProps
{
  public string Id { get; init; } = string.Empty;
  public string Title { get; init; } = string.Empty;
  public short Number { get; init; }
  public string Supertype { get; init; } = string.Empty;
  public List<string>? Subtypes { get; init; }
  public CardSet CardSet { get; init; } = new CardSet();
  public string? Rarity { get; init; }
}


public partial class Card : TextureButton
{
  public string Id { get; init; } = string.Empty;
  public string Title { get; init; } = string.Empty;
  public short Number { get; init; }
  public string Supertype { get; init; } = string.Empty;
  public List<string>? Subtypes { get; init; }
  public CardSet CardSet { get; init; } = new CardSet();
  public string? Rarity { get; init; }
  
  [Export]
  protected Texture2D textureFront = ResourceLoader.Load<Texture2D>("res://assets/cards/other/back.png");

  [Export]
  protected Texture2D textureBack = ResourceLoader.Load<Texture2D>("res://assets/cards/other/back.png");

  [Export]
  protected bool isFaceUp = true;

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
