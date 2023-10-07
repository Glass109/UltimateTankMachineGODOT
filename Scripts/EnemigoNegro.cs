using Godot;
using System;

public partial class EnemigoNegro : RigidBody2D
{
	signal_bus Signal_Bus;
	AnimationPlayer _ap;
	//[Export]
	//Movimiento _target;
	[Export]
	Vector2 speed = new Vector2(2,2);
	[Export]
	float distancia_max = 500;
	Movimiento _player;
	NavigationAgent2D _nAgent;
	Timer _timer,_timer2;
	Sprite2D _toptanq;
	bool CanShoot = true;
	PackedScene bulletScene = (PackedScene)ResourceLoader.Load("res://Escenas/Bullet.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Signal_Bus = GetNode<signal_bus>("/root/SignalBus");
		_ap = GetNode<AnimationPlayer>("Animacion");
		_player = GetParent().GetParent().GetNode<Movimiento>("Jugador");
		_nAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_timer = GetNode<Timer>("Timer");
		_timer2 = GetNode<Timer>("Timer2");
		_toptanq = GetNode<Sprite2D>("TopTanque");

		_timer.Timeout += Retarget;
		this.BodyEntered += OnBulletCollision;
		_timer2.Timeout += Disparar;
		Retarget();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		
	}
	public override void _PhysicsProcess(double delta){
		var _nextpos = _nAgent.GetNextPathPosition();
		Vector2 dir = (_nextpos - this.GlobalPosition).Normalized();
		float dirangle = (_nextpos - this.GlobalPosition).Angle();
		MoveAndCollide((dir * speed));
		_toptanq.LookAt(_player.GlobalPosition);
		_toptanq.Rotate((float)Math.PI/2);
	}

	public void Retarget(){
		
		_nAgent.TargetPosition = _player.GlobalPosition;
		LookAt(_nAgent.GetNextPathPosition());
		
	}
	public void OnBulletCollision(Node body){
		if(body.IsInGroup("Bala")){
			Bullet balaCast = (Bullet)body;
			if(balaCast.canKillEnemies){
				CanShoot = false;
				_ap.Play("Explosion");
				Signal_Bus.EmitSignal("Addpoints");
			}
		}
	}
	public void Disparar(){
		var distancia = this.GlobalPosition.DistanceTo(_player.GlobalPosition);
		if (CanShoot && distancia < distancia_max){
			var bulletInstance = (Bullet) bulletScene.Instantiate();
			GetParent().CallDeferred("add_sibling",bulletInstance);
			Marker2D _SalidaBala = (Marker2D) GetNode("TopTanque/Marker2D");
			bulletInstance.SetCollisionMaskValue(2,false);
			bulletInstance.Position = (Vector2) _SalidaBala.GlobalPosition;
			bulletInstance.Rotation = _SalidaBala.GlobalRotation - 90;
			bulletInstance.ApplyImpulse(new Vector2(-600,0).Rotated(_SalidaBala.GlobalRotation));
		}
		
	}
	public void setTimer(int numInstancias){
		_timer.WaitTime = 1/numInstancias;
	}
}
