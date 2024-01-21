[System.Serializable]
public struct Point 
{ 
    public int x; 
    public int y; 
    public Point (int _x , int _y)
    {
     x = _x;
     y = _y;   
    }

}

public enum TileKind
{
    Kats_1 = 0,
    Kats_2,
    Kats_3,
    Kats_4,
    Kats_5,
    Kats_6,
    Kats_7,

}

public interface ISlide
{
    public void SlideOn();
}
