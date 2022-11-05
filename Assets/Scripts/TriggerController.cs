using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public bool isExpo;
    public static TriggerController instance;
    public int stackTrigger;
    public GameObject stackObject;
    public int spawnObjectCount;
    public GameObject expoObject;
    public Transform spawnPos;
    private void Awake()
    {
        instance = this;
        isExpo = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExpoObject"))
        {
            Debug.Log("expop");
            other.GetComponent<BoxCollider>().enabled = false;
         
            Vector3 center = spawnPos.position;
            for (int i = 0; i < spawnObjectCount; i++)
            {
                Vector3 pos = RandomCircle(center, 2.0f);
                Instantiate(stackObject, pos, Quaternion.identity);
            }

                StartCoroutine(StartExpo());
        }
        else if (other.CompareTag("ColPlayer"))
        {
            Debug.Log("playerToplama");

            AIPlayerController aIPlayer = other.GetComponent<AIPlayerController>();
            if (aIPlayer.isFollowToMainPlayer)
            {
                return;
            }
            HumanStack.instance.AddHuman(aIPlayer);
        }
        if (other.CompareTag("Collactable"))
        {
            if (isExpo)
            {
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.GetComponent<Rigidbody>().useGravity = false;
                CollectableObject collectable = other.GetComponent<CollectableObject>();
                if (collectable.isCollected)
                    return;
                ObjectStack.instance.AddObject(collectable);
            }
           
        }
    }
    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y ;
        pos.z = center.z+ radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
   
    IEnumerator StartExpo()
    {
        yield return new WaitForSeconds(1f);
        isExpo = true;
    }
}
