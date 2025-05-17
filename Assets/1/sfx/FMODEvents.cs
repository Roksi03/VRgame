using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("torch- light source")]
    [field: SerializeField] public EventReference lightSource { get; private set; }

    [field: Header("torch ignite")]
    [field: SerializeField] public EventReference torchIgnite { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference music {  get; private set; }
   public static FMODEvents instance {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("found more than one FMOD Events script in the scene");
        }
        instance = this;
    }
}
