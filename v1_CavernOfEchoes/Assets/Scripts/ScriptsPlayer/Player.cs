using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Clases principal;
    [SerializeField] private Clases tanque;
    [SerializeField] private Clases arquero;
    public Clases actual;
    public MovimientoJugador mov;
    public PlayerAttack ataque;
    public HealthStamina bar;
    public float vida;
    private Collider2D hitbox;
    public int clase;
    public Boolean habInUse;
    private Animator animacion;

    // Start is called before the first frame update
    void Start()
    {
        habInUse = false;
        hitbox = GetComponent<Collider2D>();
        animacion = GetComponent<Animator>();
        vida = 100;
        actual = principal;
        updateStats();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && actual.nombre != "Player" && !habInUse)
        {
            actual = principal;
            updateStats();
            animacion.SetInteger("Change",1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && actual.nombre != "Tanque" && !habInUse)
        {
            actual = tanque;
            updateStats();
            animacion.SetInteger("Change",2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && actual.nombre != "Arquero" && !habInUse)
        {
            actual = arquero;
            updateStats();
        }
        if(!mov.isGrounded){
            animacion.SetBool("Salto",true);
        }
        else{
            animacion.SetBool("Salto",false);
        }
        animacion.SetFloat("Moving",Mathf.Abs(mov.horizontal));
        if(mov.horizontal>0){
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (mov.horizontal<0){
            transform.localScale = new Vector3(-1, 1, 1);
        }
        animacion.SetInteger("Change",0);
        
    }
    public void recibirDaño(int ataqueE)
    {
        vida -= actual.defensa >= ataqueE ? 1 : ataqueE - actual.defensa;
        if (vida <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void updateStats(){
        gameObject.tag = actual.nombre;
        //Variable de movilidad
        mov.speed = actual.speed;
        mov.jumpForce = actual.jumpForce;
        mov.maxJumps = actual.maxJumps;
        //Variables de combate
        ataque.attackDamage = actual.ataque;
        ataque.attackCooldown = actual.atqCD;
        //Animator change
        //habilidad = 
    }
    /*
    void cambioPrincipal()
    {
        gameObject.tag = "Player";
        SpriteRenderer cambiocolor = GetComponent<SpriteRenderer>();
        cambiocolor.color = Color.white;
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
        mov.speed = 3;
        mov.jumpForce = 2;
        mov.maxJumps = 1;
        ataque.attackDamage = 20;
        ataque.attackCooldown = 2;
        bar.attackCooldown = 1;
        clase = 2;
    }
    */

}
