using UnityEngine;

public class Splitter : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private int _minCubes = 2;
    [SerializeField] private int _maxCubes = 6;
    [SerializeField] private float _scaleFactor = 0.5f;
    [SerializeField] private float _baseExplosionForce = 100f;
    [SerializeField] private float _baseExplosionRadius = 1f;
    [SerializeField] private float _splitChanceDecay = 0.5f;
    [SerializeField] private float _positionOffset = 0.5f;

    private float _chanceToSplit = 1f;
    private float _maxExplosionEffect = 1f;

    public void SetChanceToSplit(float chance)
    {
        _chanceToSplit = chance;
    }

    public void SplitCube()
    {
        if (Random.value < _chanceToSplit)
        {
            int numCubes = Random.Range(_minCubes, _maxCubes + 1);
            Vector3 explosionPoint = transform.position;
            float newChanceToSplit = _chanceToSplit * _splitChanceDecay;

            for (int i = 0; i < numCubes; i++)
            {
                Vector3 position = explosionPoint + Random.insideUnitSphere * _positionOffset;
                float scale = transform.localScale.x * _scaleFactor;
                Cube newCube = _cubeFactory.Create(position, Random.rotation, scale, newChanceToSplit);
                Rigidbody newCubeRigidbody = newCube.GetComponent<Rigidbody>();

                newCubeRigidbody.AddExplosionForce(_baseExplosionForce, explosionPoint, _baseExplosionRadius);
            }
        }
        else
        {
            Explode();
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        float currentScale = transform.localScale.x;
        float inverseScale = _maxExplosionEffect / currentScale;
        float explosionForce = _baseExplosionForce * inverseScale;
        float explosionRadius = _baseExplosionRadius * inverseScale;
        Vector3 explosionPoint = transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPoint, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Cube cubeComponent = collider.GetComponent<Cube>();

            if (cubeComponent != null)
            {
                Rigidbody cubeRigidbody = collider.GetComponent<Rigidbody>();

                    float explosionDistance = Vector3.Distance(explosionPoint, cubeRigidbody.transform.position);
                    float explosionEffect = _maxExplosionEffect - (explosionDistance / explosionRadius);

                    cubeRigidbody.AddExplosionForce(explosionForce * explosionEffect, explosionPoint, explosionRadius);
            }
        }
    }
}