using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Launches projectiles from a touch point with the specified <see cref="initialSpeed"/>.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class ProjectileLauncher : PressInputBase
    {
        // public GameObject Vines;
        [SerializeField]
        public GameObject m_ProjectilePrefab;

        // public Rigidbody projectilePrefab
        // {
        //     get => m_ProjectilePrefab;
        //     set => m_ProjectilePrefab = value;
        // }

        [SerializeField]
        float m_InitialSpeed = 25;

        public float initialSpeed
        {
            get => m_InitialSpeed;
            set => m_InitialSpeed = value;
        }

        protected override void OnPressBegan(Vector3 position)
        {
            Debug.Log(GetComponent<Transform>().localPosition);
            if (m_ProjectilePrefab == null)
                return;
            m_ProjectilePrefab.GetComponent<GrowVines>().allowGrow = true;
            
            Vector3 projectilePosition = GetComponent<Transform>().localPosition;
            // var projectile = Instantiate(m_ProjectilePrefab, position , Quaternion.identity);

            var projectile = Instantiate(m_ProjectilePrefab, position , Quaternion.identity);
            //+ new Vector3(-9.34f,-3.4f,-7.61f)
            // var rigidbody = projectile.GetComponent<Rigidbody>();
            // rigidbody.velocity = ray.direction * m_InitialSpeed;

        }
    }
}
