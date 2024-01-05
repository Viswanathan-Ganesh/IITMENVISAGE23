using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class EnemyAttack : MonoBehaviour
{
    public Animator eAnimator;
    public Transform playerTr;
    public float timeBetweenShots;
    public float timer;
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject stone;
    public Vector3 yOffset;
    public Transform slashPoint;
    public float slashRange;
    public LayerMask playerLayer;
    public int slashDamage;
    public Vector3 slashPointOffset;
    public float facingDirection;
    public float asteroidForce;
    private float asteroidTimer;
    public float timeBetweenAsteroidAttack;

    public GameObject asteroidIndicator;

    public float degreesOfFall;
    public float rangeFromPlayer;
    public float yRandomRange;

    public float firstWaveHealth;
    public float secondWaveHealth;
    public float thirdWaveHealth;

    public float stoneShootingSpeed;
    public int noOfAsteroids;
    public int level;
    public Transform muzzle;
    public int ultimateSlashDamage;
    public float ultimateSlashOffset;
    public float ultimateSlashRange;
    public float ultimateSlashRunSpeed;

    public float enemyAttackAnimationTime;
    public float enemyAttackAnimationTimer;
    public float enemyHurtAnimationTime;
    public float enemyHurtAnimationTimer;

    private float dist;

    GameObject ai;

    // Update is called once per frame
    void Update()
    {
        
        dist = playerTr.position.x - transform.position.x;
        facingDirection = (dist)/Mathf.Abs(dist);

        
        if (facingDirection < 0)
        {
            slashPoint.position = transform.position - slashPointOffset;
            if(level == 1)
            {
                muzzle.position = transform.position - slashPointOffset;
            }
        }
        else if(facingDirection > 0) 
        {
            slashPoint.position = transform.position + slashPointOffset;
            if(level == 1)
            {
                muzzle.position = transform.position + slashPointOffset;
            }
        }

       
        // timer
        timer -= Time.deltaTime;
        asteroidTimer -= Time.deltaTime;
        enemyAttackAnimationTimer -= Time.deltaTime;
        enemyHurtAnimationTimer -= Time.deltaTime;

        if(enemyAttackAnimationTimer < 0)
        {
            eAnimator.SetBool("isSlashing", false);
        }
        /*
        if(enemyHurtAnimationTimer < 0) 
        {
            eAnimator.SetBool("isHurt", false);
        }
        */
        if (level == 2)
        {
            if (timer < 0 && Mathf.Abs(dist) <= slashRange + slashPointOffset.x)
            {
                /*
                Vector3 dir = playerTr.position - transform.position;
                dir = dir / dir.magnitude;
                float dis = (playerTr.position - transform.position).magnitude;
                if (dis <= ultimateSlashRange + ultimateSlashOffset && dis >= ultimateSlashRange - ultimateSlashRange)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, dir.y) * ultimateSlashRunSpeed;
                }
                */
                RandomizedAttack();
            }
        }
        else if (level == 1)
        {
            if (timer < 0)
            {
                /*
                Vector3 dir = playerTr.position - transform.position;
                dir = dir / dir.magnitude;
                float dis = (playerTr.position - transform.position).magnitude;
                if (dis <= ultimateSlashRange + ultimateSlashOffset && dis >= ultimateSlashRange - ultimateSlashRange)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, dir.y) * ultimateSlashRunSpeed;
                }
                */
                RandomizedAttack();
            }
        }
        
    }

    void RandomizedAttack()
    {
        /*
        if (level == 1)
        {
            int randomNum = Random.Range(1, 15);
            if (randomNum == 1)
            {
                for (int i = 0; i < noOfAsteroids; i++)
                {
                     AsteroidRain();
                }
                timer = timeBetweenShots;
            }
            else if (randomNum == 2 || randomNum == 3 || randomNum == 4 )  
            {
                ThrowingStone();
                timer = timeBetweenShots;
            }
            else if(randomNum == 7 || randomNum == 5 || randomNum == 8 || randomNum == 9 || randomNum == 10 || randomNum == 6)
            {
                WoodenLog();
                timer = timeBetweenShots;
            }
        }
        */
        if (level == 1)
        {
            if (gameObject.GetComponent<EnemyHealth>().GetHealth() >= firstWaveHealth)
            {
                WoodenLog();
                timer = timeBetweenShots;
            }
            else if (gameObject.GetComponent<EnemyHealth>().GetHealth() >= secondWaveHealth && gameObject.GetComponent<EnemyHealth>().GetHealth() <= firstWaveHealth)
            {
                ThrowingStone();
                eAnimator.SetBool("isThrowingStone", true);
                timer = timeBetweenShots;
            }
            else if (gameObject.GetComponent<EnemyHealth>().GetHealth() >= thirdWaveHealth && gameObject.GetComponent<EnemyHealth>().GetHealth() <= secondWaveHealth)
            {
                ai = Instantiate(asteroidIndicator, playerTr.position, Quaternion.identity);

                for (int i = 0; i < noOfAsteroids; i++)
                {
                    AsteroidRain();
                }
                timer = timeBetweenShots;
            }
        }
        else if(level == 2)
        {
            eAnimator.SetBool("isSlashing", true);
            WoodenLog();
            timer = timeBetweenShots;
            enemyAttackAnimationTimer = enemyAttackAnimationTime;
        }

    }

    // level 1 boss attacks

    //spl. attack astroid rain

    void AsteroidRain()
    {
        int randNum = Random.Range(1, 4);
        GameObject ast = null;
        float randomRangeFromPlayer = Random.Range(-rangeFromPlayer, rangeFromPlayer);
        float randomYOffset = Random.Range(-yRandomRange, yRandomRange);

        
        if (randNum == 1)
        {
            //ast = Instantiate(asteroid1, instantaneousPos.position + yOffset + new Vector3(yOffset.y * Mathf.Tan(degreesOfFall * Mathf.PI / 180) - randomRangeFromPlayer, randomYOffset, 0f), Quaternion.identity);
            ast = Instantiate(asteroid1, playerTr.position + yOffset + new Vector3(randomRangeFromPlayer, 0f, 0f), Quaternion.identity);
        }
        else if (randNum == 2)
        {
            ast = Instantiate(asteroid2, playerTr.position + yOffset + new Vector3(randomRangeFromPlayer, 0f, 0f), Quaternion.identity);
        }
        else if (randNum == 3)
        {
            ast = Instantiate(asteroid3, playerTr.position + yOffset + new Vector3(randomRangeFromPlayer, 0f, 0f), Quaternion.identity);
        }

        //ast.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, asteroidForce);

        Vector2 dir = new Vector2(ast.transform.position.x + randomRangeFromPlayer - playerTr.position.x, yOffset.y + randomYOffset);
        dir = dir / dir.magnitude;
        //ast.GetComponent<Rigidbody2D>().AddForce(dir * asteroidForce * Time.deltaTime, ForceMode2D.Impulse);
        //ast.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x * asteroidForce, dir.y * asteroidForce);
        ast.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, dir.y * asteroidForce);
        
        /*
        if (dir.x < 0f)
        {
            ast.GetComponent<SpriteRenderer>().flipX = true;
        }
        */
        

        /*
        Vector3 randomOffset = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 50f), 0f);
        if (randNum == 1)
        {
            ast = Instantiate(asteroid1, playerTr.position + offset + randomOffset, Quaternion.identity);
        }
        else if (randNum == 2)
        {
            ast = Instantiate(asteroid2, playerTr.position + offset + randomOffset, Quaternion.identity);
        }
        else if (randNum == 3)
        {
            ast = Instantiate(asteroid3, playerTr.position + offset + randomOffset, Quaternion.identity);
        }
        */
        //Vector2 dir = new Vector2(playerTr.position.x - transform.position.x, playerTr.position.y - ast.transform.position.y);
        // dir = dir / dir.magnitude;
        //ast.GetComponent<Rigidbody2D>().AddForce(dir * asteroidForce * Time.deltaTime, ForceMode2D.Impulse);
    }

    void ThrowingStone()
    {
        GameObject Stone = Instantiate(stone, muzzle.position, Quaternion.identity);
        Stone.GetComponent<Rigidbody2D>().velocity = new Vector2(stoneShootingSpeed * facingDirection, Stone.GetComponent<Rigidbody2D>().velocity.y);
        eAnimator.SetBool("isThrowningStone", true);
        
    }

    void WoodenLog()
    {
        
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(slashPoint.position, slashRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
            player.GetComponent<PlayerHealth>().Damage(slashDamage);
        
    }
    void OnDrawGizmos()
    {
        if (slashPoint == null)
            return;

        Gizmos.DrawWireSphere(slashPoint.position, slashRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        else
        {
            if (collision.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerHealth>().Damage(ultimateSlashDamage);
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                eAnimator.SetBool("isHurt", true);
                enemyHurtAnimationTimer = enemyHurtAnimationTime;
            }
        }
    }
}
