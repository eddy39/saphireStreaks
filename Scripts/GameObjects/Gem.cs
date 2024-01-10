using Godot;
using System;
using System.Data.Odbc;

public class Gem : Node2D
{
	[Export] public Color ColorType { get; set; }
	[Export] public bool IsInteractable { get; set; } = true;
	[Export] public bool Regeneratable { get; set; }
	private Timer RegenerationTimer;
	private Interactable Interactable;
	private Sprite GemSprite;
    private ShaderMaterial ShaderMaterial = GD.Load<ShaderMaterial>("res://Scripts/Shared/OutlineShader.tres");
	public override void _Ready()
	{
		this.GemSprite = base.GetNode<Sprite>("Sprite");
		this.Interactable = this.GetNode<Interactable>("Interactable");

		// Give the player gem when the player interacts with it
		this.Interactable.Interacted += () =>
		{
			if (!this.Visible)
				return;

			GameState.AddGem(ColorType);

			if (Regeneratable)
			{
				this.Visible = false;

				if (this.ColorType == Color.Red)
					GameState.RedGemQueue.Enqueue(this);
			}
			else
			{
				this.QueueFree();
			}
		};

		if (Regeneratable)
		{
			this.RegenerationTimer = base.GetNode<Timer>("RegenerationTimer");
			this.RegenerationTimer.Connect("timeout", this, nameof(RegenerateGem));
		}

		// Load Texture based on color
		switch (ColorType)
		{
			case Color.Blue:
				this.GetNode<Sprite>("Sprite").Texture = GD.Load<Texture>("res://Assets/LevelSprites/Blue_Gem.png");
				break;
			case Color.Yellow:
				this.GetNode<Sprite>("Sprite").Texture = GD.Load<Texture>("res://Assets/LevelSprites/Yellow_Gem.png");
				break;
			case Color.Red:
				this.GetNode<Sprite>("Sprite").Texture = GD.Load<Texture>("res://Assets/LevelSprites/Red_Gem.png");
				break;
			case Color.Purple:
				this.GetNode<Sprite>("Sprite").Texture = GD.Load<Texture>("res://Assets/LevelSprites/Purple_Gem.png");
				break;
		}

		this.Interactable.PlayerEntered += OnPlayerEntered;
		this.Interactable.PlayerExited += OnPlayerExited;
	}

	private void RegenerateGem()
	{
		if (GameState.RedGemQueue.Contains(this))
		{
			return;
		}

		this.Visible = true;
	}

	private void OnPlayerEntered()
	{
		if (IsInteractable)
		{
			this.GemSprite.Material = ShaderMaterial;
		}
	}

	private void OnPlayerExited()
	{
		this.GemSprite.Material = null;
	}

	public enum Color
	{
		Blue, Yellow, Red, Purple
	}
}

