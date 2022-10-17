using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerGameObject : MonoBehaviour
{
    public int Type = 0; // 1: RAT, 2: FERRET, 3: TIGER
    public int parentIdx = -1;
    private bool isReturn = false;
    private float AnimationSpeed;
    private bool clickChecker = false;

    // Start is called before the first frame update
    void Start()
    {
        AnimationSpeed = MNG_HAMMERGAME.instance.DefaultAnimationSpeed;
        //AnimationSpeed *= MNG_HAMMERGAME.instance.DifficultyModifier;
        AnimationSpeed *= (this.Type == 2) ? MNG_HAMMERGAME.instance.SpeedModifier : 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!MNG_HAMMERGAME.instance.gameStart) Destroy(this.gameObject);

        Vector3 localPos = this.gameObject.transform.localPosition;
        if (localPos.y >= 80)
        {
            isReturn = true;
        }
        if (localPos.y < -101)
        {
            isReturn = false;
            Destroy(this.gameObject);
        }

        if (!isReturn)
        {
            this.gameObject.transform.localPosition = new Vector3(localPos.x, localPos.y + AnimationSpeed * Time.deltaTime, localPos.z);
        }
        else
        {
            this.gameObject.transform.localPosition = new Vector3(localPos.x, localPos.y - AnimationSpeed * Time.deltaTime, localPos.z);
        }
    }

    private void OnMouseDown()
    {
        this.clickChecker = true;
        //Debug.Log(this.AnimationSpeed);
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        MNG_HAMMERGAME.instance.ProcessObject(this.Type, this.parentIdx, this.clickChecker);
    }
}
