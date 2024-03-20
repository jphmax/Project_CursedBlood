using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public Transform target;
    public float engageDistance_approach = 1f;
    public float engageDistance_encounter = 1f;
    public float engageDistance_attack = 1f;
    public Material material;
    private NavMeshAgent agent; private Renderer rend; 
    private float encountertime_state = 0f; private float encountertime = 0f; 
    private float attacktime_state = 0f; private float attacktime = 0f;
    private int state = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            material = rend.material;
            material.color = Color.red;
        }
        encountertime_state = Random.Range(0.2f, 0.5f);
        InvokeRepeating("UpdateMethod", 0, 0.25f);
    }

    void UpdateMethod()
    {
        if (engageDistance_approach >= engageDistance_encounter || engageDistance_encounter >= engageDistance_attack) {
            if (Vector3.Distance(target.position, transform.position) > engageDistance_approach) {
                SetDestinationWithOffset(engageDistance_approach); material.color = Color.red; agent.speed = 3;
            }
            else if (Vector3.Distance(target.position, transform.position) > engageDistance_encounter)
            {
                // Do Waiting For Attack State
                /*if (encountertime == 0f)
                {
                    SetDestinationWithOffset(engageDistance_encounter + Random.Range(engageDistance_attack + 0.125f, engageDistance_encounter - 0.125f));
                }
                else if (encountertime >= encountertime_state)
                {
                    encountertime_state = Random.Range(0.2f, 0.5f); encountertime = 0f;
                }
                */
                material.color = Color.yellow; agent.speed = 1; encountertime += Time.deltaTime;
            }
            else if (Vector3.Distance(target.position, transform.position) > engageDistance_attack) { material.color = Color.blue; }
        }
    }
    void SetDestinationWithOffset(float distance) // Off Function
    {
        Vector3 targetPosition = target.position + (transform.position - target.position).normalized * distance;
        agent.SetDestination(targetPosition);
    }
}
