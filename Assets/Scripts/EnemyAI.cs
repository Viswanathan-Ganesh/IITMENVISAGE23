using UnityEngine;


public class EnemyAI : MonoBehaviour
{

    public Animator enemyAnimator;
    public Rigidbody2D rb;
    public float enemyVisionDistance;
    public float enemySpeed;
    public Transform playerTr;

    public float dodgeForce;
    public LayerMask playerLayer;
    public float enemyAttackRange;
    public bool isDodging = false;

    public float dodgeCooldown;
    private float dodgeTimer;
    public int level;

    public float firstWaveHealth;
    public float secondWaveHealth;
    public float thirdWaveHealth;

    public float secondWaveRange;
    public float thirdWaveRange;


    void FixedUpdate()
    {
        Vector2 positionVec = transform.position - playerTr.position;

        if (positionVec.x < 0f )
        {
            gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        else
        {
            gameObject.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        float distance = positionVec.magnitude;
        Vector2 VelocityDirection = -(positionVec) / positionVec.magnitude;
        Vector2 vel = new Vector2(enemySpeed * VelocityDirection.x, 0f);
        if (gameObject.GetComponent<EnemyHealth>().GetHealth() >= firstWaveHealth)
        {
            if (distance <= enemyVisionDistance && distance > enemyAttackRange + 0.2f && isDodging == false)
            {
                rb.velocity = new Vector2(enemySpeed * VelocityDirection.x, rb.velocity.y);
            }
            else if (distance < enemyAttackRange - 0.2f && distance >= 0f && isDodging == false)
            {
                rb.velocity = new Vector2(-enemySpeed * VelocityDirection.x, rb.velocity.y);
            }
            else if (isDodging == false)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
        else if (gameObject.GetComponent<EnemyHealth>().GetHealth() >= secondWaveHealth)
        {
            if (distance <= enemyVisionDistance && distance > secondWaveRange + 0.2f && isDodging == false)
            {
                rb.velocity = new Vector2(enemySpeed * VelocityDirection.x, rb.velocity.y);
            }
            else if (distance < secondWaveRange - 0.2f && distance >= 0f && isDodging == false)
            {
                rb.velocity = new Vector2(-enemySpeed * VelocityDirection.x, rb.velocity.y);
            }
            else if (isDodging == false)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
        else if(gameObject.GetComponent<EnemyHealth>().GetHealth() >= thirdWaveHealth)
        {
            if (distance <= enemyVisionDistance && distance > thirdWaveRange + 0.2f && isDodging == false)
            {
                rb.velocity = new Vector2(enemySpeed * VelocityDirection.x, rb.velocity.y);
            }
            else if (distance < thirdWaveRange - 0.2f && distance >= 0f && isDodging == false)
            {
                rb.velocity = new Vector2(-enemySpeed * VelocityDirection.x, rb.velocity.y);
            }
            else if (isDodging == false)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }

        if (distance <= playerTr.GetComponent<PlayerMoves>().normalRange)
        {
            Vector2 facingDirection = playerTr.GetComponent<PlayerMoves>().facingDir;
            float relX = positionVec.x / Mathf.Abs(positionVec.x);
            bool grounded = playerTr.GetComponent<PlayerMovement>().isLanded;


            if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.S) || playerTr.GetComponent<PlayerMoves>().isCriting) && (relX > 0f && facingDirection.x > 0f || relX < 0f && facingDirection.x < 0f))
            {
                Debug.Log("Deez");
                Dodge(facingDirection, level);
                playerTr.GetComponent<PlayerMoves>().isCriting = false;
            }
        }

        if (isDodging)
        {
            enemyAnimator.SetBool("isDodging", true);

        }else if(level == 2 && isDodging == false)
        {
            enemyAnimator.SetBool("isDodging", false);
        }
        // Timer

        if (isDodging == true)
        {
            dodgeTimer -= Time.deltaTime;
        }
        if (dodgeTimer <= 0f)
        {
            isDodging = false;
        }
    }

    void Dodge(Vector2 facingDirection, int level) 
    {
        if (Randomized(level))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(facingDirection * dodgeForce * Time.deltaTime, ForceMode2D.Impulse);
            isDodging = true;
            dodgeTimer = dodgeCooldown;
            Debug.Log("HI");
        }
    }

    bool Randomized(int Level)
    {
        if (level == 1)
        {
            // 50% possiblity
            int rand = Random.Range(1, 2);

            if (rand == 1)
            {
                isDodging = true;
                return true;
            }
            else
            {
                isDodging = false;
                return false;
            }
        }
        else if (level == 2)
        {
            // 50 % possiblity
            int rand = Random.Range(1, 5);

            if (rand == 1)
            {
                isDodging = true;
                return true;
            }
            else
            {
                isDodging = false;
                return false;
            }
        }
        else if (level == 3)
        {
            // 70 % possiblity
            int rand = Random.Range(1, 10);

            if (rand == 1 || rand == 2 || rand == 3 || rand == 4 || rand == 5 || rand == 6 || rand == 7 || rand == 8 || rand == 9)
            {
                isDodging = true;
                return true;
            }
            else
            {
                isDodging = false;
                return false;
            }
        }
        else
        {
            isDodging = false;
            return false;
        }
    }
}
