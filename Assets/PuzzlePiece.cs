using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _pickUpClip, _dropClip;
    private bool _dragging,_placed;

    Vector2 _offset,_originalPos;

    private PuzzleSlot _slot;

    public void Init(PuzzleSlot slot)
    {
        _renderer.sprite = slot._renderer.sprite;
        _slot = slot;
    }

    void Awake()
    {
        _originalPos = transform.position;
    }


    
void Update()
{
    if (_placed) return;
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

        if(Vector2.Distance(transform.position, _slot.transform.position) < 3)
        {
            transform.position = _slot.transform.position;
            _slot.Placed();
            _placed = true;
        }
        else
        {
            transform.position = _originalPos;
            _source.PlayOneShot(_dropClip);
            _dragging = false;
        }
    }
    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }




}
