using Godot;
using System;

public class Level1_2 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;
    public Laser laser;
    public Laser laser2;
    
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        laser = GetNode<Laser>("Laser");
        laser2 = GetNode<Laser>("Laser2");
        // Connect
        button.ButtonPressedEvent += door.OpenCloseDoor;
        button.ButtonPressedEvent += laser.SwitchLaserOnOff;
        button.ButtonPressedEvent += laser2.SwitchLaserOnOff;
        
        //
    }

}
