using Godot;
using System;

public class Level1_4 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;
    public Lever lever;
    public Lever lever2;
    
    public Laser laser;
    public Laser laser2;
    
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        laser = GetNode<Laser>("Laser");
        lever = GetNode<Lever>("Lever");
        lever2 = GetNode<Lever>("Lever2");
        laser2 = GetNode<Laser>("Laser2");
        // Connect
        
        button.ButtonPressedEvent += door.OpenCloseDoor;
        lever.LeverStateChangedEvent += SwitchLaser1;
        lever2.LeverStateChangedEvent += SwitchLaser2;
        
        
        // FOR TESTING PURPOSES SET THE REQUIRED GEMS TRUE
        if (!GameState.Gems[0]) GameState.AddGem(Gem.Color.Blue);
    }
    public void SwitchLaser1(int leverState)
    {
        if (leverState == 1)
        {
            laser.SwitchLaserOnOff(true);
        }
        else
        {
            laser.SwitchLaserOnOff(false);
        }
    }
    public void SwitchLaser2(int leverState)
    {
        if (leverState == 1)
        {
            laser2.SwitchLaserOnOff(true);
        }
        else
        {
            laser2.SwitchLaserOnOff(false);
        }
    }

}
