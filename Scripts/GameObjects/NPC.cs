using Godot;
using System;

public class NPC : KinematicBody2D
{
    public BaseDialogue dialogue;
    public Interactable Interactable;
    public Vector2 velocity = new Vector2();
    public event Action OnDialogueInteractEvent; 
    public override void _Ready()
    {
        // get nodes
        Interactable = (Interactable) FindNode("Interactable");
        // connect events
        this.Interactable.Interacted += () =>
		{
			OnDialogueInteractEvent?.Invoke();
		};
    }
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        // apply gravity
        velocity.y += 15;
        // apply move
        velocity = MoveAndSlide(velocity, Vector2.Up);
    }
}
