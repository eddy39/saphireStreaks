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
	}

	public enum Color 
	{
		Blue, Yellow, Red, Purple
	}
}
