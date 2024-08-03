using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;


    private static readonly string runAnimation = "isRunning";
    private static readonly string fallAnimation = "isFalling";
    

    internal void PlayerRunAnim()
    {
        playerAnim.SetBool(runAnimation,true);
    }
    internal void PlayerIdleAnim()
    {
        playerAnim.SetBool(runAnimation,false);
    }
}  

