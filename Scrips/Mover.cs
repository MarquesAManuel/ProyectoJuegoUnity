using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;


    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    protected virtual void UpdateMotor(Vector3 input)
    {
        //Reseteo la posicion de Delta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //Muevo el personaje segun el input de izquierda o derecha
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }

        //Add push vector
        moveDelta += pushDirection;

        //Reduce the pushForce,per frame
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);
        

        //Revisamos que no encontramos nada con lo que choque nuestro personaje,si devuelve null,entonces podemos seguir avanzando en esa direccion
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //Mover el personaje en el mapa
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);

        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //Mover el personaje en el mapa
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);

        }
    }
}
