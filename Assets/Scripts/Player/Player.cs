using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    public float health = 10f;
    [SerializeField] private float attackPower;
    [SerializeField] private float currentXp;
    [SerializeField] private float xpToNextLevel;
    [SerializeField] private int maxXp;
    [SerializeField] private int level = 1;
    public bool canEncounter = true;
    
    public float AttackPower
    {
        get { return attackPower; }
    }
    
    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (canEncounter == true)
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                movement.SetCanMove(false);
                BattleManager.instance.player = GetComponent<Player>();
                BattleManager.instance.enemy = enemy;
                BattleManager.instance.InitiateBattleSequence();
                canEncounter = false;
            }
        }
    }
}
