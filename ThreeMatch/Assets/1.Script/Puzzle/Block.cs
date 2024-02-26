using System.Collections;
using System.Collections.Generic;
using EPOOutline;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public CookingKind kind;

    private Board board;

    public bool match = false;

    public Outlinable outlinable;
    public void Set(Board _board, Sprite _sprite , CookingKind _kind)
    {
        if(!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
        this.kind = _kind;
        spriteRenderer.sprite = _sprite;
        board = _board;
    }

    public Board GetBoard => board;

    public void Down()
    {
        board.touchSlide.TouchOn = true;
        board.touchSlide.selectTile = this;
        Debug.Log("Down");
    }


   

}
