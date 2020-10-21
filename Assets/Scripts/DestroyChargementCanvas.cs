using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChargementCanvas : MonoBehaviour
{

    //on cree une fonction destroy publique pour y acceder dans l'inspector pour detruire le gameobject lorsque qu'elle est activée
    public void Destroy()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
