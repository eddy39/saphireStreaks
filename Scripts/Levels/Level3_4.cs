using Godot;
using System;

public class Level3_4 : BaseLevel
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
        if (!GameState.Gems[2]) GameState.AddGem(Gem.Color.Red);
                
    }
    public void MultiOpenDoor(bool activated)
    {
        if (activated)
        {
            doorOpenCount+=1;
        }
        else
        {
            doorOpenCount-=1;
        }
        if (doorOpenCount >= 2)
        {
            door.OpenCloseDoor(true);
        }
        else
        {
            door.OpenCloseDoor(false);
        }
    }
    
}
