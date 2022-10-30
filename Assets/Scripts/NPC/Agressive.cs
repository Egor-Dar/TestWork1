using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Sequence = DG.Tweening.Sequence;


namespace NPC
{
    public class Agressive : NPCBAse
    {
        [SerializeField] private float rotationSpeed = 3f;
        private Transform _chaseСharacter;
        private bool _isTrigger;

        private void Update()
        {
            if (_isTrigger)
            {
                _animation.Run();
                agent.SetDestination(_chaseСharacter.position);
                var turnTowardNavSteeringTarget = agent.steeringTarget;

                Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
            if (agent.remainingDistance<= agent.stoppingDistance && _isTrigger)
            {
                _animation.Attack();
            }
            else if (agent.pathStatus == NavMeshPathStatus.PathComplete && !_isTrigger)
            {
                _animation.Idle();
            }
            else
            {
                _animation.Run();
            }
        }

        protected override void OnStartHint(Player player)
        {
            _isTrigger = true;
            _chaseСharacter = player.transform;
        }
        protected override void OnEndHint()
        {
            _isTrigger = false;
            agent.SetDestination(transform.position);
        }

    }

}
