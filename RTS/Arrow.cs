using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Transform target;

    public float speed = 70f;
    public float damage = 50f;

    /// <summary>
    /// Seeks and sets the target to the Transform of _target.
    /// </summary>
    public void Seek(Transform _target)
    {
        target = _target;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	/// <summary>
    /// Checks if the target is null. If so, it will destroy itself and return. If the target is not null,
    /// the arrow will fly towards the target with the pace that is pre-determined with the speed variable.
    /// Furthermore, will it check if the distance is equal to the Vector3 dir. magnitude. If its is, 
    /// the function will activate the HitTarget function.
    /// </summary>
	void Update () {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		
	}

    /// <summary>
    /// Causes the target to get damage, which is a public float that can be adjusted if desired 
    /// (Mainly for balancing purposes). And destroys the arrow after impact.
    /// </summary>
    public void HitTarget()
    {
        target.GetComponent<EnemyHealth>().health -= damage;
        Destroy(gameObject);
    }
}
