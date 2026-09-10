using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float bulletCooldown;
    [SerializeField] private Vector2 spawnOffset;
    [SerializeField] private GameObject bulletPrefab;

    private bool canShoot = true;

    public void SpawnBullet()
    {
        if (canShoot)
        {
            Vector3 spawnPos = transform.position + (transform.forward * spawnOffset.x);
            spawnPos.y += spawnOffset.y;

            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

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
