using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Culling : MonoBehaviour
{
    public List<GameObject> _gameObjects = new List<GameObject>();
    

    
    

    //checks the distance of a room to the player
    private float CheckDistance(GameObject other){
        var distance = Vector3.Distance(other.gameObject.transform.position, this.gameObject.transform.position);
        return distance;
    }
    
    //enables the rooms once having entered teh vicinity of the player
    public void HandleEnabling()
    {
        if(_gameObjects.Count <= 0) return;
        foreach (var obj in from obj in _gameObjects let distance = CheckDistance(obj) where !(distance > 15) select obj)
        {
            obj.SetActive(true);
        }
        
    }

    //disables the rooms after having exited the vicinity of the player
    private void OnTriggerExit(Collider sphereCollider)
    {
        if (sphereCollider.TryGetComponent<RoomBehaviour>(out var comp))
        {
            comp.gameObject.SetActive(false);
        }
    }
    
/*
    private GameObject[] GetFilteredGameObjects(float distanceThreshold)
    {
        List<GameObject> filteredObjects = new List<GameObject>();

        foreach (var obj in from obj in _gameObjects
                 let distance = CheckDistance(obj)
                 where !(distance > distanceThreshold)
                 select obj)
        {
            filteredObjects.Add(obj);
        }

        return filteredObjects.ToArray();
    }*/

    //finds all the rooms and adds them to a list so that they can be found after having been disabled
    public void Start()
    {
        
        var list = FindObjectsOfType<RoomBehaviour>();
        print(list.Length);
        foreach (var roomBehaviour in list)
        {
            GameObject roomBehaviourObject;
            (roomBehaviourObject = roomBehaviour.gameObject).SetActive(false);
            _gameObjects.Add(roomBehaviourObject);
        }
        
    }
}
