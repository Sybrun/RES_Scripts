using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{

    private string allyTag = "Selectable";
    private Transform target;

    public GameObject enemyArrowPrefab;
    public Transform firePoint;

    [Header("Attributes")]

    public float range = 30f;
    public float fireRate = 2f;
    public float fireCountdown = 1f;

    /// <summary>
    /// This Function causes UpdateTarget() to be called on a fixed rate instead of each frame.
    /// </summary>
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    /// <summary>
    /// This Function creates an array of allies within its range and looks for the ally that's the closest.
    /// </summary>
    void UpdateTarget()
    {
        GameObject[] allies = GameObject.FindGameObjectsWithTag(allyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestAlly = null;

        foreach (GameObject ally in allies)
        {
            float distanceToAlly = Vector3.Distance(transform.position, ally.transform.position);
            if (distanceToAlly < shortestDistance)
            {
                shortestDistance = distanceToAlly;
                nearestAlly = ally;
            }
        }

        if (nearestAlly != null && shortestDistance <= range)
        {
            target = nearestAlly.transform;
        }
        else
        {
            target = null;
        }

    }

    /// <summary>
    /// If there is an ally in sight, shoot at it with the fixed rate of fire.
    /// </summary>
    void Update()
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
        GameObject arrowGO = (GameObject)Instantiate(enemyArrowPrefab, firePoint.position, firePoint.rotation);
        EnemyArrow arrow = arrowGO.GetComponent<EnemyArrow>();

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
