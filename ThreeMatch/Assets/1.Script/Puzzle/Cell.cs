using System.Collections;
using System.Collections.Generic;
using EPOOutline;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Point cellPoint;

    public Vector2 position;

    public Block block;             //블럭이지워지면 block = null        
                                    
    public Cell bottom_Node;       //아래 노드 Cell - block 등록 

    public Cell topNode;
    
    public Outlinable outlinable;

    public void CellNodeSetting(int max)
    {
        if(cellPoint.y != max - 1)
        {
           bottom_Node =  Board.instance.cellList.Find(x=> x.cellPoint.x == cellPoint.x && x.cellPoint.y == cellPoint.y+1);
        }

        if(cellPoint.y != 0)
        {
            topNode =  Board.instance.cellList.Find(x=> x.cellPoint.x == cellPoint.x && x.cellPoint.y == cellPoint.y-1);
        }
    }

}



//* 칸과 블럭 개념을 따로놓고 봐야한다.

//* 칸은 주위 노드를 알고있어야한다. 

//* 아래노드의 블럭이 비어있다면 

//* 만약 가로 노드일때 바로 위 노드의 block이 자신의 block 이되면서 위치 이동한다 

//* 만역 세로 노드일때 (지워지는 자신부터 위까지 전부 리스트에 넣고 정렬 시킴?)