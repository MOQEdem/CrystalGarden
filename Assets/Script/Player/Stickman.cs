using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Stickman : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchAnimation(bool isRunning)
    {
        _animator.SetBool(AnimatorStickman.Bool.IsRunning, isRunning);
    }

    public static class AnimatorStickman
    {
        public static class Bool
        {
            public const string IsRunning = nameof(IsRunning);
        }
    }
}
