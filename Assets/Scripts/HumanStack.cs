using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanStack : MonoBehaviour
{
    public static HumanStack instance;
    private void Awake()
    {
        instance = this;
    }
    public List<Transform> humanList;
    public float humanCarryDamp;
    public float humanCarryDist;
    public float humanCarrySpeed;
   
    // Update is called once per frame
    void Update()
    {
        CarryHuman();
    }
    public void CarryHuman()
    {
        if (humanList.Count <= 0)
            return;
        for (int i = 0; i < humanList.Count; i++)
        {
            Transform curHuman = humanList[i];
            Transform prevHuman = null;
            if (i < 1)
                prevHuman = transform;
            else
                prevHuman = humanList[i - 1];

            Vector3 followPos = prevHuman.position - (prevHuman.transform.forward * humanCarryDist);
            curHuman.position = Vector3.Slerp(curHuman.position, followPos, humanCarryDamp * Time.deltaTime);

            Vector3 finalPos = curHuman.position;
            finalPos.z = followPos.z;
            curHuman.position = finalPos;

        }

        
    }
   
    public void AddHuman(AIPlayerController AI)
    {
        AI.isFollowToMainPlayer = true;
        humanList.Add(AI.transform);
        StartCoroutine(AddHumanAnimation());
    }
    IEnumerator AddHumanAnimation()
    {
        List<Transform> tempList = humanList;
        for (int i = 0; i < tempList.Count; i++)
        {
            Transform humanToAnimate = humanList[humanList.Count - 1 - i];
            if (humanToAnimate!=null)
            {
                humanToAnimate.position = Vector3.Lerp(humanToAnimate.position, humanToAnimate.position - new Vector3(0, 0, .5f), Time.deltaTime * humanCarrySpeed);
            }
        }
        yield return new WaitForSeconds(.04f);
    }

}
