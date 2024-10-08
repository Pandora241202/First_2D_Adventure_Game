using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        { 
            AllManager.Instance().playerManager.ProcessCollideTrap(other.gameObject.GetInstanceID());
        }
        else if (other.gameObject.CompareTag("Pickup"))
        {
            AllManager.Instance().pickupManager.ProcessPickedByPlayer(other.gameObject.GetInstanceID());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            AllManager.Instance().playerManager.ProcessCollideTrap(other.gameObject.GetInstanceID());
        }
    }

    public void RemovePlayer()
    {
        gameObject.SetActive(false);
    }
}
