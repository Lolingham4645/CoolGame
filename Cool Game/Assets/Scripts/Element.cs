﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ElementType
{
    Air, Fire, Water, Earth, Light, Dark, NONE
}



public class Element : MonoBehaviour
{

    public Player player;
    public ElementController ec;

    //might want to add magnet range thingy
    const float moveDistance = 2;

    const float pickupDistance = 0.5f;

    const float movementSpeed = 10;

    private bool canMove = false;

    public ElementType elementType { get; private set; } = ElementType.NONE;

    SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        //TODO: replace with auto setting on creation
        player = FindObjectOfType<Player>();
        ec = FindObjectOfType<ElementController>();

        sr = GetComponent<SpriteRenderer>();

        elementType = (ElementType)Random.Range(0, 6);

        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        //move towards player when close
        if(Vector3.Distance(transform.position, player.transform.position) <= moveDistance)
        {
            canMove = true;
        }

        if(canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * movementSpeed);
        }

        if(Vector3.Distance(transform.position, player.transform.position) <= pickupDistance)
        {
            player.AddElement(elementType);
            Destroy(gameObject);
        }
    }

    public void SetElement(ElementType type)
    {
        elementType = type;
        ChangeColor();
    }


    private void ChangeColor()
    {
        if(sr == null)
        {
            Debug.LogError("ERROR: sprite renderer is null");
            return;
        }
        sr.color = ec.GetColor(elementType);
    }
}
