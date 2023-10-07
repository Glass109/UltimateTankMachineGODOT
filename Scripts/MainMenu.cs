using Godot;
using System;

public partial class MainMenu : Control
{
	Button _start, _credits, _exit;
	AnimationPlayer animador;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_start = GetNode<Button>("VBoxContainer/Start");
		_credits = GetNode<Button>("VBoxContainer/Credits");
		_exit = GetNode<Button>("VBoxContainer/Exit");
		animador = GetNode<AnimationPlayer>("AnimationPlayer");
		_exit.Pressed += Salir;
		_start.Pressed += Empezar;
		_credits.Pressed += Creditos;
		animador.Play("Inicio");
		_start.GrabFocus();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	void Empezar(){
		GetTree().ChangeSceneToFile("res://Escenas/NivelTest.tscn");
	}
	void Creditos(){}
	void Salir(){
		GetTree().Quit();
	}
}
