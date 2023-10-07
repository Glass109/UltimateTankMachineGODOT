using Godot;
using System;

public partial class Movimiento : CharacterBody2D
{
	signal_bus Signal_Bus;

	AnimatedSprite2D _hpbar_camera;
	TileMap mapa;
	AnimationPlayer _animador;
	AudioStreamPlayer _sfxDisparo;
	CollisionShape2D _collision;
	
	Area2D _areacolision;
	double _angular_speed = Math.PI;
	Sprite2D _toptanq;
	[Export]
	Vector2 _multiplicadorVelocidad = new Vector2(2,2);
	[Export(PropertyHint.Range,"0f,1f,.2")]
	double _cadenciaFuego = 0.5f;
	double _tUltimoDisparo = 0;
	[Export]
	Vector2 vaceleracion = new Vector2(1,1);
	[Signal]
	public delegate void DisparoEventHandler();
	Vector2 _screen_size = Vector2.Zero;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animador = GetNode<AnimationPlayer>("AnimationPlayer");
		_areacolision = GetNode<Area2D>("AreaDetectar");
		Signal_Bus = GetNode<signal_bus>("/root/SignalBus");
		_toptanq = (Sprite2D)GetNode("TopTanq");
		_collision = GetNode<CollisionShape2D>("ColisionTanque");
		_screen_size = GetViewportRect().Size;
		mapa = GetParent().GetNode<TileMap>("TileMap");
		_sfxDisparo = GetNode<AudioStreamPlayer>("SonidoDisparo");
		_hpbar_camera = GetTree().CurrentScene.GetNode<AnimatedSprite2D>("GUI/HPBAR/Camara");
		
		_areacolision.AreaEntered += OnAreaEntered;
		_areacolision.BodyEntered += OnBodyEntered;
	}
	
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		this.Apuntar();
		this.Moverse(delta);
		this.Position = new Vector2(Mathf.Clamp(this.Position.X,0,mapa.GetUsedRect().Size.X*32),Mathf.Clamp(this.Position.Y,0,mapa.GetUsedRect().Size.Y*32));
		
		if (Input.IsActionPressed("Balazo")&& _tUltimoDisparo <= 0)	//Revisa si ya podemos disparar o no
		{
			this.Disparar();
		}
    }
	private void OnAreaEntered(Node body){
		if(body.GetParent().IsInGroup("Vida")){
			Signal_Bus.EmitSignal("AddHealth");
			GD.Print("ADDHEALTH EMITTED");
			body.GetParent().QueueFree();
		}
		if(body.GetParent().IsInGroup("Ammo")){

		}
	}
	private void OnBodyEntered(Node body){
		if(body.IsInGroup("Bala")){
			_areacolision.SetDeferred("monitoring",false);
			_animador.Play("Invencibility");
			Signal_Bus.EmitSignal("TakeDamage");
			body.QueueFree();
		}
		
	}

	//Hace que la parte de arriba mire hacia el cursor
	public void Apuntar(){	
		
		_toptanq.LookAt(GetGlobalMousePosition());
		_toptanq.Rotate((float)(90*Math.PI/180));
		
		// var APUNTAR = Input.GetVector("RS_LEFT","RS_RIGHT","RS_UP","RS_DOWN");
		// _toptanq.LookAt(APUNTAR + _toptanq.GlobalPosition);

		// if (APUNTAR != Vector2.Zero)
		// {
		// 			_toptanq.Rotate((float)(90*Math.PI/180));
			
		// }	
	}
	//Controla todo el movimiento, incluidos limites
	public void Moverse(double delta){
		Vector2 _vectorDelta = new Vector2((float)delta,(float)delta);
		_tUltimoDisparo -= delta;
		int direction = 0;
		if(Input.IsActionPressed("ui_left")) direction = -1;
		if(Input.IsActionPressed("ui_right")) direction = 1;
		this.Rotation += (float)(direction*_angular_speed*delta);
		var velocidadVector = Vector2.Zero;
		if(Input.IsActionPressed("ui_up"))
			velocidadVector = Vector2.Up.Rotated(Rotation) * _multiplicadorVelocidad;
		if(Input.IsActionPressed("ui_down"))
			velocidadVector = Vector2.Down.Rotated(Rotation) * _multiplicadorVelocidad;

		this.Position += (velocidadVector);
		MoveAndSlide();
	}
	//Funcion para disparar
	public void Disparar(){
		_tUltimoDisparo = _cadenciaFuego;
		_hpbar_camera.Play();
		_sfxDisparo.Play();
		EmitSignal(nameof(Disparo));
		
	}
}