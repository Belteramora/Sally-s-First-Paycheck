using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : Interactable
{
    public BoxCollider pickupCollider;
    public Transform bulletSpawnPoint;
    public Vector3 weaponPositionInHands;
    public float shootDistance;
    public float bulletDamage;
    public float shootDelay;
    public int laserSegments;
    public float laserSegmentShowDelay;

    public LineRenderer shootLine;

    private bool shootInput;

    public void SetShoot()
    {
        shootInput = true;
    }

    public void LateUpdate()
    {
        if (shootInput)
        {
            Ray crosshairRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Shoot(crosshairRay);
            shootInput = false;
        }
    }

    public void Shoot(Ray crosshairRay)
    {
        IDamageable damageable = null;
        Vector3 point = Vector3.zero;
        if(Physics.Raycast(crosshairRay, out RaycastHit hitInfo, shootDistance))
        {
            point = hitInfo.point;
            damageable = hitInfo.collider.gameObject.GetComponent<IDamageable>();
        }
        else
        {
            point = crosshairRay.GetPoint(shootDistance);
        }

        Sequence seq = DOTween.Sequence();
        seq.OnStart(() => shootLine.gameObject.SetActive(true));
        seq.OnComplete(() => 
        {
            shootLine.gameObject.SetActive(false);
            if (damageable != null)
                damageable.GetDamage(bulletDamage);
        });

        shootLine.positionCount = 2;


        for (int i = 0; i < laserSegments - 2; i++)
        {

            var t1 = (float)i / laserSegments;
            var t2 = (float)(i+1) / laserSegments;
            seq.AppendCallback(() =>
            {
                shootLine.SetPosition(0, Vector3.Lerp(bulletSpawnPoint.position, point, t1));
                shootLine.SetPosition(1, Vector3.Lerp(bulletSpawnPoint.position, point, t2));

            });

            seq.AppendInterval(laserSegmentShowDelay);

            Debug.Log("i is " + i);
            Debug.Log("t1 is " + t1 + " t2 is " + t2);
        }

        seq.AppendCallback(() =>
        {
            shootLine.SetPosition(0, Vector3.Lerp(bulletSpawnPoint.position, point, laserSegments - 1 / laserSegments));
            shootLine.SetPosition(1, point);
        });

        seq.Play();
    }

    protected override void Interact()
    {
        base.Interact();

        PlayerCore.Instance.ChangeWeapon(this);
    }
}
