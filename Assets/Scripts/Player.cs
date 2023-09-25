using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
    private Rigidbody _rb;

    public GameObject bulletPrefab;
    public GameObject bulletSpawner;

    public float rotationSpeed = 100f;
    public float thrustForce = 100f;

    public static int SCORE = 0;
    public static float xBorderLimit, yBorderLimit;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        yBorderLimit = Camera.main.orthographicSize + 1;
        xBorderLimit = (Camera.main.orthographicSize+1) * Screen.width/Screen.height;
    }

    private void FixedUpdate()
    {
        float thrust = Input.GetAxis("Thrust") * thrustForce * Time.deltaTime;
        float rotation = Input.GetAxis("Rotate") * rotationSpeed * Time.deltaTime;

        Vector2 movementDirection = transform.right;

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);//indicamos como gira 

        _rb.AddForce(thrust * movementDirection);
    }
    void Update()
    {
        var newPos = transform.position;
        if (newPos.x > xBorderLimit) newPos.x = -xBorderLimit + 1;
        else if (newPos.x < -xBorderLimit) newPos.x = xBorderLimit - 1;
        else if (newPos.y > yBorderLimit) newPos.y = -yBorderLimit + 1;
        else if (newPos.y < -yBorderLimit) newPos.y = yBorderLimit - 1;
        transform.position = newPos;

        if (Input.GetKeyDown(KeyCode.Space)){
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, Quaternion.identity);//giro ninguno
            Bullet balaScript = bullet.GetComponent<Bullet>();
            balaScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;
            RestartGame();
            
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
