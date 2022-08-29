using Leopotam.Ecs;
using UnityEngine;

sealed class NavMeshApproachSystem : IEcsRunSystem
{
    private readonly EcsFilter<NavMeshAgentComponent, ApproachComponent> _agentsFilter = null;

    public void Run()
    {
        //Debug.LogWarning("Approaching count: " + _agentsFilter.GetEntitiesCount());
        if (_agentsFilter.IsEmpty()) return;

        foreach(var i in _agentsFilter)
        {
            ref var agent = ref _agentsFilter.Get1(i).Agent;
            ref var distance = ref _agentsFilter.Get2(i).ApproachDistance;
            ref var entity = ref _agentsFilter.GetEntity(i);

            if (Vector3.Distance(agent.transform.position, agent.destination) <= distance)
            {
                //Debug.LogWarning("Approached: " + agent.name);
                agent.destination = agent.transform.position;
                entity.Del<ApproachComponent>();
                entity.Get<ApproachedEvent>();
            }
        }
    }
}
