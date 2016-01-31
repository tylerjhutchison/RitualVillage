using UnityEngine;
using System.Collections;
using DerelictComputer;

public class VillagerPatternFollower : PatternFollower
{

    private VillagerAnimation anim;

    private void Start()
    {
         anim = GetComponent<VillagerAnimation>();
    }

    protected override void OnStepTriggered(int stepIndex, double pulseTime)
    {
        if (Suspended)
        {
            return;
        }

        //anim.Dance();
        anim.MusicReact();
    }
}
