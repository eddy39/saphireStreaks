using Godot;
using System;

public class Level3_3 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;
    public StepButton button1;

    
    public int doorOpenCount = 0;
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        button1 = GetNode<StepButton>("StepButton2");
        
        // Connect
        
        button.ButtonPressedEvent += MultiOpenDoor;
        button1.ButtonPressedEvent += MultiOpenDoor;
        
        
        
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
