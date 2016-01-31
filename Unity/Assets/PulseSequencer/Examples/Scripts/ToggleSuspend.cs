using UnityEngine;
using System.Collections;
using DerelictComputer;

public class ToggleSuspend : MonoBehaviour
{
    [SerializeField] private PatternFollowerGroup _patternFollowerGroup;

    public void Toggle()
    {
        _patternFollowerGroup.Suspended = !_patternFollowerGroup.Suspended;
    }
}
