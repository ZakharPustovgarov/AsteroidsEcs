using Leopotam.Ecs;
using UnityEngine;

sealed class NavMeshStopSystem : IEcsRunSystem
{
    private readonly EcsFilter<NavMeshAgentComponent, StopTag> _stopFilter = null;
    private readonly EcsFilter<NavMeshAgentComponent, ResumeTag> _resumeFilter = null;

    public void Run()
    {
        if (_stopFilter.IsEmpty() && _resumeFilter.IsEmpty()) return;

        foreach (var i in _resumeFilter)
        {
            ref var agent = ref _resumeFilter.Get1(i).Agent;
            _resumeFilter.GetEntity(i).Del<ResumeTag>();
            if (!agent.enabled) continue;
            agent.isStopped = false;
            //_resumeFilter.GetEntity(i).Del<ResumeTag>();
            //Debug.Log(agent.name + " resuming move to point " + agent.destination);
        }

        foreach(var i in _stopFilter)
        {
            ref var agent = ref _stopFilter.Get1(i).Agent;
            _stopFilter.GetEntity(i).Del<StopTag>();
            if (!agent.enabled) continue;
            agent.isStopped = true;
            //_stopFilter.GetEntity(i).Del<StopTag>();
        }
    }
}

