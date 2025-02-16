using Godot;

public partial class GameManager : Node {
    public int Level = 0;


    public void NextLevel(){
        Level++;
        GetNode<Label>("/root/Root/CanvasLayer/Level").Text = $"Level {Level}";
    }
}