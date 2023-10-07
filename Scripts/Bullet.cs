using Godot;
using System;

public partial class Bullet : RigidBody2D
{
	AnimationPlayer _animador;
	Timer _timer;
	public bool canKillEnemies = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animador = GetNode<AnimationPlayer>("Animacion");
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += Poof;
		this.BodyEntered += Chocar;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	public void Chocar(Node body){
		this.SetCollisionLayerValue(4,false);
		_animador.Play("ExpTouch");
	}
		
	public void Poof(){
		this.SetCollisionLayerValue(4,false);
		_animador.Play("ExpTimeOut");
	}
	
}
