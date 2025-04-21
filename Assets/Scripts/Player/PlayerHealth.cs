using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    

    public float Health = 100f;



    public void Update()
    {
        Health = Mathf.Clamp(Health, 0 , 100);
    }


    public void takeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    

    void Die()
    {
       
        Destroy(gameObject);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 400, 200, 20), "Your health: " + Health);
        
    }
}
