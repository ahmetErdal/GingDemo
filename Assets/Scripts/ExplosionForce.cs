using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    public float fieldOfImpact;
    public float explosionForce;
   
  

    // Update is called once per frame
    void Update()
    {
        if (TriggerController.instance.isExpo)
        {
            Explosion();
            Destroy(gameObject,.1f);
        }
    }
    public void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, fieldOfImpact);
        foreach (Collider target in colliders)
        {
            Rigidbody rb = target.GetComponent<Rigidbody>();
            if (rb!=null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, fieldOfImpact);
                StartCoroutine(OpenTrigger(target.gameObject));
            }
        }
    }
    IEnumerator OpenTrigger(GameObject go)
    {
        yield return new WaitForSeconds(.5f);
        go.GetComponent<BoxCollider>().isTrigger = true;
    }
}
