using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PuzzleSlot : MonoBehaviour
{
    public SpriteRenderer _renderer;

    [SerializeField] private AudioSource _source;


    public void Placed()
    {
        _source.Play();
    }
}


