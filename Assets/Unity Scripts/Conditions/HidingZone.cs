using UnityEngine;

public class HidingZone : MonoBehaviour
{

    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag)) { 
            PlayerStealth player = other.GetComponent<PlayerStealth>();
            if (player != null) {
                player.SetHidden(true);
            }
            }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(playerTag)) {
            PlayerStealth player = other.GetComponent<PlayerStealth>();
            if (player != null) {
                player.SetHidden(false);
            }

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
