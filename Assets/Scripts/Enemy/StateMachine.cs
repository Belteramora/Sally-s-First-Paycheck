using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine: MonoBehaviour
{
    private MeshRenderer[] renderers;
    public Material damageMaterial;

    public BaseState activeState;

    public BaseState defaultState;

    public PatrolState patrolState;
    public DamagedState damagedState;

    public void Initialise(Enemy enemy)
    {
        patrolState = new PatrolState(this, enemy);
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        damagedState = new DamagedState(this, enemy, meshRenderers, damageMaterial);

        activeState = patrolState;
        defaultState = patrolState;
    }

    private void Update()
    {
        if(activeState != null)
        {
            
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if(activeState != null)
        {
            activeState.Exit();
        }

        activeState = newState;

        if(activeState != null)
        {
            activeState.Enter();
        }
    }
}

