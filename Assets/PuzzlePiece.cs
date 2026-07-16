using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzlePiece : MonoBehaviour
{

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _pickUpClip, _dropClip;
    private bool _dragging;
    void OnMouseDown()
    {
        _dragging = true;
        _source.PlayOneShot(_pickUpClip);
    }

    void OnMouseUp()
    {
        _dragging = false;
        _source.PlayOneShot(_dropClip);
    }




}
