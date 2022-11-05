using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{
    public bool isFollowToMainPlayer;
    //[HideInInspector] public GameObject target;
    
    //private void Awake()
    //{
    //    isFollowToMainPlayer = false;
    //}
    //private void Update()
    //{
        
    //}
    //private void FixedUpdate()
    //{
    //    if (isFollowToMainPlayer)
    //    {
            
    //        FollowToMainPlayer(target);
           
    //    }
    //}

    //public void FollowToMainPlayer(GameObject target)
    //{
       
    //    transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime);
    //    //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, .03f);
    //}
}
