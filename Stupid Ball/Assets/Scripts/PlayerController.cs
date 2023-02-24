using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private float moveSpeed;
    private float normalSpeed = 20f;
    private float bootSpeed = 5f;
    private float jumpForce = 2f;
    [SerializeField] Rigidbody playerRigidbody;
    [SerializeField] Transform rotageTransform;
    private Vector3 movement;
    public bool isAlived;
    private bool isJumped;
    public bool isFinished;
    public int playerScore;

    void _MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        isAlived = true;
        moveSpeed = normalSpeed;
        playerScore = 0;
        _MakeInstance();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0 || verticalInput != 0)
        {
            movement.x = horizontalInput;
            movement.z = verticalInput;
            movement = movement.normalized;
            Quaternion toRotate = Quaternion.LookRotation(movement, Vector3.up);
            rotageTransform.rotation = toRotate;
            playerRigidbody.AddForce(movement * moveSpeed, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.Space) && isJumped == false)
        {
            Vector3 jump = new Vector3(0, 20, 0);
            playerRigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
            isJumped = true;
        }
    }

    private void OnCollisionStay(Collision target)
    {
        if (target.gameObject.CompareTag("Floor"))
        {
            isJumped = false;
        }
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.CompareTag("Die"))
        {
            isAlived = false;
            if (GamePlayController.instance != null && isAlived == false)
            {
                GamePlayController.instance.GameOver();
            }
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Speed"))
        {
            moveSpeed = moveSpeed + bootSpeed;
            StartCoroutine("SpeedDuration");
        }
        if (target.CompareTag("Finish"))
        {
            isFinished = true;
            playerScore = GamePlayController.instance.playerScore;
            GamePlayController.instance.FinishPanel();
        }
    }

    IEnumerable SpeedDuration()
    {
        yield return new WaitForSeconds(1);
        moveSpeed = normalSpeed;
    }
}
