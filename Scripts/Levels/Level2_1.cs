using Godot;
using System;

public class Level2_1 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;
    public NPC npc;
    
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        npc = GetNode<NPC>("NPC");
        // Setup npc
        npc.dialogue = new TestDialogue();
        ui.dialogueBox.dialogue = npc.dialogue;
        // Connect
        
        button.ButtonPressedEvent += door.OpenCloseDoor;
        
        
        // FOR TESTING PURPOSES SET THE REQUIRED GEMS TRUE
        if (!GameState.Gems[0]) GameState.AddGem(Gem.Color.Blue);
    }
   

}
