using Godot;
using System;

public class ToNextLvl : Area2D
{
    [Export] public string NextLevel;
    [Export] public bool CanChangeScene = true;

    public AudioStreamPlayer2D audioPlayer;
    public override void _Ready()
    {
        Connect("body_entered", this, nameof(OnBodyEntered));
    
        audioPlayer = GetNode<AudioStreamPlayer2D>("NextlvlAudio");
        // load next lvl audio
        audioPlayer.Stream = (AudioStream) ResourceLoader.Load("res://Assets/Sound/Level_End_Sound_Effect.wav");
    }
    public void OnBodyEntered(Node body)
    {
        if (body is Player)
        {
            // start playing audio
            audioPlayer.Play();
            // connect finished signal
            audioPlayer.Connect("finished", this, nameof(OnAudioFinished));
            
        }
    }
    public void OnAudioFinished()
    {
        GameState.InvokeLevelsChanged();
            
            
        if (CanChangeScene) //Used for debugging
            GetTree().ChangeScene(NextLevel);
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
