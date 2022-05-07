using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    
    public float targetRange = 15f;
    public float fireRate = 0.5f;
    public float nextFire = 0.0f;
    
    [Header("Unity Setup Fields")]
    
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float rotateSpeed = 10f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget",0f,0.5f);
    }

    void UpdateTarget(){
        // gets all the targets with enemy tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy<shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= targetRange)
        {
            target = nearestEnemy.transform;
        }else{
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        
        // target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);

        // Checking fireRate then firing
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }

    }

    private void Shoot()
    {
        GameObject ArrowGO = (GameObject)Instantiate(projectilePrefab,firePoint.position,firePoint.rotation);
        Arrow arrow = ArrowGO.GetComponent<Arrow>();
        if (arrow != null)
            arrow.Seek(target);

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position,targetRange);
    }
}
