using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using scrubbzzAPI;
public class testapi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        cat myCat = new cat();
        myCat.Speak();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
