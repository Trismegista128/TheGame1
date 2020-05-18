using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public Player player;

    public void OnMovement(Vector2 movement)
    {
        SetMovementAnimation((int)movement.x, (int)movement.y);
    }

    public void OnHit()
    {
        animator.SetTrigger("GotHit");
    }

    public void OnDead()
    {
        //To be implemented
    }

    public void OnImmuneAnimationFinish()
    {
        Debug.Log("On immune finished animationforplayer");
        player.ImmuneFinished();
    }

    public void OnDieAnimationFisnih()
    {
        //To be implemented
    }

    private void SetMovementAnimation(float x, float y)
    {
        var hz = animator.GetBool("Horizontal");
        var aU = animator.GetBool("Up");
        var aD = animator.GetBool("Down");

        if (hz != (x != 0)) animator.SetBool("Horizontal", x != 0);
        if (aU != (y == 1)) animator.SetBool("Up", y == 1);
        if (aD != (y == -1)) animator.SetBool("Down", y == -1);

        animator.SetFloat("MySpeed", x == 0 && y == 0 ? 0 : 1);
    }
}
