using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedShooting : MonoBehaviour {

    private string enemyTag = "Enemy";
    private Transform target;

    public GameObject arrowPrefab;
    public Transform firePoint;

    [Header ("Attributes")]

    public float range = 15f;
    public float fireRate = 2f;
    public float fireCountdown = 1f;

	/// <summary>
    /// This Function causes UpdateTarget() to be called on a fixed rate instead of each frame.
    /// </summary>
	void Start ()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    /// <summary>
    /// This Function creates an array of enemies within its range and looks for the target that's the closest.
    /// </summary>
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }
	
	/// <summary>
    /// If there is an enemy in sight, shoot at it with the fixed rate of fire.
    /// </summary>
	void Update ()
    {
        if (target == null)
            return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

	}

    /// <summary>
    /// This Function produces Arrows and seeks for its nearest by target.
    /// </summary>
    public void Shoot()
    {
        GameObject arrowGO = (GameObject)Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Arrow arrow = arrowGO.GetComponent<Arrow>();

        if (arrow != null)
            arrow.Seek(target);
    }

    /// <summary>
    /// Creates red Gizmos when selected, to visualise its range.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
