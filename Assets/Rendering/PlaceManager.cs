using System.Collections.Generic;
 
using UnityEngine;
 
using UnityEngine.EventSystems;
 
using UnityEngine.XR.ARFoundation;
 
using UnityEngine.XR.ARSubsystems;
 
 
 
namespace FrameworkDesign.Example
 
{
 
  
 
    public class PlaceManager : MonoBehaviour
 
    {
 
        [Header("AR Foundation")] 
 
        /// <summary>
 
        /// The active ARRaycastManager used in the example.
 
        /// </summary>
 
        public ARRaycastManager m_RaycastManager;
 
 
 
 
 
        [Header("UI")]
 
        [SerializeField]
 
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
 
        GameObject m_PlacedPrefab;//要放置的预制件
 
 
 
        /// <summary>
 
        /// The prefab to instantiate on touch.
 
        /// </summary>
 
        public GameObject placedPrefab
 
        {
 
            get { return m_PlacedPrefab; }
 
            set { m_PlacedPrefab = value; }
 
        }
 
 
 
        [HideInInspector]
 
        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();//存放检测到的碰撞点
 
        /// <summary>
 
        /// The object instantiated as a result of a successful raycast intersection with a plane.
 
        /// </summary>
 
        public GameObject spawnedObject { get; private set; }
 
 
 
        void Awake()
 
        {
 
            // m_RaycastManager = GetComponent<ARRaycastManager>();//也可以通过GetComponent获取到ARRaycastManager
 
        }
 
 
 
        bool TryGetTouchPosition(out Vector2 touchPosition)
 
        {
 
            if (Input.touchCount > 0)
 
            {
 
                touchPosition = Input.GetTouch(0).position;
 
                return true;
 
            }
 
 
 
            touchPosition = default;
 
            return false;
 
        }
 
 
 
        void Update()
 
        {
 
            if (!TryGetTouchPosition(out Vector2 touchPosition))
 
                return;
 
 
 
            var touch = Input.GetTouch(0);
 
            const TrackableType trackableTypes =
 
            TrackableType.FeaturePoint |
 
            TrackableType.PlaneWithinPolygon;
 
 
 
            if (Input.touchCount == 1 && touch.phase == TouchPhase.Moved)//移动已放置的对象
 
            {
 
 
 
                if (m_RaycastManager.Raycast(touchPosition, s_Hits, trackableTypes))
 
                {
 
                    // Raycast hits are sorted by distance, so the first one
 
                    // will be the closest hit.
 
                    var hitPose = s_Hits[0].pose;
 
                    if (spawnedObject != null)
 
                    {
 
                        spawnedObject.transform.position = hitPose.position;
 
                    }                   
 
                }
 
            }
 
            if (Input.touchCount == 1 && touch.phase == TouchPhase.Began)//检测touch begin，在touch begin中做射线碰撞检测
 
            {
 
                //---判断是否touch到UI组件----
 
                //#if IPHONE || ANDROID
 
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
 
                //#else
 
                // if (EventSystem.current.IsPointerOverGameObject())
 
                //#endif
 
                //Debug.Log("当前触摸在UI上");
 
                {
 
                    Debug.Log($"当前触摸在UI上"+ touch.phase);
 
                    return;
 
                }
 
                else
 
                {
 
                    Debug.Log("当前没有触摸在UI上");
 
                    // Logger.Log($"当前没有触摸在UI上"+ touch.phase);
 
                }
 
 
 
                if (m_RaycastManager.Raycast(touchPosition, s_Hits, trackableTypes))
 
                {
 
                    // Raycast hits are sorted by distance, so the first one
 
                    // will be the closest hit.
 
                    var hitPose = s_Hits[0].pose;
 
                    if (spawnedObject == null)
 
                    {
 
                        spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);//实例化预制件对象
 
                    }
 
                    else
 
                    {
 
                        spawnedObject.transform.position = hitPose.position;//更新对象状态
 
                    }
 
                }
 
            }              
 
        }      
 
    }
 
}