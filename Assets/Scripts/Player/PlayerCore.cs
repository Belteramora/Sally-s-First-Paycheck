using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore Instance { get; private set; }

    public Camera cam;
    public Weapon currentWeapon;
    public float pickupTime;

    public Transform hand;

    [HideInInspector]
    public PlayerMotor pMotor;
    [HideInInspector]
    public PlayerStatus pStatus;
    [HideInInspector]
    public PlayerUI pUI;
    [HideInInspector]
    public PlayerInteract pInteract;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        pMotor = GetComponent<PlayerMotor>();
        pStatus = GetComponent<PlayerStatus>();
        pUI = GetComponent<PlayerUI>();
        pInteract = GetComponent<PlayerInteract>();

        currentWeapon.pickupCollider.enabled = false;
    }

    public void Shoot(InputAction.CallbackContext context)
    {

        currentWeapon.SetShoot();
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        var oldWeapon = currentWeapon;
        newWeapon.transform.SetParent(oldWeapon.transform.parent);
        oldWeapon.transform.SetParent(null);

        var newWPos = newWeapon.transform.position;
        var newWRot = newWeapon.transform.eulerAngles;


        Sequence seq = DOTween.Sequence();
        seq.OnComplete(() => 
        {
            oldWeapon.pickupCollider.enabled = true;
            currentWeapon = newWeapon;
            newWeapon.pickupCollider.enabled = false;
        });
        seq.Append(oldWeapon.transform.DOMove(newWPos, pickupTime));
        seq.Join(oldWeapon.transform.DORotate(newWRot, pickupTime));
        seq.Join(newWeapon.transform.DOLocalMove(newWeapon.weaponPositionInHands, pickupTime));
        seq.Join(newWeapon.transform.DOLocalRotate(Vector3.zero, pickupTime));
        seq.Join(hand.DOLocalMoveX(newWeapon.weaponPositionInHands.x, pickupTime));
        seq.Play();
    }
}
