﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSurfaceHandler : MonoBehaviour
{
    [Header("Surface detection")]
    public LayerMask surfaceLayer;

    //Hit check
    Collider2D[] surfaceCollidersHit = new Collider2D[10];
    Vector3 lastSampledSurfacePosition = Vector3.one * 10000;

    //SurfaceType
    Surface.SurfaceTypes drivingOnSurface = Surface.SurfaceTypes.Road;

    //Other components
    Collider2D carCollider;
    
    //часть костыля для моста
    private CarLayerHandler _carLayerHandler;

    void Awake()
    {
        carCollider = GetComponentInChildren<Collider2D>();
        _carLayerHandler = GetComponent<CarLayerHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //костыль чтоб на мосту ехать как по дороге
        if (_carLayerHandler.IsDrivingOnOverpass())
        {
            drivingOnSurface = Surface.SurfaceTypes.Road;
            return;
        }
        //конец костыля
        
        if ((transform.position - lastSampledSurfacePosition).sqrMagnitude < 0.75f)
            return;

        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.layerMask = surfaceLayer;
        contactFilter2D.useLayerMask = true;
        contactFilter2D.useTriggers = true;

        int numberOfHits = Physics2D.OverlapCollider(carCollider, contactFilter2D, surfaceCollidersHit);

        float lastSurfaceZValue = -1000;

        for (int i = 0; i < numberOfHits; i++)
        {
            Surface surface = surfaceCollidersHit[i].GetComponent<Surface>();
            if (surface is null)
            {
                drivingOnSurface = Surface.SurfaceTypes.Sand;
                lastSurfaceZValue = 0;
                continue;
            }
            
            if (surface.transform.position.z > lastSurfaceZValue)
            {
                drivingOnSurface = surface.surfaceType;
                lastSurfaceZValue = surface.transform.position.z;
            }
        }

        if (numberOfHits == 0)
            drivingOnSurface = Surface.SurfaceTypes.Road;

        lastSampledSurfacePosition = transform.position;

        //Debug.Log($"Driving on {drivingOnSurface}");
    }

    public Surface.SurfaceTypes GetCurrentSurface()
    {
        return drivingOnSurface;
    }
}
