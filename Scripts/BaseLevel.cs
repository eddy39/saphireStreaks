using Godot;
using System;

public class BaseLevel : Node2D
{
    // nodes
    public Player player;
    public LevelCamera levelCamera;
    public Node2D spawnPoint;
    public UI ui;
    public override void _Ready()
    {
        // get nodes
        player = GetNode<Player>("Player");
        spawnPoint = GetNode<Node2D>("SpawnPoint");
        levelCamera = GetNode<LevelCamera>("LevelCamera");
        ui = GetNode<UI>("UI");
        // hand player to camera
        levelCamera.player = player;
        levelCamera.Current = true;
        // set player position
        player.Position = spawnPoint.Position;
        player.spawnPosition = spawnPoint.Position;
        levelCamera.Position = spawnPoint.Position;
        //
        
    }

}
