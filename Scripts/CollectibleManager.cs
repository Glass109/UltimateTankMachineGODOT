using Godot;
using System;

public partial class CollectibleManager : Node
{   
    Timer _timer;
    TileMap _mapa;
    Vector2 mapasize;
    PackedScene _healthpack = (PackedScene) ResourceLoader.Load("res://Escenas/Collectibles/Health.tscn");
    [Export]
    int MAXINSTANCIAS = 5;
    public override void _Ready(){
		_timer = GetNode<Timer>("Timer");   
        _mapa = GetParent().GetNode<TileMap>("TileMap");
        mapasize = _mapa.GetUsedRect().Size;
        _timer.Timeout += OnTimerTimeout;
        var rectangulo = _mapa.GetUsedRect();  
	}

    public void OnTimerTimeout(){
        int numInstancias = this.GetTree().GetNodesInGroup("Vida").Count;
        if(numInstancias < MAXINSTANCIAS){
            TextureRect healthpackInstanec = (TextureRect) _healthpack.Instantiate();
            CallDeferred("add_child",healthpackInstanec);
            healthpackInstanec.GlobalPosition = RandomTile();
            GD.Print("CREADO");
        }
    }
    public Vector2 RandomTile(){
        var rectangulo = _mapa.GetUsedRect();  
        Random rnd = new Random();
        Vector2 victor = new Vector2(rnd.Next(30,rectangulo.Size.X*31),rnd.Next(30,rectangulo.Size.Y*32));
        return victor;
    }

}
