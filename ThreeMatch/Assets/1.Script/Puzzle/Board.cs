using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Board : MonoBehaviour
{

    public static Board instance;
    public ScriptableSprites assets_Sprite;
    public List<Cell> cellList = new List<Cell>();


    public static Cell[,] cellBoard;
    public Block[,] blockBoard;


    public TouchSlide touchSlide;

    public static int x = 9;
    public static int y = 9;


    [NonSerialized] public GameObject BlockParent;
    [NonSerialized] public GameObject CellParent;

   
    public bool blockMoving = false;

    public AnimationCurve curve;

    private void Awake() {
        instance =this;

        touchSlide = FindObjectOfType<TouchSlide>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Genrator();
    }

    private GameObject CreateParent(string name , GameObject Panel)
    {
        Panel.name = name;
        Panel.transform.SetParent(transform);
        return Panel;
    }

    void Genrator()
    {
        cellBoard = new Cell[x,y];
        blockBoard = new Block[x,y];

       
        CellParent  = CreateParent("CellPanel"  , new GameObject());
        BlockParent = CreateParent("BlockParent" , new GameObject());
        for(int j = 0  ; j < y ; j++)
        for(int i = -x/2 ; i <=x/2 ; i++)
        {
            Cell cell   = Instantiate(assets_Sprite.cell , CellParent.transform);
            Block block = Instantiate(assets_Sprite.block);

            cellBoard[i+x/2 ,j] = cell;


            cell.name = $"{i+x/2}{j} cell";
            cell.cellPoint = new Point(i+x/2 ,j);
            cell.position = new Vector2(i,transform.position.y -j);
            cell.transform.localPosition = cell.position;
            cell.block = block;

            CookingKind kind = (CookingKind)UnityEngine.Random.Range(0,(int)CookingKind.MAX);
            Sprite sprite = assets_Sprite.tileSprites[(int)kind];
            block.Set(this,sprite ,kind);
            block.transform.SetParent(cell.transform);
            block.transform.localPosition = new Vector3(0 , 0 , 0);
            block.name = $"{i+x/2},{j} block";

            cellList.Add(cell);
            blockBoard[i+x/2,j] = block;
        }

        cellList.ForEach(cell=>cell.CellNodeSetting(x));

        touchSlide.MatchLogic().Forget();

    }


    public Block CreateBlock()
    {
        var blc = Instantiate(assets_Sprite.block);
        CookingKind kind = (CookingKind)UnityEngine.Random.Range(0, (int)CookingKind.MAX);
        Sprite sprite = assets_Sprite.tileSprites[(int)kind];
        blc.Set(this ,sprite,kind);
        return blc;
    }


    
}