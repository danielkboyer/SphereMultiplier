using Assets.Scripts;
using System.Linq;
using TMPro;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public Animator CannonAnimator;
    private float? touchStartPos = null;
    private float? cannonStartPos = null;
    public Transform maxX;
    public Transform minX;
    public float touchSpeed = 1;


    public float bigBallTime = 8;
    private float bigBallTimeCurrent = 0;
    private float shotWaitTime;
    public float shotCooldown;
    public float timeBetweenBullets = .1f;
    public int shootAmount = 3;

    public TextMeshPro TutorialText;
    public float whenToShowShoot = 2;
    public float whenToShowBigBall = .5f;
    private float lastTimeTouched = 0;
    private float canShootBigBallTime = 0;
    public GameObject ball;
    public GameObject bigBall;
    public Transform ballShootPosition;

    public Transform mask;
    public SpriteRenderer barFill;
    public Color barFillColor;
   // public Color barFullColor;
    public Color cannonBarFullColor;

    public Renderer[] ChangeRenderers;
    private Color[] originalMaterials;

    private bool isGameOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //UnityEngine.InputSystem.EnhancedTouch.TouchSimulation.Enable();

        var gameData = GameStorage.GetInstance().GetGameData();
      
        CannonAnimator.SetFloat("ShootTime", 1/timeBetweenBullets);
        originalMaterials = ChangeRenderers.Select(t=>new Color(t.material.color.r,t.material.color.g,t.material.color.b,t.material.color.a)).ToArray();
    }

    public void SetGameOver()
    {
        isGameOver = true;
    }

    bool CanShootBigBall()
    {
        return bigBallTimeCurrent >= bigBallTime;
    }

    void ChangeMaterial(Color? color)
    {
       

        for(int i = 0; i < ChangeRenderers.Length; i++)
        {
            if (color == null)
            {
                ChangeRenderers[i].material.color = originalMaterials[i];
            }
            else
            {
                ChangeRenderers[i].material.color = color.Value;
            }
        }
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
                    CannonAnimator.SetTrigger("Shoot");
                    ChangeMaterial(null);
                    bigBallTimeCurrent = 0;
                    Instantiate(bigBall, ballShootPosition.position, Quaternion.identity);
                }
            }



        }
        else
        {
            canShootBigBallTime = 0;
        }

        var percentage = bigBallTimeCurrent / bigBallTime;
        barFill.color = barFillColor;
        if (CanShootBigBall())
        {
            canShootBigBallTime += Time.deltaTime;
            barFill.color = cannonBarFullColor;
            percentage = 1;
            ChangeMaterial(cannonBarFullColor);
        }
        mask.localScale = new Vector3(mask.localScale.x, percentage, mask.localScale.z);


    }
    void Shoot(bool isTouching)
    {
        var timePassed = Time.deltaTime;


        shotWaitTime -= timePassed;
        if (shotWaitTime <= 0 && isTouching)
        {
         
            shotWaitTime = shotCooldown;
            CannonAnimator.SetTrigger("Shoot");
            Instantiate(ball, ballShootPosition.position, Quaternion.identity);

        }

    }

    void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            lastTimeTouched = 0;
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
        if (isGameOver)
        {
            return;
        }
        lastTimeTouched += Time.deltaTime;
        HandleTouch();
        Shoot(Input.touchCount > 0);
        ShootBig(Input.touchCount > 0);

        if (lastTimeTouched > whenToShowShoot)
        {
            TutorialText.text = "Hold to shoot";
        }
        else if (canShootBigBallTime > whenToShowBigBall && CanShootBigBall())
        {
            TutorialText.text = "Release for Big Ball!";
        }
        else
        {
            TutorialText.text = "";
        }
    }

}
