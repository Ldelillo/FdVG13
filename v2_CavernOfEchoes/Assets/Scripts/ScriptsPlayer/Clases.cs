using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Clase",menuName ="ScriptableObjects/Clases")]
public class Clases : ScriptableObject
{
    public String nombre;
    public float speed;
    public int defensa;
    public int ataque;
    public float jumpForce;
    public int maxJumps;
    public float habCD;
    public float atqCD;

}
