using Godot;
using System;

public partial class Score : Label
{
	signal_bus Signal_Bus;
	Timer _timer;
	int multiplicador = 1;
	int puntuacion = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Signal_Bus = GetNode<signal_bus>("/root/SignalBus");
		_timer = GetNode<Timer>("Timer");
		this.Text = "PUNTUACION " + puntuacion;
		Signal_Bus.Connect(nameof(Signal_Bus.Addpoints),Callable.From(AgregarPuntos));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void AgregarPuntos(){
		puntuacion += 100 * multiplicador;
		this.Text = "PUNTUACION: " + puntuacion;
		Signal_Bus.score = puntuacion;

	}

}
