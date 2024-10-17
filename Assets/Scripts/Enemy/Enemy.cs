using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;

    public NavMeshAgent Agent { get => agent; }

    //Debug
    [SerializeField]
    private string currentState;

    public WaypointsPath path;
    public float hitPoints = 100f;
    public float sightDistance = 20f;
    public float fieldOfView = 85;
    public float eyeHeight;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialise(this);

        player = PlayerCore.Instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if(stateMachine.activeState != null)
            currentState = stateMachine.activeState.GetType().Name;
#endif
        CanSeePlayer();
    }

    public bool CanSeePlayer()
    {
        if (player == null) return false;

        if (Vector3.Distance(transform.position, player.transform.position) >= sightDistance) return false;

        Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
        float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

        if (angleToPlayer < -fieldOfView || angleToPlayer > fieldOfView) return false;

        Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);

        if(Physics.Raycast(ray, out RaycastHit hitInfo, sightDistance))
            if (hitInfo.transform.gameObject != player) return false;

#if UNITY_EDITOR
        Debug.DrawRay(ray.origin, ray.direction * sightDistance);
#endif

        return true;
    }

    public void GetDamage(float damage)
    {
        hitPoints -= damage;

        stateMachine.ChangeState(stateMachine.damagedState);

        if (hitPoints <= 0) Destroy(gameObject, 0.01f);
    }
}
