using Godot;
using System;

public class MainMenu : Control
{
	
	public override void _Ready()
	{
		GetNode<AudioStreamPlayer>("/root/BgMusic").Stream = GD.Load<AudioStream>("res://Assets/Music/Gemstasis_01_A_New_Past.mp3");
		GetNode<AudioStreamPlayer>("/root/BgMusic").Play();	
		GetNode<AudioStreamPlayer>("/root/BgMusic").VolumeDb = -25;
		// Connect StartGameBtn to StartGame
		GetNode("StartGameBtn").Connect("pressed", this, "StartGame");
	}
	public void StartGame()
	{	
		GetNode<AudioStreamPlayer>("/root/BgMusic").Stream = GD.Load<AudioStream>("res://Assets/Music/Gemstasis_02_Tunnel_Shuffle.mp3");
		GetNode<AudioStreamPlayer>("/root/BgMusic").Play();
		GetNode<AudioStreamPlayer>("/root/BgMusic").VolumeDb = -40;
		// Load the game scene
		GetTree().ChangeScene("res://Scenes/Levels/1-1.tscn");
	}

}
