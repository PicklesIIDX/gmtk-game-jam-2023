using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float playerSpeed;
    public float pullSpeed;
    public Rigidbody2D rigidbody;
    public Rigidbody2D personRigidbody;
    //Bravery meter
    //scared - brave - chaos
    private Vector2 movementDirection;
    private Vector2 inputDirection;
    float angleToFace;
    public HeroAnimationControl AnimationControl;

    Vector2 mousePos;

    public bool legControl;
    bool isMovementCorRunning = false;

    bool isSwinging = false;
    public float swingTime;

    public bool isSword;
    public GameObject theSword;
    public BoxCollider2D theSwordBox;
    public ParticleSystem swordBurst;
    public bool isSpear;
    public GameObject theSpear;
    public ParticleSystem spearBurst;
    public bool isAxe;
    public ParticleSystem axeBurst;
    public GameObject theAxe;

    public bool isSheild;

    public bool isBomb;
    public ParticleSystem bombBurst;

    // Update is called once per frame
    void Update()
    {
        InputCommands();
        AnimationControl.UpdateAnimator(movementDirection);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputCommands()
    {
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");

        //Are we boots?
        if (legControl)
        {
            movementDirection.x = inputDirection.x;
            movementDirection.y = inputDirection.y;
        }
        else
        {
            if (!isMovementCorRunning)
            {
                StartCoroutine(RandomMovement());
            }
        }

        if (!isSwinging)
        {
            //Are we a weapon?
            Weapons();

            //Are we a bomb?
            BombBlow();
        }

    }

    public void Weapons()
    {
        if (isSword)
        {
            if (Input.GetKeyDown("space"))
            {
                StartCoroutine(SwordBurstCor());
            }
        }

        else if (isSpear)
        {
            if (Input.GetKeyDown("space"))
            {
                StartCoroutine(SpearBurstCor());
            }
        }
        else if (isAxe)
        {
            if (Input.GetKeyDown("space"))
            {
                StartCoroutine(AxeBurstCor());
            }
        }
        else if (isSheild)
        {
            //Need to use angle taken from inputDirection.x and y to get direction it should face or use animation
        }
    }

    public void BombBlow()
    {
        if (isBomb)
        {
            if (Input.GetKeyDown("space"))
            {
                bombBurst.Emit(100);
            }
        }
    }

    IEnumerator SwordBurstCor()
    {
        isSwinging = true;
        swordBurst.Emit(100);

        float time = 0f;
        var start = Quaternion.identity;
        var end = Quaternion.Euler(0, 0, -90);
        while (time < swingTime)
        {
            theSwordBox.enabled = true;
            time += Time.deltaTime;
            theSword.transform.localRotation = Quaternion.Slerp(start, end, time / swingTime);
            personRigidbody.transform.position = Vector2.MoveTowards(personRigidbody.transform.position, mousePos, time / pullSpeed);

            yield return null;
        }
        time = 0f;
        while (time < swingTime)
        {
            theSwordBox.enabled = false;
            time += Time.deltaTime *2;
            theSword.transform.localRotation = Quaternion.Slerp(end, start, time / swingTime);
            yield return null;
        }
        isSwinging = false;
    }

    IEnumerator SpearBurstCor()
    {
        //Need to use angle taken from inputDirection.x and y to get direction it should face or use animation
        spearBurst.Emit(100);
        yield return new WaitForSeconds(1);
    }

    IEnumerator AxeBurstCor()
    {
        //Need to use angle taken from inputDirection.x and y to get direction it should face or use animation
        axeBurst.Emit(100);
        yield return new WaitForSeconds(2);
    }


    IEnumerator RandomMovement()
    {
        //Needs general direction it should strive for with some tossed in chaos
        isMovementCorRunning = true;
        movementDirection.x = Random.Range(-1.0f, 1.0f);
        movementDirection.y = Random.Range(-1.0f, 1.0f);
        yield return new WaitForSeconds(1);
        isMovementCorRunning = false;
    }

    void Move()
    {
        rigidbody.MovePosition(rigidbody.position + movementDirection.normalized * playerSpeed * Time.fixedDeltaTime);
        personRigidbody.MovePosition(personRigidbody.position + movementDirection.normalized * playerSpeed * Time.fixedDeltaTime);
        Vector2 lookDirection = mousePos - rigidbody.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rigidbody.rotation = angle;
        angleToFace = angle;

        //theSwordRig.rotation = angleW;
        

    }
}
