using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

/**
 * This is the base class for our AI. All roles like Carrier inherit the default behaviour of the Villager.
 * @author Nicolas Durant
 */
public class VillagerAlternate : MonoBehaviour
{
    public NavMeshAgent agent;
    public Camera NavAgentDestinationSelectionCamera;
    public ThirdPersonCharacter character;
    private GameObject currentTarget; //tree or well that is currently the target of movement
    private int timeToLive;
    private bool isSpawned;

    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false; // the character animation will handle the rotation
    }

    // Update is called once per frame
    void Update()
    {
        if (leftMouseButtonDown() && mouseIsOnScreenOfTargetingCamera())
        {
            RaycastHit hitInfo;
            Ray ray = NavAgentDestinationSelectionCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
                agent.SetDestination(hitInfo.point);
        }
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }

    private void MoveToTarget(GameObject target)
    {
        throw new NotImplementedException();
    }

    private void StartSpawning()
    {
        throw new NotImplementedException();
    }

    private bool mouseIsOnScreenOfTargetingCamera()
    {
        return Display.RelativeMouseAt(Input.mousePosition).z == NavAgentDestinationSelectionCamera.targetDisplay;
    }

    private bool leftMouseButtonDown()
    {
        return Input.GetMouseButtonDown(0);
    }
}
