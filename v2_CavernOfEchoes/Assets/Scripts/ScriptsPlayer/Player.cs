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
    private bool puedeCambiarAArquero = false;
    private bool puedeCambiarATanque = false;

    public int mirandoHacia;

    // Start is called before the first frame update
    void Start()
    {
        habInUse = false;
        hitbox = GetComponent<Collider2D>();
        animacion = GetComponent<Animator>();
        vida = 100;
        actual = principal;
        mirandoHacia = 1;
        updateStats();
    }

    void Update()
    {
        animacion.SetInteger("Change",0);
        if (Input.GetKeyDown(KeyCode.Alpha1) && actual.nombre != "Player" && !habInUse)
        {
            actual = principal;
            animacion.SetInteger("Change",1);
            updateStats();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && actual.nombre != "Tanque" && !habInUse && puedeCambiarATanque)
        {
            actual = tanque;
            animacion.SetInteger("Change",2);
            updateStats();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && actual.nombre != "Arquero" && !habInUse && puedeCambiarAArquero)
        {
            actual = arquero;
            animacion.SetInteger("Change",3);
            updateStats();
        }
        if(!mov.isGrounded){
            animacion.SetBool("Salto",true);
        }
        else{
            animacion.SetBool("Salto",false);
        }
        animacion.SetFloat("Moving",Mathf.Abs(mov.horizontal));
        if(mov.horizontal>0 && mirandoHacia < 0){
           rotar();
        }
        else if (mov.horizontal<0 && mirandoHacia > 0){
            rotar();
        }
        
        
    }

    // Nuevo método para desactivar el movimiento al colisionar con la rampa
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobamos si la colisión es con un objeto etiquetado como "Rampa"
        if (collision.gameObject.CompareTag("Rampa"))
        {
            // Desactivamos el movimiento
            mov.enabled = false;
            // Debug.Log("Movimiento desactivado al colisionar con la rampa");
        }
    }

    // Reactivar el movimiento cuando el jugador sale de la rampa
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Comprobamos si estamos saliendo de una colisión con un objeto etiquetado como "Rampa"
        if (collision.gameObject.CompareTag("Rampa"))
        {
            // Reactivamos el movimiento
            mov.enabled = true;
            // Debug.Log("Movimiento reactivado al salir de la rampa");
        }
    }

    private void rotar(){
        mirandoHacia *= -1;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180,0);
    }
    public void recibirDaño(int ataqueE)
    {
        vida -= actual.defensa >= ataqueE ? 1 : ataqueE - actual.defensa;
        if (vida <= 0)
        {
            (FindObjectOfType<SistemaPausa>()).GameOverFun();
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

    public void HabilitarTanque()
    {
        puedeCambiarATanque = true;
        Debug.Log("Ahora puedes cambiar a tanque");
    }

    public void HabilitarArquero()
    {
        puedeCambiarAArquero = true;
        Debug.Log("Ahora puedes cambiar a arquero");
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
