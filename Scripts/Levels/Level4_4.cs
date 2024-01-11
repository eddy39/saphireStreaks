using Godot;
using System;

public class Level4_4 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;
    public StepButton button2;
    public StepButton button3;
    

   
    public int doorOpenCount = 0;
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        button2 = GetNode<StepButton>("StepButton2");
        button3 = GetNode<StepButton>("StepButton3");
       
        // Connect
        
        button.ButtonPressedEvent += MultiOpenDoor;
        button2.ButtonPressedEvent += MultiOpenDoor;
        button3.ButtonPressedEvent += MultiOpenDoor;
        
        
        // FOR TESTING PURPOSES SET THE REQUIRED GEMS TRUE
        if (!GameState.Gems[1]) GameState.AddGem(Gem.Color.Yellow);
        if (!GameState.Gems[0]) GameState.AddGem(Gem.Color.Blue);
        if (!GameState.Gems[2]) GameState.AddGem(Gem.Color.Red);
        if (!GameState.Gems[3]) GameState.AddGem(Gem.Color.Purple);    
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
        if (doorOpenCount >= 3)
        {
            door.OpenCloseDoor(true);
        }
        else
        {
            door.OpenCloseDoor(false);
        }
    }
}
