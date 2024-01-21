using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Point point;

    public SpriteRenderer spriteRenderer;

    public TileKind kind;

    public void Set(Sprite _sprite , TileKind _kind)
    {
        if(!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _sprite;
        
        this.kind = _kind;
    }

}
