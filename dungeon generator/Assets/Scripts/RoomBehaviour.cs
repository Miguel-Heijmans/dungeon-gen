using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls; // Up 1 - Down 2 - Right 3 - Left 4
    public GameObject[] doors;
   
   public void UpdateRoom(bool[] status) // sets which doors are active.
   {
       for (var i = 0; i < status.Length; i++)
       {
           Destroy(status[i] ? walls[i] : doors[i]);
       }
   }
}
