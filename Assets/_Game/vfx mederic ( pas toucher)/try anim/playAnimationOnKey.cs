using UnityEngine;

public class PlayAnimationOnKey : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) // Quand on appuie sur "A"
        {
            animator.SetBool("IsPlaying", true);
        }

        if (Input.GetKeyUp(KeyCode.A)) // Quand on relâche "A"
        {
            animator.SetBool("IsPlaying", false);
        }
    }
}
