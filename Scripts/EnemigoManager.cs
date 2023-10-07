using Godot;
using System;

public partial class EnemigoManager : Node2D
{   
    Timer _timer;
    TileMap _mapa;
    Vector2 mapasize;
    PackedScene _enemigo = (PackedScene) ResourceLoader.Load("res://Escenas/EnemigoNegro.tscn");
    [Export]
    int MAXINSTANCIAS = 10;
    public override void _Ready(){
		_timer = GetNode<Timer>("Timer");   
        _mapa = GetParent().GetNode<TileMap>("TileMap");
        mapasize = _mapa.GetUsedRect().Size;
        _timer.Timeout += OnTimerTimeout;
        var rectangulo = _mapa.GetUsedRect();  
	}

    public void OnTimerTimeout(){
        int numInstancias = this.GetTree().GetNodesInGroup("Enemigos").Count;
        if(numInstancias < MAXINSTANCIAS){
            EnemigoNegro enemyInstance = (EnemigoNegro) _enemigo.Instantiate();
            CallDeferred("add_child",enemyInstance);
            enemyInstance.GlobalPosition = RandomTile();
        }
    }
    public Vector2 RandomTile(){
        var rectangulo = _mapa.GetUsedRect();  
        Random rnd = new Random();
        Vector2 victor = new Vector2(rnd.Next(rectangulo.Size.X*32),rnd.Next(rectangulo.Size.Y*32));
        return victor;
    }

}
