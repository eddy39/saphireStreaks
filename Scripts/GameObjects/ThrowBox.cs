using Godot;
using System;

public class ThrowBox : KinematicBody2D
{
    public Vector2 velocity = new Vector2();
    public float gravity = 15;
    public override void _Ready()
    {
        
    }
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        // velocity decay/friction
        if (IsOnFloor()) velocity *= 0.9f;
        // apply gravity
        velocity.y += gravity;
        // apply move
        velocity = MoveAndSlide(velocity, Vector2.Up);
        
    }
}
