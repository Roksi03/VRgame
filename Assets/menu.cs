using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
  public void ToMenu(string scenename )
    {
        SceneManager.LoadScene(scenename);
    }
}
