using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptPlayer : MonoBehaviour
{
    public float turnSpeed = 20;//velocidad de giro

    Animator anim;
    Rigidbody rb;
    AudioSource audioS;
    Vector3 movement;//variable para ir haciendo referencia al movimiento del jugador
    Quaternion rotation;

    float cronometro;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();


    }

    private void Update()
    {
        //Poner un contador de tiempo para controlar que, si han pasado más de x segundos la escena se reinicia.
        cronometro += Time.deltaTime;

        if (cronometro > 59)
        {
            SceneManager.LoadScene(0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()//usamos fixedupdate porque vamos a tratar con las maravillosas fisicas de unity
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //MOVEMENT REPRESENTA LA DIRECCION DE MOVIMIENTO
        movement.Set(horizontal, 0, vertical);//el set usamos para establecer los valores del vector,
        //seria lo mismo que poner "movement = new Verctor3(bla bla bla)
        movement.Normalize();//normalizamos el vector para que tenga la misma velocidad en horizontal, vertical, diagonal

        if (horizontal == 0 && vertical == 0) anim.SetBool("IsWalking", false);
        else anim.SetBool("IsWalking", true);

        //Audio
        if (anim.GetBool("IsWalking"))//si el paramentro es true, es decir, si el jugador se mueve,
        {
            if (!audioS.isPlaying)//si el audiosource no esta reproduciendo nada
            {
                audioS.Play();
            }
        }
        else audioS.Stop();

        //Giro del personaje
        //RotateTowards gira un vector

        /*
         * * transform.forward = la direccion hacia donde esta mirando el personaje
         * movement = la direccion en la que quiero mover el personaje
         *  turnSpeed * Time.deltaTime = velocidad a la que quiero hacer el giro
         *  0 = no quiero cambiar la magnitud del vector
         *  -------
         *  LookRotation crea un rotacion (con quaternion) hacia la direeccion (vector 3) que le meto entre ()

        */

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0);
        rotation = Quaternion.LookRotation(desiredForward);
    
    }

    private void OnAnimatorMove()
    {
        rb.MovePosition(rb.position + movement * anim.deltaPosition.magnitude);// aqui le digo al player que se mueva

        rb.MoveRotation(rotation.normalized);//AQUI LE DIGO AL PLAYER QUE ROTE
    }
}
