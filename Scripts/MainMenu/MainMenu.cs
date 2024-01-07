using Godot;
using System;

public class MainMenu : Control
{
    
    public override void _Ready()
    {
        // Connect StartGameBtn to StartGame
        GetNode("StartGameBtn").Connect("pressed", this, "StartGame");
    }
    public void StartGame()
    {
        // Load the game scene
        GetTree().ChangeScene("res://Scenes/BaseLevel.tscn");
    }

}
