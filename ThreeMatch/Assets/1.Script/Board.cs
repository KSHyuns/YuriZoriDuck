using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Board : MonoBehaviour
{
    public ScriptableSprites assets_Sprite;
    public List<Tile> tiles = new List<Tile>();

    [SerializeField] int x = 5;
    [SerializeField] int y = 5;
    // Start is called before the first frame update
    void Start()
    {
        Genrator();
    }


    void Genrator()
    {
        for(int j = 0  ; j < y ; j++)
        for(int i = -x/2 ; i <=x/2 ; i++)
        {
            Tile tile = Instantiate(assets_Sprite.tile , transform) as Tile;
            
            tile.kind = (TileKind)Random.Range(0,7);
            tile.spriteRenderer.sprite = assets_Sprite.tileSprites[(int)tile.kind];
            
            tile.point = new Point(i + x/2 , j);
            tile.transform.position = new Vector3(i , -j , 0);

            tiles.Add(tile);
        }
    }
    
}