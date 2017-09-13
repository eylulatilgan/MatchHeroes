using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private bool isSelected = false;
    private GameObject hitChar;
    private HeroController heroCntr;

    private static readonly int IgnoreRaycastLayerNum = 2;
    void Update()
    {
        if (HasInput)
        {
            PickUp();
        }
    }

    Vector2 CurrentTouchPosition
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void PickUp()
    {
        var inputPosition = CurrentTouchPosition;

        RaycastHit2D hit = Physics2D.Raycast(inputPosition, new Vector2(0, 0));

        //If I click on the stones
        if (hit.transform != null && hit.transform.tag == "Hero")
        {
            isSelected = true;
            hit.transform.localScale = new Vector3(1.5f, 1.5f, 0f);
            hitChar = hit.transform.gameObject;

        }

        //if I click outside
        else if(hit.transform == null || hit.transform.tag != "Hero" && hit.transform.tag != "Cell")
        {
            isSelected = false;
            if(hitChar != null)
            {
                hitChar.transform.localScale = new Vector3(1f, 1f, 0f);
            }            
        }

        //If I click on cell and the cell isnt occupied
        else if(hit.transform != null && hit.transform.tag == "Cell" && hit.transform.childCount == 0)
        {
            if(isSelected)
            {
                hitChar.transform.SetParent(hit.transform);
                hitChar.transform.position = hit.transform.position;
                hitChar.transform.localScale = new Vector3(1f, 1f, 0f);
                //I make the stone untouchable
                hitChar.gameObject.layer = IgnoreRaycastLayerNum;

                if (hitChar.gameObject.GetComponent<HeroController>() != null)
                {
                    heroCntr = hitChar.gameObject.GetComponent<HeroController>();
                    heroCntr.SnappedOnBoard = true;
                    GameEvents.TriggerCharSnapped(hitChar, hit.transform.name);
                }
            }
            isSelected = false;              
        }
    }

    private bool HasInput
    {
        get
        {
            return Input.GetMouseButton(0);
        }
    }
}
