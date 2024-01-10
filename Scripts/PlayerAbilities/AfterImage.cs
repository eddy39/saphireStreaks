using Godot;
using System;

public class AfterImage : Player
{
    private Area2D Detector;
    public override void _Ready()
    {
        this.Detector = base.GetNode<Area2D>("Detector");
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

    // Detonate ability
    // Haven't added anything yet
    public void Detonate()
    {
        foreach (var colObject in this.Detector.GetOverlappingBodies())
        {
            if (colObject is Laser laser)
            {
                if (!laser.CanBeDisabled)
                    return;

                laser.OverloadLaser();
            }
        }

        QueueFree();
    }
}
