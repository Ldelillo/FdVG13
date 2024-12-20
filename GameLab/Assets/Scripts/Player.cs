using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public MovimientoJugador velocidad;
    public PlayerAttack ataque;
    public float vida;
    public int defensa;
    public Animator animaciones;
    private Collider2D hitbox;
    public int clase;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<Collider2D>();
        vida = 100;
        defensa = 1;
        clase = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && clase != 1)
        {
            cambioPrincipal();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && clase != 2)
        {
            cambioTanque();
        }
    }
    public void recibirDaño(int ataqueE)
    {
        vida -= defensa >= ataqueE ? 1 : ataqueE - defensa;
        if (vida <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void cambioPrincipal()
    {
        defensa = 1;
        velocidad.speed = 5;
        ataque.attackDamage = 10;
        clase = 1;
    }
    void cambioTanque()
    {
        defensa = 10;
        velocidad.speed = 3;
        ataque.attackDamage = 20;
        clase = 2;
    }
}
