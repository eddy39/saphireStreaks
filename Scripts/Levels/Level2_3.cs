using Godot;
using System;

public class Level2_3 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;
    public StepButton button1;
    public Laser laser;
    
    public int doorOpenCount = 0;
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        button1 = GetNode<StepButton>("StepButton2");
        laser = GetNode<Laser>("Laser");

        // Connect
        
        button.ButtonPressedEvent += ReverseLaserSwitch;
        button1.ButtonPressedEvent += door.OpenCloseDoor;
        
        
        
        // FOR TESTING PURPOSES SET THE REQUIRED GEMS TRUE
        if (!GameState.Gems[1]) GameState.AddGem(Gem.Color.Yellow);
        if (!GameState.Gems[0]) GameState.AddGem(Gem.Color.Blue);
        
    }
   
    public void ReverseLaserSwitch(bool activated)
    {
        if (activated)
        {
            laser.SwitchLaserOnOff(false);
        }
        else
        {
            laser.SwitchLaserOnOff(true);
        }
    }
}
