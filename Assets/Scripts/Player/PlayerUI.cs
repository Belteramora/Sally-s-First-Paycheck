using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text promptText;

    [SerializeField]
    private StatusBar hpBar;

    [SerializeField]
    private Image playerDamageOverlay;

    [SerializeField]
    private float overlayShowDuration;

    [SerializeField]
    private float overlayFadeAnimationTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPlayerDamaged(float currentHP, float maxHP)
    {
        hpBar.SetValue(currentHP, maxHP);

        Sequence seq = DOTween.Sequence();
        seq.OnStart(() => 
        {
            Color c = playerDamageOverlay.color;
            c.a = 0;
            playerDamageOverlay.color = c;
            playerDamageOverlay.gameObject.SetActive(true);
        });
        seq.Append(playerDamageOverlay.DOFade(0.5f, overlayFadeAnimationTime / 2));
        seq.AppendInterval(overlayShowDuration);
        seq.Append(playerDamageOverlay.DOFade(0f, overlayFadeAnimationTime / 2));
        seq.OnComplete(() => playerDamageOverlay.gameObject.SetActive(false));
        seq.Play();
    }

    public void OnPlayerHealed(float currentHP, float maxHP)
    {
        hpBar.SetValue(currentHP, maxHP);
    }

    // Update is called once per frame
    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}
