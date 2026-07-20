using UnityEngine;

public class PlayerStealth : MonoBehaviour
{

    public bool isHidden = false;

    public void SetHidden(bool hiddenState) {
        if (isHidden)
        {
            Debug.Log("Player is now hdden!");
        }
        else {
            Debug.Log("Player is now visible!");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
