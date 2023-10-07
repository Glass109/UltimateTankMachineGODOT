using Godot;
using System;

public partial class BulletManager : Node2D
{
	[Export]
	float _bulletSpeed = 1000;
	Bullet bulletInstance;
	PackedScene bulletScene = (PackedScene)ResourceLoader.Load("res://Escenas/Bullet.tscn");	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var _player = GetParent().GetNode<Movimiento>("Jugador");
		_player.Disparo += OnJugadorDisparo;
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{

	}

	public void OnJugadorDisparo(){
		bulletInstance = (Bullet) bulletScene.Instantiate();
		CallDeferred("add_child",bulletInstance);
		bulletInstance.canKillEnemies = true;
		Marker2D _SalidaBala = (Marker2D) GetParent().GetNode("Jugador/TopTanq/SalidaBala");
		bulletInstance.Position = (Vector2) _SalidaBala.GlobalPosition;
		bulletInstance.Rotation = _SalidaBala.GlobalRotation;
		bulletInstance.ApplyImpulse(new Vector2((float) Math.Sin(bulletInstance.Rotation) * _bulletSpeed, (float) -Math.Cos(bulletInstance.Rotation) * _bulletSpeed ));
		
	}
}
