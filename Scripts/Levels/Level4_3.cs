using Godot;
using System;

public class Level4_3 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;

    public Lever lever;
    public Laser laser;
    public Laser laser2;
    public Laser laser3;
    public Laser laser4;
    public int doorOpenCount = 0;
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        lever = GetNode<Lever>("Lever");
        laser = GetNode<Laser>("Laser");
        laser2 = GetNode<Laser>("Laser2");
        laser3 = GetNode<Laser>("Laser3");
        laser4 = GetNode<Laser>("Laser4");
        // Connect
        
        button.ButtonPressedEvent += door.OpenCloseDoor;
        lever.LeverStateChangedEvent += SwitchLaser1;
        
        
        // FOR TESTING PURPOSES SET THE REQUIRED GEMS TRUE
        if (!GameState.Gems[1]) GameState.AddGem(Gem.Color.Yellow);
        if (!GameState.Gems[0]) GameState.AddGem(Gem.Color.Blue);
        if (!GameState.Gems[2]) GameState.AddGem(Gem.Color.Red);
        if (!GameState.Gems[3]) GameState.AddGem(Gem.Color.Purple);    
    }
    
    public void SwitchLaser1(int leverState)
    {
        
        
        laser.TemporarlyShutOffLaser(10f);
        laser2.TemporarlyShutOffLaser(10f);
        laser3.TemporarlyShutOffLaser(10f);
        laser4.TemporarlyShutOffLaser(10f);
        
    }
}
