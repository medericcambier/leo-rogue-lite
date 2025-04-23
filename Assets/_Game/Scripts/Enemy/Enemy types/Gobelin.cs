using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Gobelin : Enemy
{
    protected override void Start()
    {
        MaxHealth = 150f;           
        RotationSpeed = 10f;         
        CurrentHealth = MaxHealth;  

        base.Start();               // Tu peux appeler base.Start() si tu as d'autres trucs à init
    }
}
