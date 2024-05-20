using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public WeaponState ws;
    public StateManager sm;
    // Start is called before the first frame update
    void Awake()
    {
        //wm = Bind<WeaponManager>(gameObject);
        sm = Bind<StateManager>(gameObject);
        ws = Bind<WeaponState>(gameObject);
    }

    private T Bind<T>(GameObject obj) where T : CharacterManagerInterface
    {
        T temp;
        temp = obj.GetComponent<T>();
        if(temp == null)
        {
            temp = obj.AddComponent<T>();
        }
        temp.cm = this;
        return temp;
    }

    // Update is called once per frame
    public void Update()
    {
        //sm.Test();
    }
}
