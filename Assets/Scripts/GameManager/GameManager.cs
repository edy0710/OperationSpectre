using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerKills = 0;
    public int killsToWin = 2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKill(GameObject killedEnemy)
    {
        // Verificar que el objeto muerto tenga el layer Enemy
        if (killedEnemy.layer == LayerMask.NameToLayer("Enemy"))
        {
            playerKills++;
            Debug.Log("Enemigos eliminados: " + playerKills + "/" + killsToWin);

            CheckWinCondition();
        }
    }

    private void CheckWinCondition()
    {
        if (playerKills >= killsToWin)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("¡Has ganado! Eliminaste " + killsToWin + " enemigos.");

        // Cargar la escena 1 de forma asíncrona
        SceneManager.LoadSceneAsync(1);

        // Reiniciar el contador para la próxima partida
        playerKills = 0;
    }
}
