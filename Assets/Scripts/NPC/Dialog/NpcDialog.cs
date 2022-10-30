using System;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace NPC
{
    public class NpcDialog : NPCBAse
    {
        [SerializeField] private Transform canvas;
        [SerializeField] private CanvasGroup hint;
        private Sequence _hintFade;
        private Sequence _hintRotate;
        protected override void OnStartHint(Player player)
        {
            var playerCamera = player.GetCamera();
            _hintFade = DOTween.Sequence().Append(hint.DOFade(1, 0.2f))
                               .AppendInterval(1)
                               .Append(hint.DOFade(0, 0.2f))
                               .SetLoops(-1);
            _hintRotate = DOTween.Sequence()
                                 .SetLoops(-1)
                                 .AppendCallback(() => { canvas.transform.rotation = playerCamera.rotation; });

        }
        protected override void OnEndHint()
        {
            _hintFade.Pause();
            _hintFade.Kill();
            _hintRotate.Pause();
            _hintRotate.Kill();
            hint.alpha = 0;
        }

    }

}
