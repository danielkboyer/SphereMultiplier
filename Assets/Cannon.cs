using Assets.Scripts;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public GameObject RegularGun;
    public GameObject ShotGun;

    public Animator CannonAnimator;
    private float? touchStartPos = null;
    private float? cannonStartPos = null;
    public Transform maxX;
    public Transform minX;
    public float touchSpeed = 1;


    public float bigBallTime = 8;
    private float bigBallTimeCurrent = 0;
    private float shotWaitTime;
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

    //This is regular gun
    public Renderer[] ChangeRenderers;
    public Renderer[] ShotGunRenderers;

    private Renderer[] Renderers;
    private Color[] originalMaterials;

    private CannonData selectedCannon;
    private bool isGameOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //UnityEngine.InputSystem.EnhancedTouch.TouchSimulation.Enable();

        var gameData = GameStorage.GetInstance().GetGameData();

        selectedCannon = gameData.SelectedCannon;
        RegularGun.SetActive(false);
        ShotGun.SetActive(false);
        Debug.Log("Selected cannon: " + selectedCannon);
        switch (selectedCannon.type)
        {
            case CannonType.RegularGun:
                RegularGun.SetActive(true);
                Renderers = ChangeRenderers;
                break;
            case CannonType.ShotGun:
                ShotGun.SetActive(true);
                Renderers = ShotGunRenderers;
                break;
        }
      
        CannonAnimator.SetFloat("ShootTime", 1/timeBetweenBullets);
        originalMaterials = Renderers.Select(t=>new Color(t.material.color.r,t.material.color.g,t.material.color.b,t.material.color.a)).ToArray();
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
       

        for(int i = 0; i < Renderers.Length; i++)
        {
            if (color == null)
            {
                Renderers[i].material.color = originalMaterials[i];
            }
            else
            {
                Renderers[i].material.color = color.Value;
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
                    switch(selectedCannon.type)
                    {
                        case CannonType.RegularGun:
                            RegularGunShootBig();
                            break;
                        case CannonType.ShotGun:
                            ShotGunShootBig();
                            break;
                    }
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
         
            shotWaitTime = selectedCannon.shotCooldown;
            CannonAnimator.SetTrigger("Shoot");
            switch(selectedCannon.type)
            {
                case CannonType.RegularGun:
                    RegularGunShoot();
                    break;
                case CannonType.ShotGun:
                    ShotGunShoot();
                    break;
            }

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



    void RegularGunShoot()
    {
        Instantiate(ball, ballShootPosition.position, Quaternion.identity);
    }

    void RegularGunShootBig()
    {
        Instantiate(bigBall, ballShootPosition.position, Quaternion.identity);
    }


    void ShotGunShoot()
    {
        var ballWidth = ball.GetComponent<Renderer>().bounds.size.x;

        Instantiate(ball, ballShootPosition.position + new Vector3(ballWidth,0,0), Quaternion.identity);
        Instantiate(ball, ballShootPosition.position, Quaternion.identity);
        Instantiate(ball, ballShootPosition.position + new Vector3(-ballWidth, 0, 0), Quaternion.identity);
    }

    void ShotGunShootBig()
    {
        Instantiate(bigBall, ballShootPosition.position, Quaternion.identity);
    }

}
