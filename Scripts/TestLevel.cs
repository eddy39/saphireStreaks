using Godot;
using System;

public class TestLevel : BaseLevel
{
    // nodes
    
    public override void _Ready()
    {
        base._Ready();
        GetNode<Laser>("Laser").CastLaser();
        GetNode<Laser>("Laser2").CastLaser();
    }

}
