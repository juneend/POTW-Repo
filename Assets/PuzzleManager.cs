using UnityEngine;
using UnityEngine.SceneManagement; //add scenemanagerment to teleport between scenes
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<PuzzleSlot> _slotPrefabs;
    [SerializeField] private PuzzlePiece _piecePrefab;
    [SerializeField] private Transform _slotParent, _pieceParent;

    [SerializeField] private GameObject continueButton;
    

    private int piecesPlaced = 0;

    void Start()
    {
        piecesPlaced = 0;
        continueButton.SetActive(false);
        Spawn();
    }

    void Spawn()
    {
        var randomSet = _slotPrefabs.OrderBy(s => Random.value).Take(3).ToList();

        for (int i = 0; i < randomSet.Count; i++)
        {
            var spawnedSlot = Instantiate(
                randomSet[i],
                _slotParent.GetChild(i).position,
                Quaternion.identity);

            var spawnedPiece = Instantiate(
                _piecePrefab,
                _pieceParent.GetChild(i).position,
                Quaternion.identity);

            spawnedPiece.Init(spawnedSlot, this);
        }
    }

    public void PiecePlaced()
    {
        piecesPlaced++;
        Debug.Log($"Pieces placed: {piecesPlaced}");

        if (piecesPlaced >= 3)
        {
            if (continueButton !=null)
            {
                continueButton.SetActive(true);
            }
            else
            {
                Debug.LogError("Continue button is not assigned in the inspector.");
            } 
        }
    }

    //call this function when the continue button is clicked
    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}



