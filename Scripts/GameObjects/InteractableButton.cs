using Godot;
using System;

public class InteractableButton : Node2D
{
	private Interactable Interactable;
	public event Action Pressed;
	public override void _Ready()
	{
		this.Interactable = this.GetNode<Interactable>("Interactable");

		this.Interactable.Interacted += () => {
            OnButtonPressed();
		};
    }
	public void OnButtonPressed()
	{
		this.Pressed?.Invoke();
	}
}
