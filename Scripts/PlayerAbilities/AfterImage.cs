using Godot;
using System;

public class AfterImage : Player
{
    
    public override void _Ready()
    {
        
    }
    public override void _UnhandledKeyInput(InputEventKey @event)
    {
        // No controls
        //base._UnhandledKeyInput(@event);
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        // No controls
        //base._UnhandledKeyInput(@event);
    }
    public override void _Input(InputEvent @event)
    {
        // No controls
        //base._UnhandledKeyInput(@event);
    }
    public override void _PhysicsProcess(float delta)
    {
        // No base physics
        //base._PhysicsProcess(delta);
        // just falling
        velocity.y += gravity;
        // apply velocity
        MoveAndSlide(velocity, Vector2.Up);
    }
}
