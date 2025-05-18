using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class wALLsYMBOLS : MonoBehaviour
{
   public static wALLsYMBOLS instance;
    public GameObject[]symbol;

    public Transform[] point;

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
       
    }

  
    public void symbolsSpawn()
    {
        if (symbol != null)
        {
            StartCoroutine(spawn());
            
            DoSomething.Instance.CanClick = false;

        }
    }

    private IEnumerator spawn()
    {
        int c = Mathf.Min(symbol.Length, point.Length);

        for(int i = 0; i < c; i++)
        {
            GameObject newSymbol = Instantiate(symbol[i], point[i].position, Quaternion.identity);
            yield return new WaitForSeconds(1);
            Destroy(newSymbol);
           
          
        }



        DoSomething.Instance.CanClick = true;


    }
}
