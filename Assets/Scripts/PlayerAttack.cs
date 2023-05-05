using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    private bool isAttacking;
    private bool isAttackingPressed;

    //Attcking stats
    public float Damage = 10;
    public float CoolDownAttack = 0.3f;

    //Animations States
    const string PLAYER_ATTACK = "Player_Attack";
    const string PLAYER_IDLE = "Player_Idle";


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttacking();
    }

    void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }

    void PlayerAttacking()
    {
        if (Input.GetMouseButtonDown(0) && !isAttackingPressed)
        {
            StartCoroutine(swing());
        }
    }

    IEnumerator swing()
    {
        ChangeAnimationState(PLAYER_ATTACK);
        isAttackingPressed = true;

        yield return new WaitForSeconds(1f);

        ChangeAnimationState(PLAYER_IDLE);
        isAttackingPressed = false;
    }
}
