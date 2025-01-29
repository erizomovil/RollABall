using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 5;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private float movementX;
    private float movementY;
    private int count;

    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        winTextObject.SetActive(false);
        SetCountText();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)){
            speed = 10f;
        } else
            speed = 5f;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            count++;
            other.gameObject.SetActive(false);
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            Time.timeScale = 0;
            Destroy(gameObject);
        }
    }
}
