using Godot;
using System;

public class Level3_1 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;
    

    
    public int doorOpenCount = 0;
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        
        // Connect
        
        button.ButtonPressedEvent += door.OpenCloseDoor;
        
        
        
        // FOR TESTING PURPOSES SET THE REQUIRED GEMS TRUE
        if (!GameState.Gems[1]) GameState.AddGem(Gem.Color.Yellow);
        if (!GameState.Gems[0]) GameState.AddGem(Gem.Color.Blue);
        
    }
   
    
}
