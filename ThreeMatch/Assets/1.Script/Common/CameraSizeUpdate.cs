using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeUpdate : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float size;

    // Start is called before the first frame update
    void Start()
    {
        _camera.orthographicSize = size / _camera.aspect;
    }
}
