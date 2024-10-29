using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public MovimientoJugador mov;
    public PlayerAttack ataque;
    public HealthStamina bar;
    public float vida;
    public int defensa;
    //private int nivel;
    private float experiencia;
    //private int requisitoExp;
    public Animator animaciones;
    private Collider2D hitbox;
    public int clase;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<Collider2D>();
        //nivel = 0;
        experiencia = 0;
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
    public void expUP(float experiencia)
    {
        this.experiencia = experiencia;
    }
    void cambioPrincipal()
    {
        gameObject.tag = "Player";
        SpriteRenderer cambiocolor = GetComponent<SpriteRenderer>();
        cambiocolor.color = Color.white;
        defensa = 1;
        mov.speed = 5;
        mov.jumpForce = 5;
        mov.maxJumps = 2;
        ataque.attackDamage = 10;
        clase = 1;

    }
    void cambioTanque()
    {
        gameObject.tag = "Tanque";
        SpriteRenderer cambiocolor = GetComponent<SpriteRenderer>();
        cambiocolor.color = Color.blue;
        defensa = 10;
        mov.speed = 3;
        mov.jumpForce = 2;
        mov.maxJumps = 1;
        ataque.attackDamage = 20;
        ataque.attackCooldown = 2;
        bar.attackCooldown = 1;
        clase = 2;
    }
}
