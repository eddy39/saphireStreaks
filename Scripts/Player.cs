using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class Player : KinematicBody2D
{
    // vars
    public Vector2 velocity = new Vector2();
    private bool isJumping = false;
    private bool wasJustOnFloor = false;
    private bool fallQuick = false;
    private float coyoteTimer = 0;
    private bool nearApex = false;
    private bool bufferJump = false;
    private float bufferJumpTimer = 0;
    // movement parameters
    [Export] public float acceleration = 7; // acceleration now
    [Export] public float jumpStrength = 640;
    [Export] public float gravity = 20;
    public float maxSpeed = 200;
    public float quickFallFactor = 1.5f;
    public float slowFallFactor = 0.90f;
    public float coyoteTimeLimit = .1f;
    public float bufferJumpTimeLimit = .2f;
    public Vector2 spawnPosition = new Vector2(0, 0);
    // Abilities
    public float afterImageCooldownTimeLimit = 7;
    private bool afterImageOnColldown = false;
    public AfterImage afterImage;
    public Area2D VacuumArea;
    public Node2D VaccumPoint;
    public Timer afterImageCooldownTimer;

    public event Action<float> AfterImageUsed;
    public event Action AfterImageConsumed;
    public event Action AfterImageDetonated;
    
    
    //private Sprite PlayerSprite;
    public List<ThrowBox> VacuumedBodies = new List<ThrowBox>();
    public float succtionForce = 30;
    public float vacuumThrowForce = 1500;
    public bool vaccumActive = false;
    // Sprites
    public AnimatedSprite animatedSprite;
    private Particles2D JumpParticle;
    private Particles2D MovementPartice;
    public override void _Ready()
    {
        // Get Nodes
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");

        VacuumArea = GetNode<Area2D>("VacuumArea");
        VaccumPoint = GetNode<Node2D>("VacuumArea/VaccumPoint");
        JumpParticle = GetNode<Particles2D>("JumpParticle");
        MovementPartice = GetNode<Particles2D>("MovementParticle");

        // Connect Signals
        VacuumArea.Connect("body_entered", this, nameof(OnVacuumAreaBodyEntered));
        VacuumArea.Connect("body_exited", this, nameof(OnVacuumAreaBodyExited));
    }
    public override void _UnhandledKeyInput(InputEventKey @event)
    {
        base._UnhandledKeyInput(@event);
        // check if Ability is pressed
        if (@event.IsActionPressed("ability"))
        {
            // check if ability is unlocked
            if (GameState.Gems[(int)Gem.Color.Purple])
            {
                // check if ability is not on cooldown
                if (this.afterImage != null)
                {
                    // use ability
                    SubstituteToAfterImage();       
                }
            }

            // check if ability is unlocked
            if (GameState.Gems[(int)Gem.Color.Blue])
            {
                // check if ability is not on cooldown
                if (!afterImageOnColldown || this.afterImage == null)
                {
                    // use ability
                    UseAfterImage();

                }
            }
        }

        // Check if detonate key is pressed [X]
        if (@event.IsActionPressed("detonate"))
        {
            if (!GameState.Gems[(int)Gem.Color.Red]) // if the player doesnt have red gem return
                return;

            if (afterImage is null) // if after image doesn't exist, return
                return;

            //GameState.RemoveGem(Gem.Color.Red);
            afterImage.Detonate(); //detonate after image
            AfterImageDetonated?.Invoke();
        }
        // vacuum ability
        if (@event.IsActionPressed("vacuum"))
        {
            // check if ability is unlocked
            if (GameState.Gems[(int)Gem.Color.Yellow])
            {

                // use ability
                vaccumActive = true;

            }

        }
        if (@event.IsActionReleased("vacuum"))
        {
            // check if ability is unlocked
            if (GameState.Gems[(int)Gem.Color.Yellow])
            {

                // throw stuff away
                VaccuumThrow();
                vaccumActive = false;
            }

        }
    }
    public void UseVacuum()
    {
        foreach (ThrowBox kinBody in VacuumedBodies)
        {
            // direction from kinbody to VaccumPoint
            Vector2 direction = VaccumPoint.GlobalPosition - kinBody.GlobalPosition;
            kinBody.velocity += direction.Normalized() * succtionForce;
            // clamp velocity
            velocity = new Vector2(Mathf.Clamp(velocity.x, -200, 200), Mathf.Clamp(velocity.y, -200, 200));
        }

    }
    public void VaccuumThrow()
    {
        foreach (ThrowBox kinBody in VacuumedBodies)
        {
            // direction from player to mouse
            Vector2 direction = GetGlobalMousePosition() - GlobalPosition;
            kinBody.velocity += direction.Normalized() * vacuumThrowForce;
            //kinBody.MoveAndSlide(direction.Normalized()*succtionForce, Vector2.Up);
        }
    }
    public void OnVacuumAreaBodyEntered(Node body)
    {
        if (body is ThrowBox kinBody)
        {
            VacuumedBodies.Add(kinBody);
        }
    }
    public void OnVacuumAreaBodyExited(Node body)
    {
        if (body is ThrowBox kinBody)
        {
            VacuumedBodies.Remove(kinBody);
        }
    }
    public void UseAfterImage()
    {
        // spawn after image
        afterImage = (AfterImage)ResourceLoader.Load<PackedScene>("res://Scenes/PlayerAbilities/AfterImage.tscn").Instance();
        afterImage.Position = Position;
        
        // add this to CollisionException
        afterImage.AddCollisionExceptionWith(this);
        /// add after image to scene
        GetParent().AddChild(afterImage);
        // Connect to interact
        afterImage.interactable.Interacted += ConsumeAfterImage;
        // set after image on cooldown
        afterImageOnColldown = true;
        // start cooldown timer
        afterImageCooldownTimer = new Timer
        {
            OneShot = true,
            WaitTime = afterImageCooldownTimeLimit
        };

        afterImageCooldownTimer.Connect("timeout", this, nameof(AfterImageCooldownTimerTimeout)
        , new Godot.Collections.Array(afterImageCooldownTimer));
        AddChild(afterImageCooldownTimer);
        afterImageCooldownTimer.Start();
        // emit event
        AfterImageUsed?.Invoke(afterImageCooldownTimeLimit);

        // On After image deletion, call OnAfterImageExited
        afterImage.Connect("tree_exited", this, nameof(OnAfterImageExited));
    }
    public void ConsumeAfterImage()
    {
        // remove after image
        if (!(afterImage is null))
        {
            afterImage.QueueFree();
            afterImage = null;
        }
        // set after image off cooldown
        afterImageOnColldown = false;
        // remove timer
        afterImageCooldownTimer.Disconnect("timeout", this, nameof(AfterImageCooldownTimerTimeout));
        afterImageCooldownTimer.QueueFree(); 
        //
        AfterImageConsumed?.Invoke();
    }
    public void SubstituteToAfterImage()
    {
        // add collision exception
        afterImage.AddCollisionExceptionWith(this);
        this.AddCollisionExceptionWith(afterImage);
        // swap positions
        Vector2 prevPosition = new Vector2(this.GlobalPosition);
        this.GlobalPosition = afterImage.GlobalPosition;
        afterImage.GlobalPosition = prevPosition;
        // reset velocity
        velocity = Vector2.Zero;
        afterImage.velocity = Vector2.Zero;
    }

    // This function is invoked when after image exited
    private void OnAfterImageExited()
    {
        // sets after image to null
        afterImage = null;
    }

    public void AfterImageCooldownTimerTimeout(Timer timer)
    {
        // set after image off cooldown
        afterImageOnColldown = false;
        // remove timer
        timer.QueueFree();

        // remove after image
        if (!(afterImage is null))
        {
            afterImage.QueueFree();
            afterImage = null;
        }

    }
    public override void _PhysicsProcess(float delta)
    {
        // Player Movement

        // Get the input vector
        Vector2 input_vector = new Vector2();
        input_vector.x = Input.GetActionStrength("right") - Input.GetActionStrength("left");
        input_vector.y = Input.GetActionStrength("down") - Input.GetActionStrength("up");

        MovementPartice.Emitting = input_vector.Length() != 0 && base.IsOnFloor();
        
        // update coyote timer
        coyoteTimer += delta;
        if (coyoteTimer > coyoteTimeLimit)
        {
            wasJustOnFloor = false;
        }
        // update buffer jump timer
        bufferJumpTimer += delta;
        if (bufferJumpTimer > bufferJumpTimeLimit)
        {
            bufferJump = false;
        }
        // check if on floor
        if (IsOnFloor())
        {
            // set jumping to false
            isJumping = false;
            // set fallQuick to false
            fallQuick = false;
            // set wasJustOnFloor to true
            wasJustOnFloor = true;
            coyoteTimer = 0;

        }
        // check if approaching apex
        if (!wasJustOnFloor & (velocity.y > -gravity / 2))
        {
            // set nearApex to true
            nearApex = true;
        }
        // check if past apex 
        if (nearApex & (velocity.y > gravity / 2))
        {
            // set nearApex to false
            nearApex = false;
        }
        // jump begin
        if (Input.IsActionJustPressed("jump") & (IsOnFloor() | wasJustOnFloor))
        {
            // jump
            velocity.y = -jumpStrength;
            this.JumpParticle.Emitting = true;
            // this.JumpParticle.GetChild<Particles2D>(0).Emitting = true;
            // set jumping to true
            isJumping = true;
            // set fallQuick to false
            fallQuick = false;
        }
        // jump buffer
        if (Input.IsActionJustPressed("jump") & (isJumping))
        {
            // set bufferJump to true
            bufferJump = true;
            bufferJumpTimer = 0;
        }
        // check if jump released early
        if (@Input.IsActionJustReleased("jump") & isJumping)
        {
            // stop jumping
            velocity.y = 0;
            // set jumping to false
            isJumping = false;
            // fall faster
            fallQuick = true;
        }
        // apply gravity
        if (fallQuick)
        {
            // fall faster
            velocity.y += gravity * quickFallFactor;
        }
        else if (nearApex)
        {
            // fall slower
            velocity.y += gravity * slowFallFactor;
        }
        else
        {
            // fall normally
            velocity.y += gravity;
        }
        // sideways movement
        velocity.x += input_vector.x * acceleration;
        // apply friction
        if (Mathf.Abs(input_vector.x) < 0.01f)
        {
            velocity.x *= 0.8f;
        }
        // move player
        velocity = MoveAndSlide(velocity, Vector2.Up);

        // clamp velocity
        velocity.y = Mathf.Clamp(velocity.y, -jumpStrength, jumpStrength);
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        // ##### Animation
        if (velocity.x != 0)
        {
            animatedSprite.Play("Walk");
            animatedSprite.FlipH = velocity.x < 0;
        }
        else
        {
            animatedSprite.Play("Idle");
        }
        // ##### Vacuum Ability Rotation
        // rotate vaccum area toward mouse position
        VacuumArea.Rotation = GetGlobalMousePosition().AngleToPoint(GlobalPosition);
        //
        if (vaccumActive)
        {
            UseVacuum();
        }
        // check distance to afterimage
        if (!(afterImage is null))
        {
            if (GlobalPosition.DistanceTo(afterImage.GlobalPosition) > 60)
            {
                // remove collision exception
                afterImage.RemoveCollisionExceptionWith(this);
                this.RemoveCollisionExceptionWith(afterImage);
            }
        }
    }
    public void Die()
    {
        Position = spawnPosition;
    }
}
