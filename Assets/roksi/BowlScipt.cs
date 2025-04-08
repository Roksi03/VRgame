using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BowlScipt : MonoBehaviour
{

   [SerializeField] private int Ball = 0;

    public GameObject ItemPrefab;

    public Transform itemPoint;

    private List<GameObject> balls = new List<GameObject>();

    private void Start()
    {
        
       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("kulka");
            Ball += 1;
            balls.Add(other.gameObject);
        }
        if(Ball == 2)
        {
            foreach(GameObject ball in balls)
            {
                Destroy(ball);
            }
           balls.Clear();
            Ball = 0;
            GameObject newItem = Instantiate(ItemPrefab, itemPoint.position, itemPoint.rotation);
        }
    }
}
