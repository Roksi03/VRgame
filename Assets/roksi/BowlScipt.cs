using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BowlScipt : MonoBehaviour
{



    public GameObject ItemPrefab;

    public Transform itemPoint;

    [SerializeField] private List<GameObject> balls = new List<GameObject>();

    private void Start()
    {
        
       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball" )&& !balls.Contains(other.gameObject))
        {
            Debug.Log("kulka");
           
            balls.Add(other.gameObject);
        }
        if(balls.Count == 2)
        {
            foreach(GameObject ball in balls)
            {
                Destroy(ball);
            }
           balls.Clear();
          
            GameObject newItem = Instantiate(ItemPrefab, itemPoint.position, itemPoint.rotation);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ball")&& balls.Contains(other.gameObject))
        {
            balls.Remove(other.gameObject);
        }
    }
}
