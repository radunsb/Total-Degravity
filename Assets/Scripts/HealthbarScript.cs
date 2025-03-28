using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScript : MonoBehaviour
{
    public RectTransform reactorHealthFront;
    public RectTransform reactorHealthBack;

    public RectTransform shieldHealthFront;
    public RectTransform shieldHealthBack;

    public ReactorScript reactor;
    public ShieldScript shield;

    private void Update()
    {
        float reacHealthFrac = reactor.getHealthFraction();
        float reactorFrontWidth = reactorHealthBack.rect.width * reacHealthFrac;
        float reactorFrontHeight = reactorHealthFront.rect.height;

        reactorHealthFront.sizeDelta = new Vector2 (reactorFrontWidth, reactorFrontHeight);

        float shieldHealthFrac = shield.getHealthFraction();
        float shieldFrontWidth = shieldHealthBack.rect.width * shieldHealthFrac;
        float shieldFrontHeight = shieldHealthFront.rect.height;

        shieldHealthFront.sizeDelta = new Vector2(shieldFrontWidth, shieldFrontHeight);
    }
}
