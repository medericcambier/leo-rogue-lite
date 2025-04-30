using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 10f; // Vitesse du dash
    public float dashDuration = 0.2f; // Durée du dash
    public float dashCooldown = 1f; // Temps avant de pouvoir dasher à nouveau
    public LayerMask groundLayer; // Pour détecter le sol

    private CharacterController controller;
    private bool canDash = true;

    void Start()
    {
        controller = GetComponent<CharacterController>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) 
        {
            Vector3 dashDirection = GetMouseDirection();
            if (dashDirection != Vector3.zero)
            {
                StartCoroutine(Dash(dashDirection));
            }
        }
    }

    Vector3 GetMouseDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)) 
        {
            Vector3 targetPosition = hit.point; // Position visée
            Vector3 direction = (targetPosition - transform.position).normalized; 
            direction.y = 0; 
            return direction;
        }

        return Vector3.zero; // Aucun dash si rien n'est détecté
    }

    IEnumerator Dash(Vector3 direction)
    {
        canDash = false;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
           // controller.Move(direction * dashSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(dashCooldown); // Temps de recharge
        canDash = true;
    }
}
