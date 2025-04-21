using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, GameObject attacker)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(attacker);
        }
    }

    private void Die(GameObject killer)
    {
        // Verificar si el asesino es el jugador
        if (killer != null && killer.layer == LayerMask.NameToLayer("Player"))
        {
            // Notificar al GameManager sobre el kill
            GameManager.Instance.AddKill(gameObject);
        }

        // Destruir el enemigo
        Destroy(gameObject);
    }
}
