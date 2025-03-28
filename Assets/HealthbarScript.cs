using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScript : MonoBehaviour
{
    public RectTransform reactorHealthFront;
    public RectTransform reactorHealthBack;

    public ReactorScript reactor;

    private void Update()
    {
        float reacHealthFrac = reactor.getHealthFraction();
        float reactorFrontWidth = reactorHealthBack.rect.width * reacHealthFrac;
        float reactorFrontHeight = reactorHealthFront.rect.height;

        reactorHealthFront.sizeDelta = new Vector2 (reactorFrontWidth, reactorFrontHeight);
    }
}
