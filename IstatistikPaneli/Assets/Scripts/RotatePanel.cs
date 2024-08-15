using UnityEngine;

public class RotatePanel : MonoBehaviour
{
    private Animator animator;
    private bool isRotating = false;

    void Start()
    {
        // Animator bile�enini al
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(animator != null)
        {
        // 'a' tu�una bas�ld���nda d�nd�rme i�lemini ba�lat
            if (Input.GetKeyDown(KeyCode.A))
             {
            animator.SetTrigger("Rotate");
             }
        }
    }
}
