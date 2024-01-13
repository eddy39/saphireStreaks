using Godot;
using System;

public class Level1_1 : BaseLevel
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
        npc.dialogue = new Dialogue00();
        ui.dialogueBox.dialogue = npc.dialogue;
        npc.OnDialogueInteractEvent += ui.dialogueBox.StartDialogue;
        // Connect
        button.ButtonPressedEvent += door.OpenCloseDoor;

        
    }

}
