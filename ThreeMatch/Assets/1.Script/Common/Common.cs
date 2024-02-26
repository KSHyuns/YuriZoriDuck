[System.Serializable]
public struct Point 
{ 
    public int x; 
    public int y; 

    public Point (int _x , int _y)
    {
        x = _x; y = _y;   
    }

}
[System.Serializable]
public class curAction
{
    public AddItem item;
    public int curCnt;

    public curAction() { }

    public curAction(AddItem _item, int _cut)
    {
        item = _item;
        curCnt = _cut;
    }
}
public enum CookingKind
{
    FLOUR = 0,
    SALT,
    OIL,
    CHICKEN,
    ONIONS,
    CARROT,
    MAX
}

public interface ISlide
{
    public void SlideOn(Block scTile , Block changeTile);
}
