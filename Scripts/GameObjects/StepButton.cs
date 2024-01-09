using Godot;
using System;
using System.Collections.Generic;

public class StepButton : Node2D
{
    private StaticBody2D BottumButton;
    private Area2D TopButton;
    // vars
    public bool activated = false;
    public List<Node> objectsOnButton = new List<Node>();
    public event Action<bool> ButtonPressedEvent;
    public override void _Ready()
    {
        // get nodes
        BottumButton = (StaticBody2D) FindNode("BottumButton");
        TopButton = (Area2D) FindNode("TopButton");
        //
        TopButton.Connect("body_entered", this, nameof(OnTopButtonBodyEntered));
        TopButton.Connect("body_exited", this, nameof(OnTopButtonBodyExited));
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        
    }
    public void OnTopButtonBodyEntered(Node body)
    {
        if (body is Player)
        {
            objectsOnButton.Add(body);
            // if not already activated move down
            if (activated == false)
            {
                TopButton.Position += new Vector2(0, 5);
            }
            activated = true;
            ButtonPressedEvent?.Invoke(activated);
            
        }
    }
    public void OnTopButtonBodyExited(Node body)
    {
        if (body is Player)
        {
            objectsOnButton.Remove(body);
            if (objectsOnButton.Count == 0)
            {
                activated = false;
                ButtonPressedEvent?.Invoke(activated);
                TopButton.Position -= new Vector2(0, 5);
            }
        }
    }

}
