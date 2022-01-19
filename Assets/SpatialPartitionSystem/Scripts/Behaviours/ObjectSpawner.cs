﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using SpatialPartitionSystem.Core;

namespace SpatialPartitionSystem.Behaviours
{
    [RequireComponent(typeof(SpatialGameObject))]
    public sealed class ObjectSpawner : MonoBehaviour
    {
        [SerializeField, Range(1, 100)] private int objectsCount = 10;
        [SerializeField, Range(0f, 5f)] private float creationDelay = 2f;
        
        [Space]
        [SerializeField] private SpatialGameObject objectPrefab;
        [SerializeField] private Transform objectsContainer;
        
        [Space]
        [SerializeField] UnityEvent<SpatialGameObject> onObjectCreated = new UnityEvent<SpatialGameObject>();
        
        private readonly List<SpatialGameObject> _objects = new List<SpatialGameObject>(capacity: 100);

        private void Start()
        {
            StartCoroutine(SpawnObjectsCoroutine());
        }

        private IEnumerator SpawnObjectsCoroutine()
        {
            SpatialGameObject rootSpatialObject = GetComponent<SpatialGameObject>();
            
            for (int i = 0; i < objectsCount; i++)
            {
                Vector3 min = rootSpatialObject.BoundsMin;
                Vector3 max = rootSpatialObject.BoundsMax;
                
                float randomX = Random.Range(min.x, max.x);
                float randomY = Random.Range(min.y, max.y);
                float randomZ = Random.Range(min.z, max.z);

                var instance = Instantiate(
                    objectPrefab,
                    transform.TransformPoint(new Vector3(randomX, randomY, randomZ)),
                    objectPrefab.transform.rotation,
                    objectsContainer
                );
                instance.name = instance.name + $" ({i})";
                instance.gameObject.SetActive(false);
                
                yield return new WaitForSeconds(creationDelay);
                
                _objects.Add(instance);
                onObjectCreated.Invoke(instance);
                
                instance.gameObject.SetActive(true);
            }
        }
        
        private IEnumerator RemoveObjectsCoroutine()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                var removedObject = _objects[i];
                Debug.Log($"Removing \'{removedObject.name}\' object...");
                yield return new WaitForSeconds(creationDelay);
                
                // _quadTree.TryRemove(removedObject);
                
                removedObject.gameObject.SetActive(false);
                Debug.Log($"Object \'{removedObject.name}\' was removed!");
                Debug.Log($"************************************");
            }
            
            _objects.Clear();
        }
    }
}
