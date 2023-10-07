using Godot;
using System;

public partial class SMenuPausa : Control
{
	Button B1,B2,B3,B4;
	AnimationPlayer _animador;
	signal_bus Signal_Bus;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animador = GetNode<AnimationPlayer>("AnimationPlayer");
		Signal_Bus = GetNode<signal_bus>("/root/SignalBus");
		B1 = GetNode<Button>("CenterContainer/B1");
		B2 = GetNode<Button>("CenterContainer/B2");
		B3 = GetNode<Button>("CenterContainer/B3");
		B4 = GetNode<Button>("CenterContainer/B4");


		_animador.AnimationFinished += FinallyExit;
		Signal_Bus.Connect(nameof(Signal_Bus.Pausa), Callable.From(OnPausa));
		B1.Pressed += Continuar;
		B2.Pressed += Reiniciar;
		B3.Pressed += MainMenu;
		B4.Pressed += Quit;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//EVITA PONER ALGO ACA
	}


	public void Continuar(){
		_animador.PlayBackwards("Exit");
	}
	public void Reiniciar(){
		GetTree().ReloadCurrentScene();
		Signal_Bus.EmitSignal("Reset");
		GetTree().Paused = false;
	}
	public void MainMenu(){
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://Escenas/MainMenu.tscn");
		Signal_Bus.EmitSignal("Reset");
	}
	public void Quit(){
		GetTree().Quit();
	}
	public void OnPausa(){
		Visible = true;
		_animador.Play("Enter");

	}
	public void FinallyExit(StringName animationname){
		if(animationname == "Exit"){
			Signal_Bus.EmitSignal("Despausa");
			Visible = false;
			GetTree().Paused = false;
		}
	}
}
