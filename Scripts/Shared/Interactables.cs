using Godot;
using System;

public class Interactables : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public event Action PlayerEntered;
    public event Action PlayerExited;
    public event Action Interacted;
    public override void _Ready()
    {
        Connect("body_entered", this, nameof(OnPlayerEntered));
        Connect("body_exited", this, nameof(OnPlayerExited));
    }

    private void OnPlayerEntered(Node2D body)
    {
        if (!(body is Player))
            return;

        PlayerEntered?.Invoke();
    }

    private void OnPlayerExited(Node2D body)
    {
        if (!(body is Player))
            return;

        PlayerExited?.Invoke();
    }

    public void Interact() => this.Interacted?.Invoke();

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}