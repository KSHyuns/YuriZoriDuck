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

    

    private void Awake() {
        instance = this;
        touchSlide = FindObjectOfType<TouchSlide>();
        SoundManager.Instance.Sound_Play("GameBGM", true, Property.BGM);
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
            Block block = CreateBlock();

            cellBoard[i+x/2 ,j] = cell;


            cell.name = $"{i+x/2}{j} cell";
            cell.cellPoint = new Point(i+x/2 ,j);
            cell.position = new Vector2(i,transform.position.y -j);
            cell.transform.localPosition = cell.position;
            cell.block = block;

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

        // 확률 조절
        //100중 7 
        float rnd = UnityEngine.Random.Range(0f, 30f * (int)CookingKind.MAX );
        int n = 0;
        //  0 ~ 15
        // 16 ~ 30
        // 31 ~ 45
        // 45 ~ 60
        // 60 ~ 75
        // 75 ~ 90
        // 91 ~ 100
        if (rnd > 0f && rnd < 30f) n = 0;
        else if (rnd >= 30f && rnd < 60f) n = 1;
        else if(rnd >= 60f && rnd < 90f) n = 2;
        else if(rnd >= 90f && rnd < 120f) n = 3;
        else if (rnd >= 120f && rnd < 150f) n = 4;
        else if (rnd >= 150f && rnd < 180f) n = 5;
        else if (rnd >= 180f && rnd <= 210f) n = 6;


        CookingKind kind = (CookingKind)n; //UnityEngine.Random.Range(0, (int)CookingKind.MAX);
        Sprite sprite = assets_Sprite.tileSprites[(int)kind];
        blc.Set(this ,sprite,kind);
        return blc;
    }


    
}