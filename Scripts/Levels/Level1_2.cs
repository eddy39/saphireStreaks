using Godot;
using System;

public class Level1_2 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;
    public Laser laser;
    public Laser laser2;

    public NPC npc;
    
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        laser = GetNode<Laser>("Laser");
        laser2 = GetNode<Laser>("Laser2");
        npc = GetNode<NPC>("NPC");
        // Setup npc
        npc.dialogue = new Dialogue0();
        ui.dialogueBox.dialogue = npc.dialogue;
        npc.OnDialogueInteractEvent += ui.dialogueBox.StartDialogue;

        // Connect
        button.ButtonPressedEvent += door.OpenCloseDoor;
        button.ButtonPressedEvent += laser.SwitchLaserOnOff;
        button.ButtonPressedEvent += laser2.SwitchLaserOnOff;
        
        //
        
        // FOR TESTING PURPOSES SET THE REQUIRED GEMS TRUE
        if (!GameState.Gems[0]) GameState.AddGem(Gem.Color.Blue);
    }

}
