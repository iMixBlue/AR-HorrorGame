using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowVines : MonoBehaviour
{
    public List<MeshRenderer> growVinesMeshes;//获取List中的MeshRender组件
    public float timeToGrow = 5f;//生长时间
    public float timeRemain = 1f;
    public float refreshRate = 0.05f;//更新频率
    [Range(0, 1)]
    public float minGrow = 0.2f;//最小生长距离
    [Range(0, 1)]
    public float maxGrow = 0.97f;//最大生长距离

    private List<Material> growVinesMaterials = new List<Material>();//定义材质
    private bool fullyGrown;//生长完成

    public bool allowGrow = false;


    // Start is called before the first frame update
    void Start()
    {
        //获取List下的各个MeshRender的所有材质，growVinesMeshes[i].materials.Length为第i个组件下材质的数量
        for (int i = 0; i < growVinesMeshes.Count; i++)
        {
            for (int j = 0; j < growVinesMeshes[i].materials.Length; j++)
            {
                //如果第i个MeshRender的第j个材质具有“_Grow”属性则设置为minGrow
                if (growVinesMeshes[i].materials[j].HasProperty("_Grow"))
                {
                    growVinesMeshes[i].materials[j].SetFloat("_Glow", minGrow);
                    growVinesMaterials.Add(growVinesMeshes[i].materials[j]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space)){
            // Debug.Log(111);
            
        // }
            if(allowGrow){
            
             for (int i = 0; i < growVinesMaterials.Count; i++)
        {
            StartCoroutine(GrowAndDie(growVinesMaterials[i]));
            // Debug.Log(111);
            // StartCoroutine(GrowVine(growVinesMaterials[i]));
        }
        
      
    }


     IEnumerator GrowAndDie(Material material)
    {
        float growValue = material.GetFloat("_Grow");
        // Debug.Log(growValue);

            if (growValue < maxGrow)
            {
                growValue += 1 / (timeToGrow / refreshRate);
                material.SetFloat("_Grow", growValue);
            
                 yield return new WaitForSeconds(refreshRate);
            }

        if (growValue >= maxGrow){
            yield return new WaitForSeconds(timeRemain);
            material.SetFloat("_Grow", minGrow - 0.1f);
            allowGrow = false;
            Destroy(transform.gameObject);
            // Debug.Log(111);
        }
        
    }


    //控制growValue
//     IEnumerator GrowVine(Material mat)
//     {
//         float growValue = mat.GetFloat("_Grow");
//         if (!fullyGrown)
//         {
//             while (growValue < maxGrow)
//             {
//                 growValue += 1 / (timeToGrow / refreshRate);
//                 mat.SetFloat("_Grow", growValue);

               
//             }
//         }
//         else
//         {
//             while (growValue > minGrow)
//             {
//                 growValue -= 1 / (timeToGrow / refreshRate);
//                 mat.SetFloat("_Grow", growValue);

//                 yield return new WaitForSeconds(refreshRate);
//             }
//         }

//         if (growValue >= maxGrow)
//             fullyGrown = true;
//         else
//             fullyGrown = false;
//     }
// }
}
}