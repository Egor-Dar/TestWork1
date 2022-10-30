using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public abstract class NPCBAse : MonoBehaviour, IInteractable
    {
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] private Animator animator;
        private HintStart _hintStart;
        private HintEnd _hintEnd;
        private ISpeaker _speaker;
        protected IAnimation _animation;

        protected virtual void Start()
        {
            _animation = new Animation();
            _animation.Initialize(animator);
            _animation.Idle();
            _hintStart = OnStartHint;
            _hintEnd = OnEndHint;
            _speaker = this.AddComponent<TriggerHint>();
            _speaker.Initialize(_hintStart, _hintEnd);
        }
        public virtual void Damage()
        {
            _animation.Taking();
        }

        protected abstract void OnStartHint(Player player);

        protected abstract void OnEndHint();
    }
    public delegate void HintStart(Player player);
    public delegate void HintEnd();
}
