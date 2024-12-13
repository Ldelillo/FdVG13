using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparoJugador : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject flecha;

    private void Update()
    {  if(gameObject.CompareTag("Arquero")){
        if(Input.GetButtonDown("Fire1"))
        {
             Disparar();   
        }
       }
    }
    private void Disparar()
    {
        Instantiate(flecha, controladorDisparo.position, controladorDisparo.rotation);       
    }
}
