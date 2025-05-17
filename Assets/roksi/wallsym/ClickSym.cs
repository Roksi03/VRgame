using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ClickSym : MonoBehaviour
{

    private XRSimpleInteractable inter;
    public string SymName;

    private void Awake()
    {
        var inter = GetComponent<XRSimpleInteractable>();
        if (inter != null)
        {
            inter.selectEntered.AddListener(ClickSymbols);
        }
    }
    private void OnDestroy()
    {
        if (inter != null)
        {
            inter.selectEntered.RemoveListener(ClickSymbols);
        }
    }

    void ClickSymbols(SelectEnterEventArgs ar)
    {
        Debug.Log("klikniete");
        DoSomething.Instance.ClickedObject(SymName,transform);

    }

   

}
