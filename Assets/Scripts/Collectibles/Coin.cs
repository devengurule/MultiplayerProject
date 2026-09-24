using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticles;

    public void DestroySequence()
    {
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
