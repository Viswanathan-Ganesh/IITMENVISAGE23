using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
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

    public float degreesOfFall;
    public float rangeFromPlayer;
    public float yRandomRange;

    public float stoneShootingSpeed;
    public int noOfAsteroids;
    public int level;
    public Transform muzzle;

    // Update is called once per frame
    void Update()
    {
        facingDirection = (playerTr.position.x - transform.position.x)/Mathf.Abs((playerTr.position.x - transform.position.x));

        
        if (facingDirection < 0)
        {
            slashPoint.position = transform.position - slashPointOffset;
            muzzle.position = transform.position - slashPointOffset;
        }
        else if(facingDirection > 0) 
        {
            slashPoint.position = transform.position + slashPointOffset;
            muzzle.position = transform.position + slashPointOffset;
        }

        
        // timer
        timer -= Time.deltaTime;
        asteroidTimer -= Time.deltaTime;

        if (timer < 0)
        {

            RandomizedAttack();
           
            //timer = timeBetweenShots;
        }
    }

    void RandomizedAttack()
    {
        if (level == 1)
        {
            for (int i = 0; i < noOfAsteroids; i++)
            {

                    AsteroidRain();
                
            }
            timer = timeBetweenShots;
            /*
            int randomNum = Random.Range(1, 11);
            if (randomNum == 1)
            {
                for (int i = 0; i < noOfAsteroids; i++)
                {
                    if (asteroidTimer < 0)
                    {
                        AsteroidRain();
                    }
                    asteroidTimer = timeBetweenAsteroidAttack;
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
            */
        }

    }

    // level 1 boss attacks

    //spl. attack astroid rain

    /*
     angle of fall
     yOffset
     range from player

    Instatiate(asteroid1, playerTr.position + yOffset + new Vector3(-y*tan(angleOfFall) - Random.Range(rangeFromPlayer), 0f, 0f), Quaternion.identity)
    */

    void AsteroidRain()
    {
        int randNum = Random.Range(1, 4);
        GameObject ast = null;
        float randomRangeFromPlayer = Random.Range(-rangeFromPlayer, rangeFromPlayer);
        float randomYOffset = Random.Range(-yRandomRange, yRandomRange);

        if (randNum == 1)
        {
            ast = Instantiate(asteroid1, playerTr.position + yOffset + new Vector3(-yOffset.y * Mathf.Tan(degreesOfFall * Mathf.PI / 180) - randomRangeFromPlayer, randomYOffset, 0f), Quaternion.identity);
        }
        else if (randNum == 2)
        {
            ast = Instantiate(asteroid2, playerTr.position + yOffset + new Vector3(-yOffset.y * Mathf.Tan(degreesOfFall * Mathf.PI / 180) - randomRangeFromPlayer, randomYOffset, 0f), Quaternion.identity);
        }
        else if (randNum == 3)
        {
            ast = Instantiate(asteroid3, playerTr.position + yOffset + new Vector3(-yOffset.y * Mathf.Tan(degreesOfFall  * Mathf.PI / 180) - randomRangeFromPlayer, randomYOffset, 0f), Quaternion.identity);
        }
        
        Vector2 dir = new Vector2(playerTr.position.x + randomRangeFromPlayer, yOffset.y);
        dir = dir / dir.magnitude;
        ast.GetComponent<Rigidbody2D>().AddForce(dir * asteroidForce * Time.deltaTime, ForceMode2D.Impulse);
        
        

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
}
