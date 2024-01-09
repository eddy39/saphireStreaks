using Godot;
using System;

public class Laser : Node2D
{
    // nodes
    public Sprite Emitter;
    public RayCast2D RayCast;
    public Line2D line;
    public Tween tween;
    public Timer PeriodicTimer;
    //
    public Particles2D particlesBeam;
    public AudioStreamPlayer2D audioLaser;

    private bool IsCasting = false;
    

    public enum FiringMode
    {
        Continuous,
        Periodic,
        
    }
    [Export] public FiringMode firingMode = FiringMode.Continuous;
    [Export] public float periodLength = 2f;
    public override void _Ready()
    {
        // get nodes
        line = (Line2D) FindNode("Line2D");
        tween = (Tween) FindNode("Tween");
        particlesBeam = (Particles2D) FindNode("particlesBeam");
        audioLaser = (AudioStreamPlayer2D) FindNode("audioLaser");
        RayCast = (RayCast2D) FindNode("RayCast2D");
        PeriodicTimer = (Timer) FindNode("PeriodicTimer");
        // initial settings
        SetPhysicsProcess(false);
        line.Width = 0;
        RayCast.CastTo = new Vector2(100000,0);
        //
        ShutOnLaser(true);
    }
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Vector2 castPoint = RayCast.CastTo;
        RayCast.ForceRaycastUpdate();
        if (RayCast.IsColliding())
        {
            castPoint = ToLocal(RayCast.GetCollisionPoint());
            //Particles2DEnd.Position = castPoint;
            //Particles2DEnd.GlobalRotation = RayCast.GetCollisionNormal().Angle();
            // tell colliding object to die
            if (RayCast.GetCollider() is Player)
            {
                Player damageable = (Player) RayCast.GetCollider();
                damageable.Die();
            }
        }
        //Particles2DEnd.Emitting = RayCast.IsColliding();


        line.SetPointPosition(1,castPoint);
        
    }
    public void SwitchLaserOnOff(bool off)
    {
        if (!off)
        {
            ShutOnLaser(off);
        }
        else
        {
            ShutOffLaser(off);
        }
    }
    public void ShutOnLaser(bool _)
    {
        if (firingMode == FiringMode.Continuous)
        {
            CastLaser();
        }
        else if (firingMode == FiringMode.Periodic)
        {
            //create timers to start and top laser
            
            PeriodicTimer.Connect("timeout", this, nameof(SwitchLaser));
            PeriodicTimer.WaitTime = periodLength;
            PeriodicTimer.Start();
            
        }
    }
    public void ShutOffLaser(bool _)
    {
        if (firingMode == FiringMode.Continuous)
        {
            StopLaser();
        }
        else if (firingMode == FiringMode.Periodic)
        {
            //create timers to start and top laser
            
            PeriodicTimer.Disconnect("timeout", this, nameof(SwitchLaser));
            PeriodicTimer.Stop();

            StopLaser();
            
        }
    }
    private void SwitchLaser()
    {
        if (IsCasting)
        {
            StopLaser();
        }
        else
        {
            CastLaser();
        }
    }
    public void CastLaser()
    {
        // 
        if( IsCasting) return;
        // Set Casting
        IsCasting = true;
        SetPhysicsProcess(true);
        //particles2D.Emitting = true;
        particlesBeam.Emitting = true;
        //
        particlesBeam.ProcessMaterial.Set("emission_box_extents",new Vector3(RayCast.CastTo.Length()/2,0,0));
        //
        tween.InterpolateProperty(line,"width",line.Width,25,.1f);
        tween.Start();
        // 
        audioLaser.Play();
        // 
        
    }
    public void StopLaser()
    {
        if( !IsCasting) return;
        // Set Casting
        IsCasting = false;
        SetPhysicsProcess(false);
        //particles2D.Emitting = false;

        //Particles2DEnd.Emitting = false;
        //
        tween.InterpolateProperty(line,"width",line.Width,0,.1f);
        tween.Start();
        // 
        audioLaser.Stop();
    }
}
