using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class PuzzleSlot : MonoBehaviour
{
    public SpriteRenderer _renderer;

    [SerializeField] private AudioSource _source;

    public Sprite lettuceSprite;
    public Sprite newspaperSprite;

    public void Start()
    {

        Scene scene = SceneManager.GetActiveScene();



        _renderer = GetComponent<SpriteRenderer>();
        if (scene.name == "Newspaper_pzl")
        {
            _renderer.sprite = newspaperSprite;
        }
        else if (scene.name == "Lettuce_pzl")
        {
            _renderer.sprite = lettuceSprite;
        }
    }

    public void Placed()
    {
        _source.Play();
    }
}


