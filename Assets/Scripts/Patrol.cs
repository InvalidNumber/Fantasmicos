using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;//puntos de patrulla del fantasma
    NavMeshAgent agent;

    int currentWaypoint;//variable para controlar el waypoint en el que nos encontramos

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //remainingDistance me devuelve la distancia que hay entre el agente y el destino en el path actual
        //si la distancia que hay entre el agente y el destino es menor/igual que la distancia en la que se va a parar se mete en el if

        if(agent.remainingDistance < agent.stoppingDistance)
        {
            //si estoy en la ultima posicion del array vuelvo al comiezo,
            //sino sumo 1 para pasar al siguiente casilla
            if (currentWaypoint == waypoints.Length - 1) currentWaypoint = 0;
            else currentWaypoint++;

            //le indico con la variable "currentwaypoint" que se mueve a la posicion
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }
}
