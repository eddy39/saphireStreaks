using Godot;
using System;

public class Door : StaticBody2D
{
    [Export] public Gem.Color DoorColor;
    private Sprite DoorSprite;
    private CollisionShape2D Collider;
    public override void _Ready()
    {
        this.DoorSprite = this.GetNode<Sprite>("Sprite");
        this.Collider = this.GetNode<CollisionShape2D>("Collider");

        // I tried to avoid using singleton but ok

        // On gems updated, check if the color matches with door color
        // if matches sets the sprite's transparency to 0.5 (temporary)
        // and disable its collision

        /* GameState.GemsUpdatedNotifier += (color) => {
            if (DoorColor == color)
            {
                this.DoorSprite.Modulate = new Color
                (
                    this.DoorSprite.Modulate.r,
                    this.DoorSprite.Modulate.g,
                    this.DoorSprite.Modulate.b,
                    0.5f
                );

                Collider.Disabled = true;
            }
        }; */
    }
    public void OpenCloseDoor(bool open)
    {
        if (open)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }
    public void OpenDoor()
    {
        this.DoorSprite.Modulate = new Color
        (
            this.DoorSprite.Modulate.r,
            this.DoorSprite.Modulate.g,
            this.DoorSprite.Modulate.b,
            0.5f
        );

        // turn off collision
        Collider.SetDeferred("disabled", true);
    }
    public void CloseDoor()
    {
        this.DoorSprite.Modulate = new Color
        (
            this.DoorSprite.Modulate.r,
            this.DoorSprite.Modulate.g,
            this.DoorSprite.Modulate.b,
            1f
        );

        Collider.SetDeferred("disabled", false);
    }
}

