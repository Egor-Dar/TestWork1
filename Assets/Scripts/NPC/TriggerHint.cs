using System;
using JetBrains.Annotations;
using UnityEngine;

namespace NPC
{
    public interface ISpeaker
    {
        public void Initialize(HintStart hintStart, HintEnd hintEnd);
    }
    public class TriggerHint : MonoBehaviour, ISpeaker
    {
        private event HintStart HintStart;
        private event HintEnd HintEnd;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                HintStart?.Invoke(player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                HintEnd?.Invoke();
            }
        }
        public void Initialize(HintStart hintStart, HintEnd hintEnd)
        {
            HintStart += hintStart;
            HintEnd += hintEnd;
        }
    }
}
