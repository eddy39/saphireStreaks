using Godot;
using System;

public class Level5_1 : BaseLevel
{
    // nodes
    public Door door;
    public StepButton button;
    public Lever lever;
    public Laser laser;
    public NPC npc;

   
    public int doorOpenCount = 0;
    public override void _Ready()
    {
        base._Ready();
        // Nodes
        door = GetNode<Door>("Door");
        button = GetNode<StepButton>("StepButton");
        lever = GetNode<Lever>("Lever");
        laser = GetNode<Laser>("Laser");
        npc = GetNode<NPC>("NPC");
        // Setup npc
        npc.dialogue = new Dialogue4();
        ui.dialogueBox.dialogue = npc.dialogue;
        npc.OnDialogueInteractEvent += ui.dialogueBox.StartDialogue;
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
    }
}
