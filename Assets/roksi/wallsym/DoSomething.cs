using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DoSomething : MonoBehaviour
{
   public static DoSomething Instance;

  
    public bool CanClick;
    public bool Good;

    private int currentStep = 0;
    private readonly List<string> clickList = new List<string>
    {
        "click1" ,"click2","click3","click4","click5"
    };
    private void  Awake()
    {
        Instance = this;
    }
     
    public void ClickedObject(string objectName, Transform spawnPoint)
    {

        if (!CanClick)
        {
            Debug.Log("nie mozna");
            return;
        }
      
        if(currentStep<clickList.Count && objectName == clickList[currentStep])
        {
            
            Debug.Log($"kliknieto{objectName}");


            if(wALLsYMBOLS.instance != null && wALLsYMBOLS.instance.symbol.Length > currentStep)
            {
                GameObject symPrefab = wALLsYMBOLS.instance.symbol[currentStep];
                Instantiate(symPrefab, spawnPoint.position,Quaternion.identity);
            }

            currentStep++;

           

            if (currentStep == clickList.Count)
            {
                Debug.Log("dobrze");
                Good = true;

            }
            
        }
        else
        {
            Debug.Log("zle");
            currentStep = 0;

        }
    }


}
