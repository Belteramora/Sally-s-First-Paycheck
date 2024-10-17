using DG.Tweening;
using UnityEngine;

public class DamagedState : BaseState
{
    private Material[][] originMaterials;
    public MeshRenderer[] enemyMeshes;
    public Material matToDamage;

    public DamagedState(StateMachine stateMachine, Enemy enemy, MeshRenderer[] renderers, Material matToDamage) : base(stateMachine, enemy)
    {
        enemyMeshes = renderers;
        this.matToDamage = matToDamage;

        originMaterials = new Material[renderers.Length][];
        for (int i = 0; i < renderers.Length; i++)
        {
            originMaterials[i] = renderers[i].materials;
        }
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            foreach (var meshRend in enemyMeshes)
            {
                meshRend.material = matToDamage;
            }
        });
        seq.AppendInterval(0.4f);
        seq.AppendCallback(() =>
        {
            for(int i = 0; i < enemyMeshes.Length; i++)
            {
                enemyMeshes[i].materials = originMaterials[i];
            }
        });

        seq.OnComplete(() => stateMachine.ChangeState(stateMachine.defaultState));
        
        
    }
}

