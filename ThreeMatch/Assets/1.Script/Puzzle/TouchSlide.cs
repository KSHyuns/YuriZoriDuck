using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Cysharp.Threading.Tasks;
using System;

public class TouchSlide : MonoBehaviour
{

    [SerializeField] public Block selectTile;
    [SerializeField] private Block changeTile;

    [SerializeField] List<Cell> MatchList = new List<Cell>();

    List<Cell> rowList = new List<Cell>();    //* 媛濡?
    [SerializeField] List<Cell> colList = new List<Cell>();    //* ?몃?


    public bool TouchOn;

    private Vector2 origin;
    private Vector2 direction; 
    
    private bool dirCheker;

    public void OnEnable()
    {
#if UNITY_EDITOR
        Debug.Log("에디터 온");
#else
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += OnDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += OnMove;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += OnUp;
#endif
    }

    public void OnDisable()
    {
#if UNITY_EDITOR
        Debug.Log("에디터 오프");
#else
        EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= OnDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= OnMove;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= OnUp;
#endif
    }

    private void OnDown(Finger finger)
    {
        Down(finger.currentTouch.screenPosition);
    }

    private void OnMove(Finger finger)
    {
        Move(finger.currentTouch.screenPosition);
    }

    private void OnUp(Finger finger)
    {
        Up();
    }

    private void Update()
    {
        #if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
           Down(Input.mousePosition);
        }

        else if(Input.GetMouseButton(0))
        {
            Move(Input.mousePosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Up();
        }
        #endif
    }


    private void Down(Vector2 pos)
    {
        // TouchOn = true;
        // origin = Camera.main.ScreenToWorldPoint(pos);

        // var raycast = Physics2D.Raycast(origin, Vector2.zero , Mathf.Infinity , 1 << LayerMask.NameToLayer("Tile"));

        // if(raycast)
        // {
        //     if(raycast.collider.TryGetComponent(out Tile tile))
        //     {
        //         selectTile = tile;
        //     }
        // }
    }
    private void Move(Vector2 pos)
    {
        if(selectTile && TouchOn && !dirCheker)
        {
            direction = Camera.main.ScreenToWorldPoint(pos);

            //? 諛⑺? 援ы?湲? ????쇱? 
            var dir = direction - (Vector2)selectTile.transform.position;
            float sildeDistance = dir.magnitude;

            var angle = Mathf.Atan2(dir.x , dir.y) * Mathf.Rad2Deg;
           
            //* ???留??ㅽ? ???
            if(sildeDistance > 0.5f)
            {   
                //* ??
                if(angle < 45.0f && angle > -45.0f) 
                { 
                    changeTile = BlockSwap.SWAP(selectTile,Vector2.up);
                    if(changeTile == null) return;

                    dirCheker = true;
                }
                //* ?ㅻⅨ履?
                else if(angle >= 45f && angle < 135f)      
                {
                     changeTile = BlockSwap.SWAP(selectTile,Vector2.right);
                     if(changeTile == null) return;

                     dirCheker = true;
                }
                //* ?쇱そ
                else if(angle <= -45f && angle > -135f) 
                {
                    changeTile = BlockSwap.SWAP(selectTile,Vector2.left);
                    if(changeTile == null) return;

                    dirCheker = true;
                }
                //* ???
                else                 
                {
                    changeTile = BlockSwap.SWAP(selectTile,Vector2.down);
                    if(changeTile == null) return;

                    dirCheker = true;
                }
                
            }
        }
    }

    private void Up()
    {
        TouchOn = false;
        dirCheker = false;

        if(Board.instance.blockMoving == true) 
        {
            selectTile = null;
            changeTile = null;
            return;
        }
        if(selectTile && changeTile)
        {   
            SlideOn(selectTile , changeTile);
        }
        selectTile = null;
        changeTile = null;
    }

        
    private void SlideOn(Block scTile ,Block changeTile)
    {
        Vector2 myTilePosition = scTile.transform.position;
        Vector2 changeTilePosition = changeTile.transform.position;


       
        DG.Tweening.Sequence seq = DOTween.Sequence();
        seq.OnStart(()=>{ })
        .AppendCallback(()=>{ BlockSwap.swap(scTile, changeTile , Board.instance.cellList); })
        .Insert(0f, scTile.transform.DOMove(changeTilePosition , 0.2f))
        .Insert(0f, changeTile.transform.DOMove(myTilePosition , 0.2f))
        .OnComplete(()=>{MatchLogic3().Forget();});
        
    }
    

    private void Add(Cell cell , List<Cell> cellList , List<Cell> RowNColList)
    {
        {
            if(!cellList.Contains(cell))   cellList.Add(cell);
            if(!RowNColList.Contains(cell)) RowNColList.Add(cell);
        }
    }


