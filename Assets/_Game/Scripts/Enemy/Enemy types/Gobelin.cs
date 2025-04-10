using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Gobelin : Enemy
{
    protected override void Start()
    {
        MaxHealth = 150f;           // Gobelin a plus de vie
        RotationSpeed = 10f;         // Gobelin tourne plus lentement
        CurrentHealth = MaxHealth;  // Important de mettre �a apr�s avoir d�fini MaxHealth

        base.Start();               // Tu peux appeler base.Start() si tu as d'autres trucs � init
    }
}
