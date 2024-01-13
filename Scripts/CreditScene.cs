using Godot;
using System;

public class CreditScene : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<AudioStreamPlayer>("/root/BgMusic").Stream = GD.Load<AudioStream>("res://Assets/Music/Gemstasis_03_Credits_Transmission.mp3");
		GetNode<AudioStreamPlayer>("/root/BgMusic").Play();	
		GetNode<AudioStreamPlayer>("/root/BgMusic").VolumeDb = -25;

        GetNode("StartGameBtn").Connect("pressed", this,nameof(toMainMenu) );
    }
    public void toMainMenu()
    {
        // change scene to main menu
        GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
    
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
