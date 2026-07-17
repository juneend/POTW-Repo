using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzlePiece : MonoBehaviour
{

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _pickUpClip, _dropClip;
    private bool _dragging;

    Vector2 _offset,_originalPos;

    void Awake()
    {
        _originalPos = transform.position;
    }


    
void Update()
{
    if (!_dragging) return;

    transform.position = GetMousePos() - _offset;
}


    
    void OnMouseDown()
    {
        _dragging = true;
        _source.PlayOneShot(_pickUpClip);

        _offset = GetMousePos() - (Vector2)transform.position;
    }
    void OnMouseUp()
    {
        transform.position = _originalPos;
        _dragging = false;
        _source.PlayOneShot(_dropClip);
    }
    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }




}
