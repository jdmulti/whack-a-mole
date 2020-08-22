using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Util class to quickly get all objects in scene of a certain component type.
/// </summary>
public class Components
{
    /*################################################################################
        Functions: inherit
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        GetAllOfType
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Get all components of type T from the scene.
    /// </summary>
    /// <typeparam name="T">Component type.</typeparam>
    /// <returns>Array of components type T existing in current scene.</returns>
    public static T[] GetAllOfType<T>()
    {
        List<T> components = new List<T>();
        int count = SceneManager.sceneCount;
        for (int i = 0; i < count; i++)
        {
            GameObject[] gameObjects = SceneManager.GetSceneAt(i).GetRootGameObjects();
            components.AddRange(GetTypesFromGameObject<T>(gameObjects));
        }
        T[] componentArray = components.ToArray();
        return componentArray;
    }
    /*--------------------------------------------------------------------------------
        GetTypesFromGameObject
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Get all components of type T from parent object.
    /// </summary>
    /// <typeparam name="T">Compontent type.</typeparam>
    /// <param name="gameObjects">Array of gameobjects (parents) to check childeren of component type T.</param>
    /// <returns></returns>
    public static T[] GetTypesFromGameObject<T>(GameObject[] gameObjects)
    {
        List<T> components = new List<T>();
        int count = gameObjects.Length;
        for (int i = 0; i < count; i++)
        {
            components.AddRange(gameObjects[i].GetComponentsInChildren<T>(true));
        }
        return components.ToArray();
    }
}
