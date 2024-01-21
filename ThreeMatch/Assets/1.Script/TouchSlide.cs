using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
public class TouchSlide : MonoBehaviour
{

    [SerializeField] private Tile selectTile;

    private bool TouchOn;

    private Vector2 origin;
    private Vector2 direction; 
    
    private bool dirCheker;

    public void OnEnable()
    {
#if UNITY_EDITOR
        Debug.Log("초기화 ON");
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
        Debug.Log("초기화 OFF");
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
        TouchOn = true;
        origin = Camera.main.ScreenToWorldPoint(pos);

        var raycast = Physics2D.Raycast(origin, Vector2.zero , Mathf.Infinity , 1 << LayerMask.NameToLayer("Tile"));

        if(raycast)
        {
            if(raycast.collider.TryGetComponent(out Tile tile))
            {
                selectTile = tile;
            }
        }
    }
    string dirName = "";
    private void Move(Vector2 pos)
    {
        if(selectTile && TouchOn && !dirCheker)
        {
            direction = Camera.main.ScreenToWorldPoint(pos);

            //? 방향 구하기  위오왼아 
            var dir = direction - origin;
            float sildeDistance = dir.magnitude;

            var angle = Mathf.Atan2(dir.x , dir.y) * Mathf.Rad2Deg;
            if(sildeDistance > 0.6f)
            {
                //* 한번만 실행 되게
                if(angle < 45.0f && angle > -45.0f) 
                { 
                    dirName = "위";
                    dirCheker = true;
                }

                else if(angle >= 45f && angle < 135f)      
                {
                     dirName = "오른쪽";
                     dirCheker = true;
                }
                else if(angle <= -45f && angle > -135f) 
                {
                    dirName = "왼쪽";
                    dirCheker = true;
                }
                else                 
                {
                    dirName = "아래";
                    dirCheker = true;
                }
            }

            switch(dirName)
            {
                case "위":
                    Debug.Log("위 Tile과 스위칭");
                break;
                case "아래":
                    Debug.Log("아래 Tile과 스위칭");
                break;
                case "왼쪽":
                    Debug.Log("왼쪽 Tile과 스위칭");
                break;
                case "오른쪽":
                    Debug.Log("오른쪽 Tile과 스위칭");
                break;
            }

           




        }
    }

    private void Up()
    {
        dirName = "";
        TouchOn = false;
        dirCheker = false;
        selectTile = null;
    }

  

}
