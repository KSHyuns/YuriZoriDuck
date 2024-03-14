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
    감자 = 0,
    고구마,
    누룽지,
    물,
    설탕,
    소금,
    식용류,
    MAX
}

public interface ISlide
{
    public void SlideOn(Block scTile , Block changeTile);
}
