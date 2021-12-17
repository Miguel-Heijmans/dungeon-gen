using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Culling : MonoBehaviour
{
    private readonly List<GameObject> _gameObjects = new List<GameObject>();

    private void OnTriggerEnter(Collider sphereCollider)
    {
        if (!sphereCollider.TryGetComponent<RoomBehaviour>(out var comp)) return;
        GameObject o;
        (o = comp.gameObject).SetActive(true);
        _gameObjects.Add(o);
    }

    private float CheckDistance(GameObject other){
        var distance = Vector3.Distance(other.gameObject.transform.position, this.gameObject.transform.position);
        return distance;
    }
    
    public void HandleEnabling()
    {
        if(_gameObjects.Count <= 0) return;
        foreach (var obj in from obj in _gameObjects let distance = CheckDistance(obj) where !(distance > 15) select obj)
        {
            obj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider sphereCollider)
    {
        if (sphereCollider.TryGetComponent<RoomBehaviour>(out var comp))
        {
            comp.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        var list = FindObjectsOfType<RoomBehaviour>();

        foreach (var roomBehaviour in list)
        {
            GameObject roomBehaviourObject;
            (roomBehaviourObject = roomBehaviour.gameObject).SetActive(false);
            _gameObjects.Add(roomBehaviourObject);
        }
    }
}
