
using UnityEngine;
using System.Collections;

public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T> {

	private static T instance;

	
	public static T Instance
	{
		get
		{
			if (instance == null) {


                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;

                    if (instance == null)
                    {

                        GameObject go = new GameObject();
                        instance = go.AddComponent<T>();
                    }
                					
				}

			}
			return instance;
		}
	}

}
