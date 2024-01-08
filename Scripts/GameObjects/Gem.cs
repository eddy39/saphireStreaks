using Godot;
using System;

public class Gem : Node2D
{
	[Export] public Color ColorType;
	private Interactable Interactable;
	public override void _Ready()
	{
		this.Interactable = this.GetNode<Interactable>("Interactable");

		// Give the player gem when the player interacts with it
		this.Interactable.Interacted += () => {
			GameState.AddGem(ColorType);
			this.Visible = false;
		};
		// Load Texture based on color
		switch (ColorType)
		{
			case Color.Blue:
				this.GetNode<Sprite>("Sprite").Texture = GD.Load<Texture>("res://Assets/LevelSprites/GemBlue.png");
				break;
			case Color.Yellow:
				this.GetNode<Sprite>("Sprite").Texture = GD.Load<Texture>("res://Assets/LevelSprites/GemYellow.png");
				break;
			case Color.Red:
				this.GetNode<Sprite>("Sprite").Texture = GD.Load<Texture>("res://Assets/LevelSprites/GemRed.png");
				break;
			case Color.Purple:
				this.GetNode<Sprite>("Sprite").Texture = GD.Load<Texture>("res://Assets/LevelSprites/GemPurple.png");
				break;
		}
	}

	public enum Color 
	{
		Blue, Yellow, Red, Purple
	}
}
