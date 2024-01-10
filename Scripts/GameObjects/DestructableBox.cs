using Godot;
using System;

public class DestructableBox : StaticBody2D
{
    public AnimatedSprite animatedSprite;
    public override void _Ready()
    {
        // get nodes
        animatedSprite = (AnimatedSprite) FindNode("AnimatedSprite");
    }
    public void Boom()
    {
        animatedSprite.Play("default");
        
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        if (animatedSprite.Frame == 11)
        {
            QueueFree();
        }
    }
}
