using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PointToAttach : MonoBehaviour
{
    public string nameItem;
    public bool isPlaced = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == nameItem)
        {
            isPlaced = true;
            ItemManager.Instance.CheckItem();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == nameItem)
        {
            isPlaced = false;
            ItemManager.Instance.CheckItem();
        }
    }

    public bool Correct()
    {
        return isPlaced;
    }








}
