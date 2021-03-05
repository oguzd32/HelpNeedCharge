using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health Parameters")]
    [SerializeField] int maxCharge = 10;
    [SerializeField] int StartingCharge = 5;
    [SerializeField] int currentCharge;
    [SerializeField] int decraseChargePerFiveSeconds = 1;
    [SerializeField] int chargePerBattery = 1;

    [Header("Movement Parameters")]
    [SerializeField] float forwardSpeed = 5f;
    [SerializeField] float jumpSpeed = 3f;

    bool isAlive = true;
    float count = 0;

    [Header("Cache Components")]
    ChargeBar chargeBar;
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        chargeBar = FindObjectOfType<ChargeBar>();

        currentCharge = maxCharge;
        chargeBar.SetMaxCharge(maxCharge);
        DealCharge(-(maxCharge - StartingCharge));
    }

    void Update()
    {
        if (!GameSession.isGameStart) { return; }
        if (!isAlive) { return; }
        if(currentCharge > 0)
        {
            count += Time.deltaTime;
        }
        while(count > 5)
        {
            DealCharge(-decraseChargePerFiveSeconds);
            count = 0;
        }
        myRigidbody.velocity = new Vector3(0, myRigidbody.velocity.y, forwardSpeed);
        Move();
        Jump();
    }

    void Move()
    {
        #region Keyboard Input Control
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position.x < 0) { return; }
            transform.position += Vector3.left * 2.5f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x > 0) { return; }
            //transform.position += Vector3.right * 2.5f;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(2.5f, transform.position.y, transform.position.z), 2.5f);
        }
        #endregion
       
        #region Touch Input Control
        if (SwipeManager.IsSwipingLeft())
        {
            if (transform.position.x < 0) { return; }
            transform.position += Vector3.left * 2.5f;
        }

        if (SwipeManager.IsSwipingRight())
        {
            if (transform.position.x > 0) { return; }
            transform.position += Vector3.right * 2.5f;
        }
        #endregion
    }

    void Jump()
    {
        // Keyboard Input Control
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(transform.position.y > 1) { return; }
            Vector3 jumpVelocityToAdd = new Vector3(0f, jumpSpeed, 0f);
            myRigidbody.velocity += jumpVelocityToAdd;
        }

        // Touch Input Control
        if (SwipeManager.IsSwipingUp())
        {
            if (transform.position.y > 1) { return; }
            Vector3 jumpVelocityToAdd = new Vector3(0f, jumpSpeed, 0f);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    void DealCharge(int amount)
    {
        currentCharge += amount;
        chargeBar.SetCharge(currentCharge);

        if (currentCharge <= 0)
        {
            GameSession.youLose = true;
            isAlive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Player is dead");
            GameSession.youLose = true;
            isAlive = false;
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
