using Godot;
using System;

public class BaseLevel : Node2D
{
    // nodes
    public Player player;
    public Node2D spawnPoint;
    public override void _Ready()
    {
        // get nodes
        player = GetNode<Player>("Player");
        spawnPoint = GetNode<Node2D>("SpawnPoint");
    
        // set player position
        player.Position = spawnPoint.Position;
    }

}
