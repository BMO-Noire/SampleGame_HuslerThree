using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    const float MOUSE_THRESHOLD = 1f;

    public int spriteType = -1;
    public int specialType = 0; // 0:normal, 1:vertical, 2:horizontal
    public int posX = 0;
    public int posY = 0;
    private const int POS_PRESET = MNG_ANIPANGGAME.POS_PRESET*-1;

    private Vector3 startMousePoint;
    
    private GameObject[,] Field;
    // Start is called before the first frame update
    void Start()
    {
        Field = GameObject.Find("MotherObject").GetComponent<MNG_ANIPANGGAME>().Field;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        startMousePoint = Input.mousePosition;
    }
    private void OnMouseUp()
    {
        Vector3 endMousePoint = Input.mousePosition;
        float deltaX = Mathf.Abs(endMousePoint.x - startMousePoint.x);
        float deltaY = Mathf.Abs(endMousePoint.y - startMousePoint.y);
        if (deltaX > deltaY)
        {
            if (deltaX <= MOUSE_THRESHOLD) return;

            if (startMousePoint.x > endMousePoint.x)
            {
                LeftCheck();
            }
            else if (startMousePoint.x < endMousePoint.x)
            {
                RightCheck();
            }
        }
        else
        {
            if (deltaY <= MOUSE_THRESHOLD) return;

            if (startMousePoint.y > endMousePoint.y)
            {
                DownCheck();
            }
            else if (startMousePoint.y < endMousePoint.y)
            {
                UpCheck();
            }
        }
    }
    private void OnMouseDrag()
    {
        //Debug.Log("onMouseDrag");
    }
    void LeftCheck() {
        this.gameObject.transform.parent.GetComponent<MNG_ANIPANGGAME>().MatchCheck(this.gameObject, MNG_ANIPANGGAME.LEFT);
    }
    void RightCheck() {
        this.gameObject.transform.parent.GetComponent<MNG_ANIPANGGAME>().MatchCheck(this.gameObject, MNG_ANIPANGGAME.RIGHT);
    }
    void UpCheck() {
        this.gameObject.transform.parent.GetComponent<MNG_ANIPANGGAME>().MatchCheck(this.gameObject, MNG_ANIPANGGAME.UP);
    }
    void DownCheck() {
        this.gameObject.transform.parent.GetComponent<MNG_ANIPANGGAME>().MatchCheck(this.gameObject, MNG_ANIPANGGAME.DOWN);
    }
}
