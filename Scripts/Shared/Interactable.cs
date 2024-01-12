using Godot;
using System;

public class Interactable : Area2D
{
    public event Action PlayerEntered;
    public event Action PlayerExited;
    public event Action Interacted;
    public bool PlayerInArea { get; set; }

    public Sprite KeyBG;
    public override void _Ready()
    {
        Connect("body_entered", this, nameof(OnPlayerEntered));
        Connect("body_exited", this, nameof(OnPlayerExited));

        KeyBG = GetNode<Sprite>("KeyBG");
        KeyBG.Visible = false;
        KeyBG.GlobalRotation = 0;
    }
    
    private void OnPlayerEntered(Node2D body)
    {
        if (!(body is Player))
            return;

        PlayerInArea = true;
        PlayerEntered?.Invoke();

        KeyBG.Visible = true;
    }

    private void OnPlayerExited(Node2D body)
    {
        if (!(body is Player))
            return;

        PlayerInArea = false;
        PlayerExited?.Invoke();
    
        KeyBG.Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact") && PlayerInArea)
        {
            this.Interacted?.Invoke();
        }
    }
}