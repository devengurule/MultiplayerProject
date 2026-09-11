using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private Light bulletLight;
    private Material bulletMaterial;

    private void Start()
    {
        bulletMaterial = GetComponent<Renderer>().material;

        var particleSystemMain = deathParticles.main;

        particleSystemMain.startColor = bulletMaterial.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        string collisionTag = other.gameObject.tag;

        switch (collisionTag)
        {
            case "Wall":

                DestroySequence();

                break;
            case "Player":

                DestroySequence();

                break;
        }
    }

    private void DestroySequence()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        bulletLight.enabled = false;

        StartCoroutine(PlayDeathParticles());
    }

    private IEnumerator PlayDeathParticles()
    {
        deathParticles.Play();

        while (deathParticles.isEmitting)
        {
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
