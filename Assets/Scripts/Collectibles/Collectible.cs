using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private Color particleColor;
    [SerializeField] private ParticleSystem deathParticles;

    private void Start()
    {
        if (deathParticles == null) return;

        var main = deathParticles.main;
        main.startColor = particleColor;

        deathParticles.Play();
    }

    public void DestroySequence()
    {
        if (deathParticles == null) return;

        var main = deathParticles.main;
        main.startColor = particleColor;

        foreach (SphereCollider collider in gameObject.GetComponents<SphereCollider>())
        {
            collider.enabled = false;
        }

        foreach (MeshRenderer meshRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (meshRenderer != null) meshRenderer.enabled = false;
        }

        gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

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
