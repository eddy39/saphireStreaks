using Godot;
using System;

public class AfterImage : ThrowBox
{
    private Area2D Detector;
    public Interactable interactable;
    public AudioStreamPlayer2D AfterImageAudio;
    public AnimatedSprite animatedSprite;
    private ShaderMaterial ShaderMaterial = GD.Load<ShaderMaterial>("res://Scripts/Shared/OutlineShader.tres");
    /* public Vector2 velocity = new Vector2();
    public float gravity = 15; */
    public override void _Ready()
    {
        this.AfterImageAudio = base.GetNode<AudioStreamPlayer2D>("AfterImageAudio");

        this.Detector = base.GetNode<Area2D>("Detector");
        this.animatedSprite = base.GetNode<AnimatedSprite>("AnimatedSprite");
        // interactble stuff
        this.interactable = GetNode<Interactable>("Interactable");
        
        this.interactable.PlayerEntered += OnPlayerEntered;
        this.interactable.PlayerExited += OnPlayerExited;

        // load afterimage appear sound
        this.AfterImageAudio.Stream = (AudioStream) ResourceLoader.Load("res://Assets/Sound/AfterImage/Afterimage_Appear.wav");
        this.AfterImageAudio.Play();
    }
    public override void _UnhandledKeyInput(InputEventKey @event)
    {
        // No controls
        //base._UnhandledKeyInput(@event);
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        // No controls
        //base._UnhandledKeyInput(@event);
    }
    public override void _Input(InputEvent @event)
    {
        // No controls
        //base._UnhandledKeyInput(@event);
    }
    public override void _PhysicsProcess(float delta)
    {
        // No base physics
        base._PhysicsProcess(delta);
        /* // just falling
        velocity.y += gravity;
        // apply velocity
        MoveAndSlide(velocity, Vector2.Up); */
    }
    
    private void OnPlayerEntered()
    {
        
        this.animatedSprite.Material = ShaderMaterial;
        
    }

    private void OnPlayerExited()
    {
        this.animatedSprite.Material = null;
    }

    // Detonate ability
    // Haven't added anything yet
    public void Detonate()
    {
        foreach (var colObject in this.Detector.GetOverlappingBodies())
        {
            if (colObject is Laser laser)
            {
                if (laser.CanBeDisabled)
                    laser.OverloadLaser();
                    
            }
            if (colObject is DestructableBox box)
            {
                box.Boom();
            }
        }
        // load explosion sound
        this.AfterImageAudio.Stream = (AudioStream) ResourceLoader.Load("res://Assets/Sound/AfterImage/Explosion_Sound_Effect.wav");
        this.AfterImageAudio.Play();
        // connect to finished
        this.AfterImageAudio.Connect("finished", this, nameof(OnAudioBoomFinished));
        // stop movement
        this.velocity = Vector2.Zero;
        // set gravity to 0
        this.gravity = 0;
    }
    public void OnAudioBoomFinished()
    {
        this.AfterImageAudio.Disconnect("finished", this, nameof(OnAudioBoomFinished));
        QueueFree();
    }
    
}
