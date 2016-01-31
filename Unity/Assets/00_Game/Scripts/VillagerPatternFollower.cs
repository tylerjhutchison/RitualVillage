using UnityEngine;
using System.Collections;
using DerelictComputer;

public class VillagerPatternFollower : PatternFollower
{
    [SerializeField]
    private string _triggerName;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void OnStepTriggered(int stepIndex, double pulseTime)
    {
        if (Suspended)
        {
            return;
        }

        _animator.SetTrigger(_triggerName);
    }
}
