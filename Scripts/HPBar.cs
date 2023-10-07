using Godot;
using System;

public partial class HPBar : Control
{
	AnimatedSprite2D _barra,_camara;
	signal_bus Signal_Bus;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Signal_Bus = GetNode<signal_bus>("/root/SignalBus");
		_barra = GetNode<AnimatedSprite2D>("Barra");
		_camara = GetNode<AnimatedSprite2D>("Camara");
	
		Signal_Bus.Connect(nameof(Signal_Bus.UpdateUI1),Callable.From(OnTakeDamage));
		Signal_Bus.Connect(nameof(Signal_Bus.UpdateUI2),Callable.From(OnAddHealth));
		_barra.SetFrameAndProgress(Signal_Bus.vida,0f);

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	public void OnTakeDamage(){
		
		_barra.SetFrameAndProgress((Signal_Bus.vida),0f);
		if(Signal_Bus.vida <= 0){
			Signal_Bus.EmitSignal("GameOver");
		}
	}
	public void OnAddHealth(){
		
		_barra.SetFrameAndProgress(Signal_Bus.vida,0f);
	}
}
