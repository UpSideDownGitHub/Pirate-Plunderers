
using UnityEngine;

public class healthbardamage : MonoBehaviour
{
    public float damage;

    // Update is called once per frame
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            takedamage();
    }
    }

    void takedamage()
    {
        healthbar.currenthealth -= damage;
    }
}
