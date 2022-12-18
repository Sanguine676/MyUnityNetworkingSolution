using UnityEngine;
//

//
public static class Animator_ExtentionMethods
{
    //
    public static void SwitchAnimationIndex(this Animator animator, int newIndex)
    {
        animator.SetInteger("animationIndex", newIndex);
    }

    //
    public static bool StateNameIs(this Animator animator, int layerIndex, string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
    }

    //
    public static void ResetAllTriggers(this Animator animator)
    {
        AnimatorControllerParameter[] animatorParameterArr = animator.parameters;
        for (int i = 0; i < animatorParameterArr.Length; ++i)
            if (animatorParameterArr[i].type == AnimatorControllerParameterType.Trigger)
                animator.ResetTrigger(animatorParameterArr[i].name);
    }

    //
    public static AnimationClip CurFirstClip(this Animator animator, int layerIndex)
    {
        return animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip;
    }

    //
    public static float CurFirstClipLength(this Animator animator, int layerIndex)
    {
        return animator.CurFirstClip(layerIndex).length;
    }
}
