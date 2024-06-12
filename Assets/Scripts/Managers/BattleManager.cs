using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public static BattleManager instance;
    [SerializeField] private GameObject battleUI;
    
    public Player player;
    public Enemy enemy;
    private bool isPlayerTurn = true;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void InitiateBattleSequence()
    {
        battleUI.SetActive(true);
        StartPlayerTurn();
    }
    
    public void StartPlayerTurn()
    {
        isPlayerTurn = true;
    }
    
    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        StartEnemyTurn();
    }
    
    public void StartEnemyTurn()
    {
        isPlayerTurn = false;
        EnemyAttack();
    }

    public void EndEnemyTurn()
    {
        isPlayerTurn = true;
        StartPlayerTurn();
    }

    public void PlayerAttack()
    {
        enemy.health -= player.AttackPower;
        Debug.Log("Enemy health: " + enemy.health);
        if (enemy.health <= 0)  
        {
            EndBattle();
        }
        else
        {
            Debug.Log("End player turn");
            EndPlayerTurn();
        }
    }

    public void EnemyAttack()
    {
        player.health -= enemy.attackPower;
        Debug.Log("Player health: " + player.health);
        if (player.health <= 0)
        {
            EndBattle();
        }
        else
        {
            Debug.Log("End enemy turn");
            EndEnemyTurn();
        }
    }
    
    public void EndBattle()
    {
        Debug.Log("End battle");
        Destroy(enemy.gameObject);
        player.canEncounter = true;
        battleUI.SetActive(false);
        player.GetComponent<PlayerMovement>().SetCanMove(true);
    }
}
