using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class level1Monster : MonoBehaviour
{
    enum MonsterState
    {
        Move,
        Attack,
        Damaged,
        Die
    }

    MonsterState monsterState;

    private RaycastHit2D rayUp;
    private RaycastHit2D rayDown;
    private RaycastHit2D rayLeft;
    private RaycastHit2D rayRight;
    private int colliderLayerMask;

    public int movingSpeed;

    private Vector3 currentDirection;
    public Transform enemy;

    public float attackRange;
    private float currentTime = 0;
    private float attackDelay = 2f;

    private int attackPower = 1;

    private int hp = 15;

    // Start is called before the first frame update
    void Start()
    {
        monsterState = MonsterState.Move;
        currentDirection = Vector3.up;

        colliderLayerMask = (1 << LayerMask.NameToLayer("Level1Monster")); 
        colliderLayerMask = ~colliderLayerMask;
    }

    private void FixedUpdate()
    {
        updatingRay();
        switch (monsterState)
        {
            case MonsterState.Move:
                move();
                break;
            case MonsterState.Attack:
                attack();
                break;
            case MonsterState.Damaged:
                //damaged();
                break;
            case MonsterState.Die:
                //die();
                break;
        }
    }

    private void updatingRay()
    {
        rayUp = Physics2D.Raycast(this.transform.position + new Vector3(0, 0.6f, 0), Vector2.up, 0.5f, colliderLayerMask);
        rayDown = Physics2D.Raycast(this.transform.position + new Vector3(0, -0.6f, 0), Vector2.down, 0.5f, colliderLayerMask);
        rayLeft = Physics2D.Raycast(this.transform.position + new Vector3(-0.6f, 0, 0), Vector2.left, 0.5f, colliderLayerMask);
        rayRight = Physics2D.Raycast(this.transform.position + new Vector3(0.6f, 0, 0), Vector2.right, 0.5f, colliderLayerMask);

        Debug.DrawRay(this.transform.position + new Vector3(0, 0.6f, 0), new Vector3(0, 0.5f, 0), Color.blue);
        Debug.DrawRay(this.transform.position + new Vector3(0, -0.6f, 0), new Vector3(0, -0.5f, 0), Color.blue);
        Debug.DrawRay(this.transform.position + new Vector3(-0.6f, 0, 0), new Vector3(-0.5f, 0, 0), Color.blue);
        Debug.DrawRay(this.transform.position + new Vector3(0.6f, 0, 0), new Vector3(0.5f, 0, 0), Color.blue);
    }

    private void move()
    {
        if (rayUp.collider != null && rayUp.collider.name == enemy.name ||
            rayDown.collider != null && rayDown.collider.name == enemy.name ||
            rayLeft.collider != null && rayLeft.collider.name == enemy.name ||
            rayRight.collider != null && rayRight.collider.name == enemy.name)
        {
            if (currentDirection == Vector3.up || currentDirection == Vector3.down)
            {
                if (this.transform.position.y > 0 &&
                    this.transform.position.y <= (int)enemy.transform.position.y + 0.55f &&
                    this.transform.position.y >= (int)enemy.transform.position.y + 0.5f)
                {
                    currentDirection = Vector3.zero;
                    monsterState = MonsterState.Attack;
                    Debug.Log("Changing State - Attack");
                }
                else if (this.transform.position.y < 0 &&
                    this.transform.position.y <= (int)enemy.transform.position.y - 0.5f &&
                    this.transform.position.y >= (int)enemy.transform.position.y - 0.55f)
                {
                    currentDirection = Vector3.zero;
                    monsterState = MonsterState.Attack;
                    Debug.Log("Changing State - Attack");
                }
            }

            else if (currentDirection == Vector3.left || currentDirection == Vector3.right)
            {
                if (this.transform.position.x > 0 &&
                    this.transform.position.x <= (int)enemy.transform.position.x + 0.55f &&
                    this.transform.position.x >= (int)enemy.transform.position.x + 0.5f)
                {
                    currentDirection = Vector3.zero;
                    monsterState = MonsterState.Attack;
                    Debug.Log("Changing State - Attack");
                }
                else if (this.transform.position.x < 0 &&
                    this.transform.position.x <= (int)enemy.transform.position.x - 0.5f &&
                    this.transform.position.x >= (int)enemy.transform.position.x - 0.55f)
                {
                    currentDirection = Vector3.zero;
                    monsterState = MonsterState.Attack;
                    Debug.Log("Changing State - Attack");
                }
            }
        }

        else if (currentDirection == Vector3.up)
        {
            if (rayUp.collider != null && rayLeft.collider == null && rayRight.collider == null)
            {
                if (this.transform.position.y > 0 &&
                    this.transform.position.y <= (int)this.transform.position.y + 0.55f &&
                    this.transform.position.y >= (int)this.transform.position.y + 0.5f)
                {
                    Debug.Log("Vector3 Up Detected 1, Changing Direction");

                    int temp = Random.Range(0, 3);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.down;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.left;
                    }
                    else if (temp == 2)
                    {
                        currentDirection = Vector3.right;
                    }
                }

                else if (this.transform.position.y < 0 &&
                         this.transform.position.y <= (int)this.transform.position.y - 0.5f &&
                         this.transform.position.y >= (int)this.transform.position.y - 0.55f)
                {
                    Debug.Log("Vector3 Up Detected 1, Changing Direction");

                    int temp = Random.Range(0, 3);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.down;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.left;
                    }
                    else if (temp == 2)
                    {
                        currentDirection = Vector3.right;
                    }
                }
            }

            else if (rayUp.collider != null && rayLeft.collider != null && rayRight.collider == null)
            {
                if (this.transform.position.y > 0 &&
                    this.transform.position.y <= (int)this.transform.position.y + 0.55f &&
                    this.transform.position.y >= (int)this.transform.position.y + 0.5f)
                {
                    Debug.Log("Vector3 Up Detected 2, Changing Direction");

                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.down;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.right;
                    }
                }

                else if (this.transform.position.y < 0 &&
                         this.transform.position.y <= (int)this.transform.position.y - 0.5f &&
                         this.transform.position.y >= (int)this.transform.position.y - 0.55f)
                {
                    Debug.Log("Vector3 Up Detected 2, Changing Direction");

                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.down;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.right;
                    }
                }
            }

            else if (rayUp.collider != null && rayLeft.collider == null && rayRight.collider != null)
            {
                if (this.transform.position.y > 0 &&
                    this.transform.position.y <= (int)this.transform.position.y + 0.55f &&
                    this.transform.position.y >= (int)this.transform.position.y + 0.5f)
                {
                    Debug.Log("Vector3 Up Detected 3, Changing Direction");

                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.down;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.left;
                    }
                }

                else if (this.transform.position.y < 0 &&
                         this.transform.position.y <= (int)this.transform.position.y - 0.5f &&
                         this.transform.position.y >= (int)this.transform.position.y - 0.55f)
                {
                    Debug.Log("Vector3 Up Detected 3, Changing Direction");

                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.down;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.left;
                    }
                }
            }

            else if (rayUp.collider != null && rayLeft.collider != null && rayRight.collider != null)
            {
                if (this.transform.position.y > 0 &&
                    this.transform.position.y <= (int)this.transform.position.y + 0.55f &&
                    this.transform.position.y >= (int)this.transform.position.y + 0.5f)
                {
                    Debug.Log("Vector3 Up Detected 4, Changing Direction");
                    currentDirection = Vector3.down;
                }

                else if (this.transform.position.y < 0 &&
                         this.transform.position.y <= (int)this.transform.position.y - 0.5f &&
                         this.transform.position.y >= (int)this.transform.position.y - 0.55f)
                {
                    Debug.Log("Vector3 Up Detected 4, Changing Direction");
                    currentDirection = Vector3.down;
                }
            }
        }

        // Checking Down Vector
        else if (currentDirection == Vector3.down)
        {
            if (rayDown.collider != null && rayLeft.collider == null && rayRight.collider == null)
            {
                if (this.transform.position.y > 0 &&
                    this.transform.position.y <= (int)this.transform.position.y + 0.55f &&
                    this.transform.position.y >= (int)this.transform.position.y + 0.5f)
                {
                    Debug.Log("Vector3 Down Detected 1, Changing Direction");
                    int temp = Random.Range(0, 3);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.up;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.left;
                    }
                    else if (temp == 2)
                    {
                        currentDirection = Vector3.right;
                    }
                }

                else if (this.transform.position.y < 0 &&
                         this.transform.position.y <= (int)this.transform.position.y - 0.5f &&
                         this.transform.position.y >= (int)this.transform.position.y - 0.55f)
                {
                    Debug.Log("Vector3 Down Detected 1, Changing Direction");
                    int temp = Random.Range(0, 3);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.up;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.left;
                    }
                    else if (temp == 2)
                    {
                        currentDirection = Vector3.right;
                    }
                }
            }

            else if (rayDown.collider != null && rayLeft.collider != null && rayRight.collider == null)
            {
                if (this.transform.position.y > 0 &&
                    this.transform.position.y <= (int)this.transform.position.y + 0.55f &&
                    this.transform.position.y >= (int)this.transform.position.y + 0.5f)
                {
                    Debug.Log("Vector3 Down Detected 2, Changing Direction");
                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.up;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.right;
                    }
                }

                else if (this.transform.position.y < 0 &&
                         this.transform.position.y <= (int)this.transform.position.y - 0.5f &&
                         this.transform.position.y >= (int)this.transform.position.y - 0.55f)
                {
                    Debug.Log("Vector3 Down Detected 2, Changing Direction");
                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.up;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.right;
                    }
                }
            }

            else if (rayDown.collider != null && rayLeft.collider == null && rayRight.collider != null)
            {
                if (this.transform.position.y > 0 &&
                    this.transform.position.y <= (int)this.transform.position.y + 0.55f &&
                    this.transform.position.y >= (int)this.transform.position.y + 0.5f)
                {
                    Debug.Log("Vector3 Down Detected 3, Changing Direction");
                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.up;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.left;
                    }
                }

                else if (this.transform.position.y < 0 &&
                         this.transform.position.y <= (int)this.transform.position.y - 0.5f &&
                         this.transform.position.y >= (int)this.transform.position.y - 0.55f)
                {
                    Debug.Log("Vector3 Down Detected 3, Changing Direction");
                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.up;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.left;
                    }
                }
            }

            else if (rayDown.collider != null && rayLeft.collider != null && rayRight.collider != null)
            {
                if (this.transform.position.y > 0 &&
                    this.transform.position.y <= (int)this.transform.position.y + 0.55f &&
                    this.transform.position.y >= (int)this.transform.position.y + 0.5f)
                {
                    Debug.Log("Vector3 Down Detected 4, Changing Direction");
                    currentDirection = Vector3.up;
                }

                else if (this.transform.position.y < 0 &&
                         this.transform.position.y <= (int)this.transform.position.y - 0.5f &&
                         this.transform.position.y >= (int)this.transform.position.y - 0.55f)
                {
                    Debug.Log("Vector3 Down Detected 4, Changing Direction");
                    currentDirection = Vector3.up;
                }
            }
        }

        // Checking Left Vector
        else if (currentDirection == Vector3.left)
        {
            if (rayLeft.collider != null && rayUp.collider == null && rayDown.collider == null)
            {
                if (this.transform.position.x > 0 &&
                    this.transform.position.x <= (int)this.transform.position.x + 0.55f &&
                    this.transform.position.x >= (int)this.transform.position.x + 0.5f)
                {
                    Debug.Log("Vector3 Left Detected 1, Changing Direction");
                    int temp = Random.Range(0, 3);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.right;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.up;
                    }
                    else if (temp == 2)
                    {
                        currentDirection = Vector3.down;
                    }
                }

                else if (this.transform.position.x < 0 &&
                         this.transform.position.x <= (int)this.transform.position.x - 0.5f &&
                         this.transform.position.x >= (int)this.transform.position.x - 0.55f)
                {
                    Debug.Log("Vector3 Left Detected 1, Changing Direction");
                    int temp = Random.Range(0, 3);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.right;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.up;
                    }
                    else if (temp == 2)
                    {
                        currentDirection = Vector3.down;
                    }
                }
            }

            else if (rayLeft.collider != null && rayUp.collider != null && rayDown.collider == null)
            {
                if (this.transform.position.x > 0 &&
                    this.transform.position.x <= (int)this.transform.position.x + 0.55f &&
                    this.transform.position.x >= (int)this.transform.position.x + 0.5f)
                {
                    Debug.Log("Vector3 Left Detected 2, Changing Direction");
                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.right;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.down;
                    }
                }

                else if (this.transform.position.x < 0 &&
                         this.transform.position.x <= (int)this.transform.position.x - 0.5f &&
                         this.transform.position.x >= (int)this.transform.position.x - 0.55f)
                {
                    Debug.Log("Vector3 Left Detected 2, Changing Direction");
                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.right;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.down;
                    }
                }
            }

            else if (rayLeft.collider != null && rayUp.collider == null && rayDown.collider != null)
            {
                if (this.transform.position.x > 0 &&
                    this.transform.position.x <= (int)this.transform.position.x + 0.55f &&
                    this.transform.position.x >= (int)this.transform.position.x + 0.5f)
                {
                    Debug.Log("Vector3 Left Detected 3, Changing Direction");
                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.right;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.up;
                    }
                }

                else if (this.transform.position.x < 0 &&
                         this.transform.position.x <= (int)this.transform.position.x - 0.5f &&
                         this.transform.position.x >= (int)this.transform.position.x - 0.55f)
                {
                    Debug.Log("Vector3 Left Detected 3, Changing Direction");
                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.right;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.up;
                    }
                }
            }

            else if (rayLeft.collider != null && rayUp.collider != null && rayDown.collider != null)
            {
                if (this.transform.position.x > 0 &&
                    this.transform.position.x <= (int)this.transform.position.x + 0.55f &&
                    this.transform.position.x >= (int)this.transform.position.x + 0.5f)
                {
                    Debug.Log("Vector3 Left Detected 4, Changing Direction");
                    currentDirection = Vector3.right;
                }

                else if (this.transform.position.x < 0 &&
                         this.transform.position.x <= (int)this.transform.position.x - 0.5f &&
                         this.transform.position.x >= (int)this.transform.position.x - 0.55f)
                {
                    Debug.Log("Vector3 Left Detected 4, Changing Direction");
                    currentDirection = Vector3.right;
                }
            }
        }

        // Checking right Vector
        else if (currentDirection == Vector3.right)
        {
            if (rayRight.collider != null && rayUp.collider == null && rayDown.collider == null)
            {
                if (this.transform.position.x > 0 &&
                    this.transform.position.x <= (int)this.transform.position.x + 0.55f &&
                    this.transform.position.x >= (int)this.transform.position.x + 0.5f)
                {
                    Debug.Log("Vector3 Left Detected 1, Changing Direction");
                    int temp = Random.Range(0, 3);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.left;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.up;
                    }
                    else if (temp == 2)
                    {
                        currentDirection = Vector3.down;
                    }
                }

                else if (this.transform.position.x < 0 &&
                         this.transform.position.x <= (int)this.transform.position.x - 0.5f &&
                         this.transform.position.x >= (int)this.transform.position.x - 0.55f)
                {
                    Debug.Log("Vector3 Left Detected 1, Changing Direction");
                    int temp = Random.Range(0, 3);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.left;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.up;
                    }
                    else if (temp == 2)
                    {
                        currentDirection = Vector3.down;
                    }
                }
            }

            else if (rayRight.collider != null && rayUp.collider != null && rayDown.collider == null)
            {
                if (this.transform.position.x > 0 &&
                    this.transform.position.x <= (int)this.transform.position.x + 0.55f &&
                    this.transform.position.x >= (int)this.transform.position.x + 0.5f)
                {
                    Debug.Log("Vector3 Left Detected 2, Changing Direction");
                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.left;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.down;
                    }
                }

                else if (this.transform.position.x < 0 &&
                         this.transform.position.x <= (int)this.transform.position.x - 0.5f &&
                         this.transform.position.x >= (int)this.transform.position.x - 0.55f)
                {
                    Debug.Log("Vector3 Left Detected 2, Changing Direction");
                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.left;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.down;
                    }
                }
            }

            else if (rayRight.collider != null && rayUp.collider == null && rayDown.collider != null)
            {
                if (this.transform.position.x > 0 &&
                    this.transform.position.x <= (int)this.transform.position.x + 0.55f &&
                    this.transform.position.x >= (int)this.transform.position.x + 0.5f)
                {
                    Debug.Log("Vector3 Left Detected 3, Changing Direction");
                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.left;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.up;
                    }
                }

                else if (this.transform.position.x < 0 &&
                         this.transform.position.x <= (int)this.transform.position.x - 0.5f &&
                         this.transform.position.x >= (int)this.transform.position.x - 0.55f)
                {
                    Debug.Log("Vector3 Left Detected 3, Changing Direction");
                    int temp = Random.Range(0, 2);

                    if (temp == 0)
                    {
                        currentDirection = Vector3.left;
                    }
                    else if (temp == 1)
                    {
                        currentDirection = Vector3.up;
                    }
                }
            }

            else if (rayRight.collider != null && rayUp.collider != null && rayDown.collider != null)
            {
                if (this.transform.position.x > 0 &&
                    this.transform.position.x <= (int)this.transform.position.x + 0.55f &&
                    this.transform.position.x >= (int)this.transform.position.x + 0.5f)
                {
                    Debug.Log("Vector3 Left Detected 4, Changing Direction");
                    currentDirection = Vector3.left;
                }

                else if (this.transform.position.x < 0 &&
                         this.transform.position.x <= (int)this.transform.position.x - 0.5f &&
                         this.transform.position.x >= (int)this.transform.position.x - 0.55f)
                {
                    Debug.Log("Vector3 Left Detected 4, Changing Direction");
                    currentDirection = Vector3.left;
                }
            }
        }

        this.transform.position += currentDirection * movingSpeed * Time.deltaTime;
    }

    private void attack()
    {
        // When the object attacking, movingSpeed must be 0.
        //movingSpeed = 0;

        if (Vector3.Distance(this.transform.position, enemy.transform.position) <= attackRange + 0.1f)
        {
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                Debug.Log("Ataque - monster");
                enemy.GetComponent<enemy>().damageAction(attackPower);
                currentTime = 0;
            }
        }
        
        // Actually, This Part Is Not Necessary
        else
        {
            monsterState = MonsterState.Move;
            Debug.Log("Changing State - Move");
        }
    }

    private void damaged()
    {
        StartCoroutine(damageProcess());
    }

    IEnumerator damageProcess()
    {
        yield return new WaitForSeconds(0.5f);

        monsterState = MonsterState.Attack;
        Debug.Log("Changing State - Attack");
    }

    public void damageAction(int damage)
    {
        if (monsterState == MonsterState.Damaged || monsterState == MonsterState.Die)
        {
            return;
        }

        hp -= damage;

        if (hp > 0)
        {
            monsterState = MonsterState.Damaged;
            Debug.Log("Changing State - Damaged");
            damaged();
        }

        else
        {
            monsterState = MonsterState.Die;
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
