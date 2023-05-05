using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    private bool isAttacking;
    private bool isAttackingPressed;

    public Camera WeaponAttack;

    //Attcking stats
    public int Damage = 10;
    public float CoolDownAttack = 0.3f;
    public float range = 3f;

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
            StartCoroutine(attacking());
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

        IEnumerator attacking()
        {
            RaycastHit hit;
            if (Physics.Raycast(WeaponAttack.transform.position, WeaponAttack.transform.forward, out hit, range))
            {
               Debug.Log(hit.transform);
               
               AI Aiscript = hit.transform.GetComponent<AI>();
               if (Aiscript != null)
               {
                Aiscript.TakeDamage(Damage);
               }
               

            }

            yield return new WaitForSeconds(1f);
        }
    
}
