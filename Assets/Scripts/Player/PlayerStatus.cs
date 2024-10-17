using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    private PlayerUI playerUI;
    [SerializeField]
    private bool isDied;

    public float maxHP;
    public float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        playerUI = GetComponent<PlayerUI>();

        playerUI.OnPlayerHealed(currentHP, maxHP);
    }

    
    public void OnTakeDamage(float damageTaken)
    {
        if (isDied) return;

        currentHP -= damageTaken;

        if(currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("game over");
            GameOver();
        }

        playerUI.OnPlayerDamaged(currentHP, maxHP);

    }

    public void OnHeal(float healAmount)
    {
        currentHP += healAmount;

        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
            Debug.Log("fully healed");
        }

        playerUI.OnPlayerHealed(currentHP, maxHP);
    }

    public void GameOver()
    {
        isDied = true;
    }
}
