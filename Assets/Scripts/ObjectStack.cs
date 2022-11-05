using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStack : MonoBehaviour
{
    public static ObjectStack instance;
    private void Awake()
    {
        instance = this;
    }
    public List<Transform> objectList;
    public float carryDamp;
    public float carryDist;
    public float speed;
    public Transform stackFirstPosition;

    // Update is called once per frame
    void Update()
    {
        CarryObject();
       
    }
    
    public void CarryObject()
    {
        if (objectList.Count <= 0)
            return;
        for (int i = 0; i < objectList.Count; i++)
        {
            Transform curObject = objectList[i];
            Transform prevObject = null;
            if (i<1)
            {
                prevObject = stackFirstPosition;
            }
            else
            {
                prevObject = objectList[i - 1];
            }
            Vector3 followPos = prevObject.position + (new Vector3(0, .2f, 0)) + (prevObject.transform.up * carryDist);
            curObject.position = Vector3.Slerp(curObject.position, followPos, carryDamp * Time.deltaTime);

            Vector3 finalPos = curObject.position;
            finalPos.z = followPos.z;
            curObject.position = finalPos;
        }

        
    }
    public void AddObject(CollectableObject collects)
    {
        collects.isCollected = true;
        objectList.Add(collects.transform);
        StartCoroutine(AddObjectAnimation());

    }
    IEnumerator AddObjectAnimation()
    {
        List<Transform> tempList = objectList;
        for (int i = 0; i < tempList.Count; i++)
        {
            Transform objectToAnimate = tempList[tempList.Count - 1 - i];
            if (objectToAnimate!=null)
            {
                objectToAnimate.position = Vector3.Lerp(objectToAnimate.position, objectToAnimate.position /*+ new Vector3(0, (objectList.Count*.01f), 0)*/, Time.deltaTime * speed);
                //go.transform.parent = transform;
            }
        }
        yield return new WaitForSeconds(.04f);
    }

}
