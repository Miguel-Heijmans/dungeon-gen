using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            myDoor.Play("DoorOpen", 0, 0.0f);
            // gameObject.SetActive(false);

            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        myDoor.Play("DoorClose", 0, 0.0f);
        // gameObject.SetActive(false);
    }
    
}
