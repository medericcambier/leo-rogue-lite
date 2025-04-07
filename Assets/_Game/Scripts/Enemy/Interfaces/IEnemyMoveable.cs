using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMoveable 
{
    Rigidbody Rigidbody { get; set; }

    void MoveEnemy(Vector3 direction);


}
