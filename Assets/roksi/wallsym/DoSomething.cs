using UnityEngine;

public class DoSomething : MonoBehaviour
{
   public static DoSomething Instance;

    private string LastClicked = "";
    public bool CanClick;
    public bool Good;

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
        else if(LastClicked =="" && objectName == "click1")
        {
            Debug.Log("kliknieto a");
            LastClicked = "click1";
            
        }
        else if(LastClicked == "click1"&&  objectName == "click2")
        {
            Debug.Log("Dobra kolejnosc");
            LastClicked = "";
            Good = true;
        }
        else
        {
            Debug.Log("èle");
            LastClicked = "";
        }
    }


}
