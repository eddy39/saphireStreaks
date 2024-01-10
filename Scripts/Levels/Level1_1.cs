using Godot;
using System;

public class Level1_1 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        // Connect
        button.ButtonPressedEvent += door.OpenCloseDoor;

        var laser = GetNode<Laser>("Laser");
        laser.ShutOnLaser(true);
        
    }

}
