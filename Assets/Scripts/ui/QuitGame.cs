using UnityEngine;
using UnityEngine.SceneManagement;


public class QuitGame : MonoBehaviour
{
   public void BackGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
