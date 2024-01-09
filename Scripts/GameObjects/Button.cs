using Godot;
using System;

public class Button : Node2D
{
	private Interactable Interactable;
	public override void _Ready()
	{
		this.Interactable = this.GetNode<Interactable>("Interactable");

		this.Interactable.Interacted += () => {
            GameState.OpenDoor();
		};
    }	
}
