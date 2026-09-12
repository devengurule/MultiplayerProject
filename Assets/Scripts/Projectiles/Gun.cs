using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float bulletCooldown;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject bulletPrefab;

    private bool canShoot = true;

    public void SpawnBullet()
    {
        if (canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPos.position, Quaternion.identity);

            Vector3 moveVector = new(transform.forward.x, 0, transform.forward.z);
            bullet.GetComponent<Rigidbody>().linearVelocity = moveVector * bulletVelocity;

            StartCoroutine(CooldownTimer());
        }
    }

    private IEnumerator CooldownTimer()
    {
        canShoot = false;

        yield return new WaitForSeconds(bulletCooldown);

        canShoot = true;
    }
}
