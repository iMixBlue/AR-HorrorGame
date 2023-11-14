using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public GameObject m_ProjectilePrefab;
    public GameObject myCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            m_ProjectilePrefab.GetComponent<GrowVines>().allowGrow = true;
            
            Vector3 position = myCamera.GetComponent<Transform>().localPosition;
            var projectile = Instantiate(m_ProjectilePrefab, position , Quaternion.identity);

        }
            //+ new Vector3(-9.34f,-3.4f,-7.61f)
            // var rigidbody = projectile.GetComponent<Rigidbody>();
            // rigidbody.velocity = ray.direction * 20;

        
         
    }
}
