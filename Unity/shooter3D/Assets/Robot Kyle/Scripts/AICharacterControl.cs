using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        private Enemy enemy;
        public float attackRate;
        private float nextAttack;
        private GameController gameController;


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            enemy = GetComponent<Enemy>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
            
            nextAttack = Time.time + attackRate;
            attackRate = 1.95f;
            
            gameController = (GameController) GameObject.FindGameObjectWithTag("GameController").GetComponent(typeof(GameController));
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(target.position);

            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, false, false);
            else
            {
                    character.Move(Vector3.zero, false, false);
                    character.Attack();
                    var distance = Vector3.Distance(agent.transform.position, gameController.getPlayer().transform.position);
                    
                    if(Time.time > nextAttack && distance < 2)
                    {
                        nextAttack = Time.time + attackRate;
                        
                        gameController.getPlayer().isHit(5);
                    }
                    
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
