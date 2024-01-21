using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : BaseEnemy
{
    public float dummyHp;
    [SerializeField]
    private GameObject visual;

    private float timeTillReset = 0f;
    private bool bVisualActive = true;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();

        dummyHp = hp;

        if (bVisualActive == false)
        {
            timeTillReset += Time.deltaTime;
        }
        if (timeTillReset >= 0.5f)
        {
            ResetVisual();
        }
    }

    protected override void HandleDeath()                                   // neveikia
    {
        if (hp <= 0f)
        {
            visual.SetActive(false);
            bVisualActive = false;
        }
    }

    private void ResetVisual()
    {
        hp = maxHp;
        visual.SetActive(true);
        timeTillReset = 0f;
        bVisualActive = true;
    }
}
