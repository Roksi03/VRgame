using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BowlScipt : MonoBehaviour
{



    public GameObject IGPrefab;
    public GameObject ICPrefab;
    public GameObject GCPrefab;

    public Transform itemPoint;

    [SerializeField] private List<string> balls = new List<string>();
    [SerializeField] private List<GameObject> ballObjects = new List<GameObject>();

  


    void SpawnSphere(string name1 ,string name2)
    {
      HashSet<string> combo = new HashSet<string> { name1, name2 };

        if(combo.SetEquals(new HashSet<string> { "gold", "iron" }))
        {
            Instantiate(IGPrefab,itemPoint.position,Quaternion.identity);
        }
        else if(combo.SetEquals(new HashSet<string> { "gold", "copper" }))
        {
            Instantiate(GCPrefab,itemPoint.position,Quaternion.identity);
        }
        else if(combo.SetEquals(new HashSet<string> { "iron", "copper" }))
        {
            Instantiate(ICPrefab,itemPoint.position,Quaternion.identity);
        }
    }






    void OnTriggerEnter(Collider other)
    {

        ItemNamesforBowl n =other.GetComponent<ItemNamesforBowl>();

        if(n != null && !balls.Contains(n.Namee))
        {
            balls.Add(n.Namee);
            ballObjects.Add(other.gameObject);

            if(balls.Count == 2)
            {
                SpawnSphere(balls[0], balls[1]);
                
                Destroy(ballObjects[0]);
                Destroy(ballObjects[1]);

                balls.Clear();
                ballObjects.Clear();
            }

            
        }

       








    }
}
