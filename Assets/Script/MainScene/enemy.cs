using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    enum EnemyState
    {
        Chase,
        Attack,
        Damaged,
        Die
    }

    EnemyState enemyState;

    /*
    private RaycastHit2D rayUp;
    private RaycastHit2D rayDown;
    private RaycastHit2D rayLeft;
    private RaycastHit2D rayRight;
    */

    private int colliderLayerMask;

    private int hp = 100;


    private int attackPower = 5;
    public float attackRange;
    private float currentTime = 0;
    private float attackDelay = 2f;

    public Transform level1Monster;
    private Vector3 currentDirection;
    public int movingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Chase;

        colliderLayerMask = (1 << LayerMask.NameToLayer("Level1Monster"));
        colliderLayerMask = ~colliderLayerMask;
        currentDirection = Vector3.down;
    }

    private void FixedUpdate()
    {
        switch (enemyState)
        {
            case EnemyState.Chase:
                chase();
                break;
            case EnemyState.Attack:
                attack();
                break;
            case EnemyState.Damaged:
                //damaged();
                break;
            case EnemyState.Die:
                //die();
                break;
        }
    }

    private void chase()
    {
        currentDirection = (level1Monster.transform.position - this.transform.position).normalized;

        this.transform.position += currentDirection * movingSpeed * Time.deltaTime;
    }

    private void attack()
    {

        // When the object attacking, movingSpeed must be 0.
        //movingSpeed = 0;

        //Debug.Log(Vector3.Distance(this.transform.position, level1Monster.transform.position));
        if (Vector3.Distance(this.transform.position, level1Monster.transform.position) <= attackRange + 0.1f)
        {
            Debug.Log("I got Enemy in sight");
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                print("Ataque - enemy");
                level1Monster.GetComponent<level1Monster>().damageAction(attackPower);
                currentTime = 0;
            }
        }

        // Actually, This Part Is Not Necessary
        /*else
        {
            enemyState = EnemyState.Move;
            Debug.Log("Changing State - Move");
        }
        */
    }
    
    private void damaged()
    {
        StartCoroutine(damageProcess());
    }

    IEnumerator damageProcess()
    {
        yield return new WaitForSeconds(0.5f);

        enemyState = EnemyState.Attack;
        Debug.Log("Changing State - Attack");
    }

    public void damageAction(int damage)
    {
        hp -= damage; 

        if (enemyState == EnemyState.Damaged || enemyState == EnemyState.Die)
        {
            return;
        }

        hp -= damage;

        if (hp > 0)
        {
            enemyState = EnemyState.Damaged;
            Debug.Log("Changing State - Damaged");
            damaged();
        }

        else
        {
            enemyState = EnemyState.Die;
            Debug.Log("Changing State - Die");
            die();
        }
    }

    private void die()
    {
        StopAllCoroutines();
        StartCoroutine(dieProcess());
    }

    IEnumerator dieProcess()
    {
        this.enabled = false;

        yield return new WaitForSeconds(2f);
        Debug.Log("Level1Monster Dead");
        Destroy(this.gameObject);
    }
}