    private void EvalBlock(int x , int y , List<Cell> cellList)
    {
        try
        { 
        
            for(int i = 0; i < y ; i++)
            {
                var Row = Board.instance.cellList.FindAll(x=>x.cellPoint.y == i);

                for(int r = 0 ; r < Row.Count -2 ; r++)
                {
                    if(Row[r].block.kind   == Row[r+1].block.kind &&
                       Row[r+1].block.kind == Row[r+2].block.kind && 
                       Row[r].block.kind   == Row[r+2].block.kind)
                       {
                            Add(Row[r]   , cellList , rowList);
                            Add(Row[r+1] , cellList , rowList);
                            Add(Row[r+2] , cellList , rowList);
                       }
                }
            } 

       
            for(int i=0;i<x;i++)
            {
                var Col = Board.instance.cellList.FindAll(x=>x.cellPoint.x == i);

                for(int c =0 ; c < Col.Count - 2 ; c++)
                {
                     if(Col[c].block.kind   == Col[c+1].block.kind &&
                        Col[c+1].block.kind == Col[c+2].block.kind && 
                        Col[c].block.kind   == Col[c+2].block.kind)
                       {
                            Add(Col[c]   , cellList , colList);
                            Add(Col[c+1] , cellList , colList);
                            Add(Col[c+2] , cellList , colList);
                       }
                }
            }
        }
        catch (Exception e) { Debug.Log(e.Message); }
    }


   
    public async UniTask MatchLogic()
    {
        await UniTask.Yield();

        EvalBlock(Board.x,Board.y,MatchList);

        if(MatchList.Count <= 0) return;
        
       // MatchList.ForEach(Cell=>
      //  {
           // Cell.block.transform.localScale = Vector2.one * 0.1f;
      //  });

        MatchList.ForEach(Cell=>
        {
            CookingKind kind = (CookingKind)UnityEngine.Random.Range(0, (int)CookingKind.MAX);
            Sprite sprite = Cell.block.GetBoard.assets_Sprite.tileSprites[(int)kind];
            Cell.block.Set(Cell.block.GetBoard,sprite ,kind);
          //  Cell.block.transform.DOScale(1 ,0.3f);
        });
        //await UniTask.Delay(System.TimeSpan.FromSeconds(0.1f));
       
        MatchList.Clear();
        rowList.Clear();
        colList.Clear();

        //await UniTask.Delay(System.TimeSpan.FromSeconds(0.1f));

        MatchLogic().Forget();
    }

    
    public async UniTask MatchLogic3()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.02f));

        //寃??媛濡??몃?   寃??寃곌낵 matchList
        EvalBlock(Board.x,Board.y,MatchList);

        if(MatchList.Count <= 0) return;

        //留ㅼ묶??釉?? ??굅 null
        MatchList.ForEach(async cell=> 
        {
            await GameManager.Instance.gameData.storeRoom.ItemAdd(new AddItem() 
            {
                itemSprite = cell.block.spriteRenderer.sprite ,
                kind = cell.block.kind
            });

            Destroy(cell.block.gameObject); 
            cell.block = null;
        });

        MatchList.Clear();
        colList.Clear();
        Board.instance.blockMoving = true;
        await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
        
        float curveSpeed = 0;

        for(int i=0;i< Board.x ; i++)
        {
            //?몃? ???紐⑤? 李얜???0, 1, 2, 3, 4
            var colarray = Board.instance.cellList.FindAll(x=>x.cellPoint.x == i);

            
            //?대? ???block??null??cell 移댁???
            int nullCnt = 0;
            colarray.ForEach(
            cell=>
            {
                if(cell.block == null) ++nullCnt; 
               
            });

            List<Block> blockSort =new List<Block>();          

            //移댁???媛??媛 0?대?硫?留ㅼ????????? ????댁? ?ㅼ? 以?? ???媛??.
            if(nullCnt == 0) continue;

            //吏??? block 媛??
            for(int j = 0 ; j < nullCnt ; j++)
            {
               for(int k = colarray.Count - 1 ; k >= 0 ; k--)
               {    
                    if(j == 0) curveSpeed = 0.5f;
                  
                    if(colarray[0].block == null)
                    {
                        Block block = Board.instance.CreateBlock();
                        block.transform.SetParent(colarray[0].transform);
                        block.transform.position =  colarray[0].transform.position + Vector3.up;
                        
                        colarray[0].block = block;
                        await UniTask.Delay(TimeSpan.FromSeconds(0.01f));

                       // BlockMove.Move(block , colarray[0] , 1 + curveSpeed).Forget();

                        block.transform.DOMove(colarray[0].transform.position , curveSpeed * 0.6f);
                    }
               
                    if(k == 0) break;
                    if(colarray[k].block != null && colarray[k-1].block != null) continue;
                    if(colarray[k].block != null && colarray[k-1].block == null) continue; 
                    if(colarray[k].block == null && colarray[k-1].block == null) continue;

                    
                    if(colarray[k].block == colarray[k-1].block) return;
                    

                    if(nullCnt > 1) curveSpeed +=0.002f;
                    else curveSpeed = 0.2f;



                    colarray[k].block = colarray[k-1].block;

                    colarray[k].block.transform.SetParent(colarray[k].transform);
                   // await UniTask.Yield();
                                                                        //0 . 6    0 . 4    0 . 2    1???????
                                                                        //0 . 4    0 . 6    0 . 8   1.0    1.2    1???????        
                    colarray[k].block.transform.DOMove(colarray[k].transform.position,   curveSpeed * 0.6f );
                    colarray[k-1].block = null;

                   // BlockMove.Move(colarray[k].block , colarray[k] , 1 + curveSpeed).Forget();

                   // await UniTask.Yield();
                    
               }

            }

            colarray.Clear();
        }

        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        Board.instance.blockMoving = false;
      //  colList.Clear();
      //  ;
        MatchLogic3().Forget();
    }


   


    
}
