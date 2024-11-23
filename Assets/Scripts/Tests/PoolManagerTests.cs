using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using StarterKit.Editor.Utility;
using StarterKit.PoolManagerLib;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class PoolManagerTests
{
    [SetUp]
    public void Setup()
    {
        var poolManagerPrefab = AssetFinder.Find<GameObject>("PoolManager", "Assets/Plugins/StarterKit/PoolManager/Prefabs");
        Object.Instantiate(poolManagerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
    
    [UnityTest]
    public IEnumerator CreatePrefab()
    {
        var capsulePrefab = Resources.Load<GameObject>("Prefabs/Capsule");
        var createdCapsule = PoolManager.GetGameObject(capsulePrefab);
        yield return null;
        Assert.IsNotNull(createdCapsule);
    }
    
    [UnityTest]
    public IEnumerator CreatePrefabInActive()
    {
        var capsulePrefab = Resources.Load<GameObject>("Prefabs/Capsule");
        var createdCapsule = PoolManager.GetGameObject(capsulePrefab, false);
        yield return null;
        Assert.IsNotNull(createdCapsule);
        Assert.IsTrue(!createdCapsule.activeSelf);
    }
}
