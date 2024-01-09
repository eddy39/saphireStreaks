using Godot;
using System;

public class Level1_3 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;
    public StepButton button2;
    
    public Laser laser;
    public Laser laser2;
    
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        button2 = GetNode<StepButton>("StepButton2");
        laser = GetNode<Laser>("Laser");
        //laser2 = GetNode<Laser>("Laser2");
        // Connect
        
        button.ButtonPressedEvent += laser.ShutOffLaser;
        button2.ButtonPressedEvent += door.OpenCloseDoor;
        
        
        // FOR TESTING PURPOSES SET THE REQUIRED GEMS TRUE
        if (!GameState.Gems[0]) GameState.AddGem(Gem.Color.Blue);
    }

}
