using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health Parameters")]
    [SerializeField] int maxCharge = 10;
    [SerializeField] int StartingCharge = 5;
    [SerializeField] int currentCharge;
    [SerializeField] int decraseChargePerFiveSeconds = 1;
    [SerializeField] int chargePerBattery = 1;

    [Space]

    [Header("Movement Parameters")]
    [SerializeField] float forwardSpeed = 5f;
    [SerializeField] float jumpSpeed = 3f;
    [SerializeField] float moveZpos = 3f;

    bool hasHorizontalSpeed = false;
    bool isAlive = true;
    float[] targetPosX = { -2.5f, 0, 2.5f };
    float count = 0;
    int laneIndex = 1; // 0 = left lane, 1 = middle lane, 2 = right lane

    // Cache Components
    ChargeBar chargeBar;
    Rigidbody myRigidbody;
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        myRigidbody = GetComponentInChildren<Rigidbody>();
        chargeBar = FindObjectOfType<ChargeBar>();

        currentCharge = maxCharge;
        chargeBar.SetMaxCharge(maxCharge);
        DealCharge(-(maxCharge - StartingCharge));
    }

    void Update()
    {
        if (!GameSession.isGameStart) 
        {
            return;
        }
        else
        {
            animator.SetTrigger("GameStart");
        }
        if (!isAlive || GameSession.youLose) { return; }



        #region Decrease battery variable unit per 5 seconds
        if (currentCharge > 0)
        {
            count += Time.deltaTime;
        }
        while(count > 2)
        {
            DealCharge(-decraseChargePerFiveSeconds);
            count = 0;
        }
        #endregion

        myRigidbody.velocity = new Vector3(transform.position.x, myRigidbody.velocity.y, forwardSpeed);

        Jump();

        #region Move Left, Right

        if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeManager.IsSwipingLeft())
        {
            if (hasHorizontalSpeed) { return; } // prevent multi change lane

            hasHorizontalSpeed = true;
            
            laneIndex--;
            if (laneIndex < 0) { laneIndex = 0; }

            StartCoroutine(Move(0.3f));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || SwipeManager.IsSwipingRight())
        {
            if (hasHorizontalSpeed) { return; } // prevent multi change lane

            hasHorizontalSpeed = true;

            laneIndex++;
            if (laneIndex > 2) { laneIndex = 2; }

            StartCoroutine(Move(0.3f));
        }

        #endregion
    }

    IEnumerator Move(float duration)
    {
        Vector3 targetPos = new Vector3(targetPosX[laneIndex], 0, transform.position.z + moveZpos);

        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;     
        }
        transform.position = targetPos;
        hasHorizontalSpeed = false;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || SwipeManager.IsSwipingUp())
        {
            if(Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon) { return; }

            Vector3 jumpVelocityToAdd = new Vector3(0f, jumpSpeed, 0f);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
        bool isJumping = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        animator.SetBool("Jump", isJumping);
    }

    void DealCharge(int amount)
    {
        currentCharge += amount;
        chargeBar.SetCharge(currentCharge);

        if (currentCharge <= 0)
        {
            GameSession.youLose = true;
            animator.SetBool("isEmpty", GameSession.youLose);
            isAlive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Player is dead");
            animator.SetTrigger("Dying");
            GameSession.youLose = true;
            isAlive = false;
            myRigidbody.velocity = Vector3.zero;
        }

        if (other.gameObject.tag == "Battery")
        {
            DealCharge(chargePerBattery);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("Game is finish");
            GameSession.isGameFinish = true;
        }
    }
}
