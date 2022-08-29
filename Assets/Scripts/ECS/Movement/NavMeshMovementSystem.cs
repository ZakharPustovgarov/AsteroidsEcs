using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

sealed class NavMeshMovementSystem : IEcsRunSystem
{
    private readonly EcsFilter<NavMeshAgentComponent, NavMeshDestinationComponent> _agentsFilter = null;
    //private readonly EcsFilter<GameFinishedEvent> _endGameEvent = null;

    private bool _blocked = false;

    private struct DelayedMovement
    {
        public NavMeshAgent Agent;
        public Vector3 Destination;

        public DelayedMovement(NavMeshAgent Agent, Vector3 Destination)
        {
            this.Agent = Agent;
            this.Destination = Destination;
        }
    }
    private List<DelayedMovement> _delayed = new List<DelayedMovement>();

    public void Run()
    {
        //Debug.Log("Active agents count: " + _agentsFilter.GetEntitiesCount());
        if(_blocked || _agentsFilter.IsEmpty()) return;

        foreach(var i in _agentsFilter)
        {
            ref var agent = ref _agentsFilter.Get1(i);

            ref var direction = ref _agentsFilter.Get2(i);

            if(!agent.Agent.enabled)
            {
                _delayed.Add(new DelayedMovement(agent.Agent, direction.Destination));
                continue;
            }

            if(_delayed.Count > 0)
            {
                for(int j = 0; j < _delayed.Count;)
                {
                    if (_delayed[j].Agent == agent.Agent)
                    {
                        agent.Agent.SetDestination(_delayed[j].Destination);
                        _delayed.RemoveAt(j);
                    }
                    else j++;
                }
            }

            agent.Agent.SetDestination(direction.Destination);
            _agentsFilter.GetEntity(i).Del<NavMeshDestinationComponent>();
            //Debug.Log(agent.Agent.name + " going to " + agent.Agent.destination);
        }
    }
}
