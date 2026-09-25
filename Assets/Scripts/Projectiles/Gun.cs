using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private float bulletVelocity;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Image fullAutoFillBar;

    private int currentFullAutoBullets;
    private bool canShoot = true;
    public bool fullAuto {  get; private set; }
    
    private void OnEnable() => GetComponent<InputHandler>().fireGun += SpawnBullet;
    private void OnDisable() => GetComponent<InputHandler>().fireGun -= SpawnBullet;

    public void SpawnBullet()
    {
        if (GameController.instance.isPaused) return;

        if (canShoot)
        {
            if (fullAuto)
            {
                currentFullAutoBullets--;
                fullAutoFillBar.fillAmount = currentFullAutoBullets / (float)GameController.instance.fullAutoBullets;
                if (fullAutoFillBar.fillAmount == 0) TurnOffFullAuto();
            }

            GameObject bullet = Instantiate(bulletPrefab, spawnPos.position, Quaternion.identity);

            Vector3 moveVector = new(transform.forward.x, 0, transform.forward.z);
            bullet.GetComponent<Rigidbody>().linearVelocity = moveVector * bulletVelocity;

            StartCoroutine(CooldownTimer());
        }
    }

    private IEnumerator CooldownTimer()
    {
        canShoot = false;

        if(!fullAuto) yield return new WaitForSeconds(GameController.instance.bulletCooldown);
        if(fullAuto) yield return new WaitForSeconds(GameController.instance.fullAutoCooldown);

        canShoot = true;
    }

    private void TurnOffFullAuto()
    {
        fullAuto = false;
    }

    public void TurnOnFullAuto()
    {
        currentFullAutoBullets = GameController.instance.fullAutoBullets;
        fullAutoFillBar.fillAmount = 1f;
        fullAuto = true;
    }
    
}
