using Godot;
using System;

public class Level2_2 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;
    public StepButton button1;
    public StepButton button2;
    public StepButton button3;
    
    public int doorOpenCount = 0;
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        button1 = GetNode<StepButton>("StepButton2");
        button2 = GetNode<StepButton>("StepButton3");
        button3 = GetNode<StepButton>("StepButton4");
        // Connect
        
        button.ButtonPressedEvent += MultiOpenDoor;
        button1.ButtonPressedEvent += MultiOpenDoor;
        button2.ButtonPressedEvent += MultiOpenDoor;
        button3.ButtonPressedEvent += MultiOpenDoor;
        
        
        // FOR TESTING PURPOSES SET THE REQUIRED GEMS TRUE
        if (!GameState.Gems[0]) GameState.AddGem(Gem.Color.Yellow);
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
        if (doorOpenCount >= 4)
        {
            door.OpenCloseDoor(true);
        }
        else
        {
            door.OpenCloseDoor(false);
        }
    }
}
