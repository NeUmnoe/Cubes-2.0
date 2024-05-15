using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Splitter))]

public class Cube : MonoBehaviour
{
    private Splitter _splitter;

    private void Awake()
    {
        _splitter = GetComponent<Splitter>();
    }

    public void Initialize(float scale, Color color, float chanceToSplit)
    {
        transform.localScale = new Vector3(scale, scale, scale);
        GetComponent<Renderer>().material.color = color;
        _splitter.SetChanceToSplit(chanceToSplit);
    }
}