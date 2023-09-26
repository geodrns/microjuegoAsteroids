using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public GameObject fragmentPreFab;

    public float speed = 10f;
    public float maxLifeTime = 3f;

    public Vector3 targetVector;
    void Start()
    {
        //Destroy(gameObject, maxLifeTime);
    }

    void Update()
    {
        transform.Translate(targetVector * speed * Time.deltaTime);

        var newPos = transform.position;
        if (newPos.x > Player.xBorderLimit) gameObject.SetActive(false); 
        else if (newPos.x < -Player.xBorderLimit) gameObject.SetActive(false);
        else if (newPos.y > Player.yBorderLimit) gameObject.SetActive(false);
        else if (newPos.y < -Player.yBorderLimit) gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            //Destroy(gameObject);//destruye bala
            gameObject.SetActive(false);
            Destroy(collision.gameObject);//destruye asteroide
            IncreaseScore();

            Fragments(collision.transform.position);//obtengo la posicion para q sea el spawn de los fragmentos
        }

        else if (collision.gameObject.CompareTag("Fragment"))
        {
            gameObject.SetActive(false);
            Destroy(collision.gameObject);//destruye fragmento
            //Destroy(gameObject);//destruye bala
            IncreaseScore();
        }
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Score: " + Player.SCORE;

    }

    private void Fragments(Vector2 spawnPosition)
    {
        GameObject fragment1 = Instantiate(fragmentPreFab, spawnPosition, transform.rotation * Quaternion.Euler(0f, 0f, -50f));//giro izq
        fragment1.GetComponent<Rigidbody>().velocity = -50 * transform.localScale.y * fragment1.transform.right * Time.deltaTime;//para dar efecto de separacion
        spawnPosition.x +=1.5f;//separacion entre ambos fragmentos
        GameObject fragment2 = Instantiate(fragmentPreFab, spawnPosition, transform.rotation * Quaternion.Euler(0f, 0f, 50f));//giro der
        fragment2.GetComponent<Rigidbody>().velocity = 50 * transform.localScale.y * fragment1.transform.right * Time.deltaTime;//para dar efecto de separacion

        Destroy(fragment1, 4f);
        Destroy(fragment2, 4f);

    }

}
