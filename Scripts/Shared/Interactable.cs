using Godot;
using System;

public class Interactable : Area2D
{
    public event Action PlayerEntered;
    public event Action PlayerExited;
    public event Action Interacted;
    public bool PlayerInArea { get; set; }
    public override void _Ready()
    {
        Connect("body_entered", this, nameof(OnPlayerEntered));
        Connect("body_exited", this, nameof(OnPlayerExited));
    }
    
    private void OnPlayerEntered(Node2D body)
    {
        if (!(body is Player))
            return;

        PlayerInArea = true;
        PlayerEntered?.Invoke();
    }

    private void OnPlayerExited(Node2D body)
    {
        if (!(body is Player))
            return;

        PlayerInArea = false;
        PlayerExited?.Invoke();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact") && PlayerInArea)
        {
            this.Interacted?.Invoke();
        }
    }
}