using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Material vignetterMaterial;
    private int health;

    private void Start()
    {
        health = GameController.instance.maxPlayerHealth;

        vignetterMaterial.SetFloat("_Alpha", 0);
    }

    public void ChangeHealth(int amount)
    {
        health += amount;

        if (health <= 0)
        {
            GetComponent<StunController>().HeavyStun();
        }
        else
        {
            GetComponent<StunController>().LightStun();
        }
    }
}
