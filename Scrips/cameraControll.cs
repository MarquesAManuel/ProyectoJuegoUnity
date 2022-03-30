using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControll : MonoBehaviour
{
    public Transform lookAt;
    public static cameraControll instance;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private void Awake()
    {
        if (cameraControll.instance == null)
        {
            cameraControll.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
            
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        //Revisamos que el personaje este dentro de los limites del rango X de la camara
        float deltaX = lookAt.position.x - transform.position.x;
        if(deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        //Revisamos que el personaje este dentro de los limites del rango Y de la camara
        float deltaY = lookAt.position.y - transform.position.y;
        if(deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
