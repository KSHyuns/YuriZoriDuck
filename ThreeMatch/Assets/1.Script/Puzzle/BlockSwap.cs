using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
public class BlockSwap
{
    public static Block SWAP(Block selectTile,Vector3 dir)
    {
        return  Board.instance.cellList.Find(x=>x.transform.position == selectTile.transform.position + dir).block;
    }


    public static void swap(Block scTile ,Block changeTile , List<Cell> cellList)
    {
        int sc = scTile.transform.parent.GetSiblingIndex();
        int ch = changeTile.transform.parent.GetSiblingIndex();

        Block block = cellList[sc].block;
        cellList[sc].block = cellList[ch].block;
        cellList[ch].block = block;


        var scParent = scTile.transform.parent;
        var chParent = changeTile.transform.parent;
        if(scTile != null && changeTile != null)
        {
            scTile.transform.parent = null;
            changeTile.transform.parent = null;

            scTile.transform.parent = chParent;
            changeTile.transform.parent = scParent;
        }

    }

}


public class BlockMove
{
    public static  async UniTask Move(Block block , Cell Goal , float time)
    {
        float timer = 0f;
        Vector3 GoalPosition = Goal.transform.position;

        while(true)
        {
            if(timer >= 1f) break;

            timer += Time.deltaTime ;

            block.transform.position = Vector3.MoveTowards(block.transform.position , GoalPosition , timer * time);
        
            await UniTask.Delay(TimeSpan.FromSeconds(0.02f) , DelayType.UnscaledDeltaTime);
        }

    } 
}