using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public static ItemManager Instance;

    public PointToAttach[] slots;
    public bool completed = false;

    [SerializeField] private GameObject keytoSpawn;
    [SerializeField] private Transform pointtoSpawn;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckItem()
    {
        if(completed) return;

        int corect = 0;
        foreach (var item in slots)
        {
            if (!item.Correct())
            {
                return;
               
            }
            corect++;
           
        }
        if(corect == slots.Length)
        {
            completed = true;
            Debug.Log("itemy git");
            Instantiate(keytoSpawn,pointtoSpawn.position, Quaternion.identity);

        }
    }
    
}
