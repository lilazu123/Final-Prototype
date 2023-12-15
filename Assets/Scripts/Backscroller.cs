using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float scrollSpeed = 2.0f; // Adjust the speed as needed
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        renderer.material.mainTextureOffset = new Vector2(0, offset);
    }
}

