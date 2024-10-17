using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DummyTarget : MonoBehaviour, IDamageable
{
    private MeshRenderer _meshRenderer;
    private Material[] originMaterials;
    private Sequence seq;
    private float accumulatedDamage = 0f;

    public Material damageMaterial;
    public TMP_Text targetText;

    public float showDelay;


    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        originMaterials = _meshRenderer.materials;
    }

    //public void Update()
    //{
    //    Debug.Log("is seq killed = " + seq.IsActive());
    //}

    public void GetDamage(float damageAmount)
    {
        if (seq.IsActive())
        {
            seq.Kill();
        }

        seq = DOTween.Sequence();

        seq.SetAutoKill(true);

        accumulatedDamage += damageAmount;

        seq.OnComplete(() =>
        {
            accumulatedDamage = 0;
            targetText.text = accumulatedDamage.ToString();
            _meshRenderer.materials = originMaterials;
        });

        seq.AppendCallback(() => _meshRenderer.material = damageMaterial);
        seq.JoinCallback(() => targetText.text = accumulatedDamage.ToString());
        seq.AppendInterval(showDelay);

        seq.Play();

    }
}
