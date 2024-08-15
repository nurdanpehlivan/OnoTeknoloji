using UnityEngine;

public class RotatePanel : MonoBehaviour
{
    private Animator animator;
    private bool isRotating = false;

    void Start()
    {
        // Animator bileþenini al
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(animator != null)
        {
        // 'a' tuþuna basýldýðýnda döndürme iþlemini baþlat
            if (Input.GetKeyDown(KeyCode.A))
             {
            animator.SetTrigger("Rotate");
             }
        }
    }
}
