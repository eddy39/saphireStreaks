using Godot;
using System;

public class Lever : Node2D
{
    [Export] public bool LeverState { get => _leverState; set { _leverState = value; OnLeverStateChanged(); } }
    [Export] public bool IsInteractable;

    private bool _leverState;
    private int _currentFrame;
    private Sprite LeverSprite;
    private AudioStreamPlayer2D Sound;
    public AudioStream Lever0Sound;
    public AudioStream Lever1Sound;

    public event Action<int> LeverStateChangedEvent;
    private Interactable Interactable;
    private ShaderMaterial ShaderMaterial = GD.Load<ShaderMaterial>("res://Scripts/Shared/OutlineShader.tres");
    private void OnLeverStateChanged()
    {
        _currentFrame++;
        _currentFrame = Mathf.Wrap(_currentFrame, 0, LeverSprite.Hframes);
        this.LeverSprite.Frame = _currentFrame;
        this.LeverStateChangedEvent?.Invoke(_currentFrame);
        
       // play Btn Up
        if (_currentFrame == 0)
        {
            Sound.Stream = Lever0Sound;
        }
        else
        {
            Sound.Stream = Lever1Sound;
        }
       
        Sound.Play();
        
    }

    public override void _Ready()
    {
        this.LeverSprite = base.GetNode<Sprite>("Sprite");

        this.Interactable = base.GetNode<Interactable>("Interactable");
        this.Interactable.Interacted += OnLeverStateChanged;
        this.Interactable.PlayerEntered += OnPlayerEntered;
        this.Interactable.PlayerExited += OnPlayerExited;

        //
        Sound = (AudioStreamPlayer2D) FindNode("AudioStreamPlayer2D");

        // load sounds
        Lever0Sound = (AudioStream) ResourceLoader.Load("res://Assets/Sound/Lever/Lever0.wav");
        Lever1Sound = (AudioStream) ResourceLoader.Load("res://Assets/Sound/Lever/Lever1.wav");
    }

    private void OnPlayerEntered()
    {
        if (IsInteractable)
        {
            this.LeverSprite.Material = ShaderMaterial;
        }
    }

    private void OnPlayerExited()
    {
        this.LeverSprite.Material = null;
    }
}
