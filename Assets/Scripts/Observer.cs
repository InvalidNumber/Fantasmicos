using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public EndGame gameEnding;//referencia a la clase(script)EndGame

    bool isPlayerInRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player) isPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player) isPlayerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange)
        {
            //direccion entre el player y la gargola
            //transform.position + Vector.up lo que hace es sumar 1 en el eje y, lo hacemos
            //para que la direccion salga "desde el centro de  la gargola

            Vector3 direction = player.position - transform.position + Vector3.up;//Vector3.up - 0,1,0
            Ray ray = new Ray(transform.position, direction);//esto es igual que poner lo de ray.origin
            //= transform.position y

            //ray.direction = direction
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.transform == player)//si el raycast choca con el player
                {
                    gameEnding.CaughtPlayer();//llmao la funcion de el script endgame
                }
            }
        }
    }
}
