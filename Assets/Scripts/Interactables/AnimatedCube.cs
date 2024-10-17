using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class AnimatedCube : Interactable
{
    public float transitionTime;
    public float delayTime;

    private bool animationOn;
    private Vector3 originPosition;
    private Vector3 originRotation;
    private Sequence animationSequence;
    private Sequence toDefaultSequence;

    private void Start()
    {
        originPosition = transform.position;
        originRotation = transform.eulerAngles;
    }

    protected override void Interact()
    {
        base.Interact();

        animationOn = !animationOn;

        toDefaultSequence?.Kill();
        animationSequence?.Kill();

        if (animationOn)
        {
            animationSequence = DOTween.Sequence();

            var subSeq = DOTween.Sequence();
            subSeq.SetLoops(-1, LoopType.Incremental);
            subSeq.Append(transform.DOLocalRotate(Vector3.up * 15, transitionTime / 4).SetEase(Ease.Linear));

            animationSequence.Append(subSeq);
            animationSequence.Join(transform.DOMoveY(originPosition.y + 2, transitionTime));

            animationSequence.Play();
        }
        else
        {
            toDefaultSequence = DOTween.Sequence();
            //toDefaultSequence.SetLoops(1);
            //toDefaultSequence.OnPlay(() => gameObject.layer = LayerMask.NameToLayer("Default"));
            //toDefaultSequence.OnComplete(() => gameObject.layer = LayerMask.NameToLayer("Interactable"));
            toDefaultSequence.Append(transform.DOMove(originPosition, transitionTime));
            toDefaultSequence.Join(transform.DORotate(originRotation, transitionTime));
            toDefaultSequence.Play();
        }

    }
}
