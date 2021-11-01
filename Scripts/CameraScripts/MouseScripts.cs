using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScripts : MonoBehaviour
{
    public Texture2D cursorTexture;
  //  private CursorMode mode = CursorMode.ForceSoftware;
    private Vector2 hotspot = Vector2.zero;

    public GameObject mousePoint;
    private GameObject instantiatedMouse;

    void Update()
    {
        //Cursor.SetCursor(cursorTexture,  hotspot, mode);
        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider as TerrainCollider)
                {
                    Vector3 temp = hit.point;
                    temp.y = 1.74f;

                    if(instantiatedMouse == null)
                    {
                       
                      instantiatedMouse = Instantiate(mousePoint) as GameObject;
                        instantiatedMouse.transform.position = temp;


                    }
                    else
                    {
                        
                        Destroy(instantiatedMouse);
                        instantiatedMouse = Instantiate(mousePoint) as GameObject;
                        instantiatedMouse.transform.position = temp;
                        

                    }
                }
            }
        }

     


    }
}
