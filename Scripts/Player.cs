using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

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
    public float speed = 500;
    public float jumpStrength = 700;
    public float gravity = 15;
    public float quickFallFactor = 1.5f;
    public float slowFallFactor = 0.95f;
    public float coyoteTimeLimit = .1f;
    public float bufferJumpTimeLimit = .2f;
    // Abilities
    public float afterImageCooldownTimeLimit = 1;
    private bool afterImageOnColldown = false;
    public AfterImage afterImage;
    
    public override void _Ready()
    {
        
    }
    public override void _UnhandledKeyInput(InputEventKey @event)
    {
        base._UnhandledKeyInput(@event);
        // check if Ability is pressed
        if (@event.IsActionPressed("ability"))
        {
            // check if ability is unlocked
            if (GameState.Gems[(int)Gem.Color.Blue])
            {
                // check if ability is not on cooldown
                if (!afterImageOnColldown)
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
            
            afterImage.Detonate(); //detonate after image
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
        // set after image on cooldown
        afterImageOnColldown = true;
        // start cooldown timer
        Timer afterImageCooldownTimer = new Timer
        {
            OneShot = true,
            WaitTime = afterImageCooldownTimeLimit
        };

        afterImageCooldownTimer.Connect("timeout", this, nameof(AfterImageCooldownTimerTimeout),new Godot.Collections.Array(afterImageCooldownTimer));
        AddChild(afterImageCooldownTimer);
        afterImageCooldownTimer.Start();

        // On After image deletion, call OnAfterImageExited
        afterImage.Connect("tree_exited", this, nameof(OnAfterImageExited));
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
            afterImage.QueueFree();
    }
    public override void _PhysicsProcess(float delta)
    {
        // Player Movement

        // Get the input vector
        Vector2 input_vector = new Vector2();
        input_vector.x = Input.GetActionStrength("right") - Input.GetActionStrength("left");
        input_vector.y = Input.GetActionStrength("down") - Input.GetActionStrength("up");
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
        if (!wasJustOnFloor& (velocity.y > -gravity/2))
        {
            // set nearApex to true
            nearApex = true;
        }
        // check if past apex 
        if (nearApex & (velocity.y > gravity/2))
        {
            // set nearApex to false
            nearApex = false;
        }
        // jump begin
        if (Input.IsActionJustPressed("jump")&(IsOnFloor()|wasJustOnFloor))
        {
            // jump
            velocity.y = -jumpStrength;
            // set jumping to true
            isJumping = true;
            // set fallQuick to false
            fallQuick = false;  
        }
        // jump buffer
        if (Input.IsActionJustPressed("jump")&(isJumping))
        {
            // set bufferJump to true
            bufferJump = true;
            bufferJumpTimer = 0;
        }
        // check if jump released early
        if (@Input.IsActionJustReleased("jump")&isJumping)
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
        
        velocity.x = input_vector.x * speed;
        velocity = MoveAndSlide(velocity, Vector2.Up);

        // clamp velocity
        velocity.y = Mathf.Clamp(velocity.y, -jumpStrength, jumpStrength/2);
    }
}
