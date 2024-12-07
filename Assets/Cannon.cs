using UnityEngine;

public class Cannon : MonoBehaviour
{

    private float? touchStartPos = null;
    private float? cannonStartPos = null;
    public Transform maxX;
    public Transform minX;
    public float touchSpeed = 1;

    private bool isShooting = false;

    public float bigBallTime = 8;
    private float bigBallTimeCurrent = 0;
    private float shotWaitTime;
    public float shotCooldown;
    public float timeBetweenBullets = .1f;
    private float lastBulletTime = 0;
    public int shootAmount = 3;
    private int currentShotAmount = 0;

    public GameObject ball;
    public GameObject bigBall;
    public Transform ballShootPosition;

    public Transform mask;
    public SpriteRenderer barFill;
    public Color barFillColor;
    public Color barFullColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnityEngine.InputSystem.EnhancedTouch.TouchSimulation.Enable();
    }



    bool CanShootBigBall()
    {
        return bigBallTimeCurrent >= bigBallTime;
    }
    void ShootBig(bool isTouching)
    {
        if (isTouching)
        {
            bigBallTimeCurrent += Time.deltaTime;

         

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                if (CanShootBigBall())
                {
                    bigBallTimeCurrent = 0;
                    Instantiate(bigBall, ballShootPosition.position, Quaternion.identity);
                }
            }
               
             
            
        }

        var percentage = bigBallTimeCurrent / bigBallTime;
        barFill.color = barFillColor;
        if (CanShootBigBall())
        {
            barFill.color = barFullColor;
            percentage = 1;
        }
        mask.localScale = new Vector3(mask.localScale.x, percentage, mask.localScale.z);


    }
    void Shoot(bool isTouching)
    {
        var timePassed = Time.deltaTime;

       
      
        if (isShooting)
        {
            lastBulletTime -= timePassed;
            if (lastBulletTime <= 0)
            {
                Instantiate(ball, ballShootPosition.position, Quaternion.identity);
      
                lastBulletTime = timeBetweenBullets;
                currentShotAmount++;

                if (currentShotAmount == shootAmount)
                {
                    currentShotAmount = 0;
                    isShooting = false;
                }
            }
           
            return;

        }

        shotWaitTime -= timePassed;
        if (shotWaitTime <= 0  && isTouching)
        {
            shotWaitTime = shotCooldown;
            Instantiate(ball, ballShootPosition.position, Quaternion.identity);
            lastBulletTime = timeBetweenBullets;
            currentShotAmount = 1;
            isShooting = true;
            
        }
      
    }

    void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position.x;
                cannonStartPos = transform.position.x;
            }
            // Move the cube if the screen has the finger moving.
            else if (touch.phase == TouchPhase.Moved && touchStartPos.HasValue && cannonStartPos.HasValue)
            {
                float diff = (float)(touch.position.x - touchStartPos);
                var x = (diff * touchSpeed) + (float)cannonStartPos;
                var clampedX = Mathf.Clamp(x, minX.position.x, maxX.position.x);
                transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
            }


        }

    }

    // Update is called once per frame
    void Update()
    {
        HandleTouch();
        Shoot(Input.touchCount > 0);
        ShootBig(Input.touchCount > 0);
    }

}
