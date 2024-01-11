using Godot;
using System;

public class LevelCamera : Camera2D
{
    public Player player;
    // movemetn vars
    private Vector2 velocity = new Vector2();
    private Vector2 target = new Vector2();
    [Export] private Vector2 offset = new Vector2(0, 0);
    [Export] private float speedx = 10;
    [Export] private float speedy = 10;
    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        // get target
        target = player.Position;
        // move towards target
        velocity.x = Mathf.Lerp(Position.x, target.x, speedx); // Never do interpolation with delta
        velocity.y = Mathf.Lerp(Position.y, target.y, speedy);
        // apply velocity
        Position = velocity + offset;
    }
}
