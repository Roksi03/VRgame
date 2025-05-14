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
     
    public void ClickedObject(string objectName)
    {
        if (!CanClick)
        {
            Debug.Log("nie mozna");
            return;
        }
      
        if(currentStep<clickList.Count && objectName == clickList[currentStep])
        {
            Debug.Log($"kliknieto{objectName}");
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
