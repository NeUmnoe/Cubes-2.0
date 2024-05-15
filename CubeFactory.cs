using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _prefab;

    public Cube Create(Vector3 position, Quaternion rotation, float scale, float chanceToSplit)
    {
        Cube cube = Instantiate(_prefab, position, rotation);
        cube.Initialize(scale, new Color(Random.value, Random.value, Random.value), chanceToSplit);
        return cube;
    }
}