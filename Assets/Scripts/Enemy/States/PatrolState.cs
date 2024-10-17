using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PatrolState : BaseState
{
    public int waypointIndex;
    
    public float waitTimer = 0;

    public PatrolState(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
    {
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        PatrolCycle();
    }

    private void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer > 3)
            {
                waitTimer = 0;

                if (waypointIndex < enemy.path.waypoints.Count - 1)
                    waypointIndex++;
                else
                    waypointIndex = 0;

                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
            }
        }
    }
}

