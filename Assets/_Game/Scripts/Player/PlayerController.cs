using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static SwordPickup;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Mouvement")]
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float mouseSensitivity = 100f;

    [Header("Physique")]
    public float gravity = -9.81f;
    public Transform cameraPivot;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator animator;

    private float rotationY = 0f;
    public Vector3 currentVelocity;
    public bool isGrounded;

    [Header("Armes")]
    public GameObject normalSwordPrefab;
    public GameObject fireSwordPrefab;
    public GameObject iceSwordPrefab;
    public GameObject lightningSwordPrefab;
    public SwordState currentSwordState;
    public Transform swordHolder;
    private GameObject currentSwordInstance;


    [Header("Immunités de blocage")]
    public bool fireDamageImmune = false;
    public bool iceDamageImmune = false;


    public PlayerStateMachine stateMachine { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        stateMachine = new PlayerStateMachine();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb.freezeRotation = true;

        stateMachine.Initialize(new IdleState(this, stateMachine));
    }

    void Update()
    {
        stateMachine.CurrentState?.HandleInput(); // Entrées utilisateur
        stateMachine.CurrentState?.Update();
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationY += mouseX;
        Quaternion targetRotation = Quaternion.Euler(0f, rotationY, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    public void ApplyGravity()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);
        if (!isGrounded)
        {
            rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);
        }
        else if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
    }

    public void SetActiveSwordModel(SwordType type)
    {
        // Détruire l'ancienne épée dans la scène
        if (currentSwordInstance != null)
        {
            Destroy(currentSwordInstance);
        }

        GameObject prefabToSpawn = null;

        switch (type)
        {
            case SwordType.Normal:
                prefabToSpawn = normalSwordPrefab;
                break;
            case SwordType.Fire:
                prefabToSpawn = fireSwordPrefab;
                break;
            case SwordType.Ice:
                prefabToSpawn = iceSwordPrefab;
                break;
            case SwordType.Lightning:
                prefabToSpawn = lightningSwordPrefab;
                break;
        }

        if (prefabToSpawn != null)
        {
            // Instancier l'épée dans la scène comme enfant de swordHolder
            currentSwordInstance = Instantiate(prefabToSpawn, swordHolder);
            currentSwordInstance.transform.localPosition = Vector3.zero;
            currentSwordInstance.transform.localRotation = Quaternion.identity;
            currentSwordInstance.transform.localScale = Vector3.one;
        }
    }

    public void SpawnVFX(string effectName)
    {
        // Instancier le bon prefab VFX selon effectName
    }

    public void SetSwordState(SwordState newState)
    {
        if (currentSwordState != null)
            currentSwordState.Exit();

        currentSwordState = newState;

        // Récupérer le collider dans l'épée actuelle
        if (currentSwordInstance != null)
        {
            Collider swordCollider = currentSwordInstance.GetComponentInChildren<Collider>();
            if (swordCollider != null)
            {
                currentSwordState.SetHitCollider(swordCollider);
            }
        }

        if (currentSwordState != null)
            currentSwordState.Enter();
    }

    public void HandleInput()
    {
        stateMachine.CurrentState?.HandleInput();
    }

    // Bloque les déplacements (par exemple dans AttackState)
    public void BlockMovement()
    {
        rb.velocity = Vector3.zero;
    }

    // Active la fenêtre de dégâts pour l'arme actuelle
    public void EnableDamageForCurrentWeapon()
    {
        currentSwordState?.EnableDamage(); // À adapter selon ta logique
    }

    // Vérifie si l'animation d'attaque est terminée
    public bool IsAttackAnimationFinished()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }

    // Débloque les déplacements après une attaque
    public void UnblockMovement()
    {
        // Ne rien faire ici si tu n'as pas de système de blocage automatique
        // ou tu peux gérer un flag comme playerCanMove = true;
    }

    // Désactive la hitbox de l'arme
    public void DisableSwordCollider()
    {
        currentSwordState?.DisableDamage(); // À implémenter dans chaque SwordState
    }

    // Retourne la progression de l’animation en cours
    public float GetCurrentAnimationTime(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(animationName))
        {
            return stateInfo.normalizedTime;
        }
        return 0f;
    }

    // Vérifie la fin d'une animation spécifique
    public bool IsAttackAnimationFinished(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1f;
    }
}





