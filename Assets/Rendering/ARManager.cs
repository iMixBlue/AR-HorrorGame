using System.Collections.Generic;
 
using UnityEngine;
 
using UnityEngine.XR.ARFoundation;
 
 
 
public class ARManager : MonoBehaviour
 
{
 
    [Header("AR Foundation")]
 
    /// <summary>
 
    /// The active ARRaycastManager used in the example.
 
    /// </summary>
 
    public ARPlaneManager m_ARPlaneManager;
 
 
 
    [HideInInspector]
 
 
 
    /// <summary>
 
    /// 当前识别出的平面
 
    /// </summary>
 
    List<ARPlane> detectPlanes = new List<ARPlane>();
 
 
 
    /// <summary>
 
    /// 当前是否要显示平面
 
    /// </summary>
 
    bool isShowPlane = true;
 
 
 
    #region MonoBehaviour CallBacks
 
 
 
    private void Awake()
 
    {     
 
        m_ARPlaneManager = FindObjectOfType<ARPlaneManager>();
 
    }
 
 
 
    void Start()
 
    {
 
        CheckDevice();
 
 
 
        m_ARPlaneManager.planesChanged += OnPlaneChanged;
 
    }
 
 
 
    private void Update()
 
    {
 
        SaveElePolicy();
 
    }
 
 
 
    void OnDisable()
 
    {
 
        m_ARPlaneManager.planesChanged -= OnPlaneChanged;
 
    }
 
 
 
    #endregion
 
 
 
    // 启用与禁用平面检测
 
    // 程序默认启用，启用时一直不停地检测平面。关闭时则不会再检测新平面了。
 
    public void DetectionPlane(bool value)
 
    {
 
        m_ARPlaneManager.enabled = value;
 
        if (m_ARPlaneManager.enabled)
 
        {
 
            print("已启用平面检测");
 
        }
 
        else
 
        {
 
            print("已禁用平面检测");
 
        }
 
    }
 
 
 
    // 显示与隐藏检测到的平面 
 
    public void SwitchPlane()
 
    {
 
        isShowPlane = !isShowPlane;
 
 
 
        for (int i = detectPlanes.Count - 1; i >= 0; i--)
 
        {
 
            if (detectPlanes[i] == null || detectPlanes[i].gameObject == null)
 
                detectPlanes.Remove(detectPlanes[i]);
 
            else
 
                detectPlanes[i].gameObject.SetActive(isShowPlane);
 
        }
 
    }
 
 
 
    /// <summary>
 
    /// 得到当前AR会话是否正在运行，并被跟踪(即，该设备能够确定其在世界上的位置和方向)。
 
    /// </summary>
 
    public bool Skode_IsTracking()
 
    {
 
        bool isTracking = false;
 
 
 
        if (ARSession.state == ARSessionState.SessionTracking)
 
        {
 
            isTracking = true;
 
        }
 
 
 
        return isTracking;
 
    }
 
 
 
 
 
    //在ARFoundation新发现平面时，将平面添加进列表里，便于我们控制这些平面
 
    void OnPlaneChanged(ARPlanesChangedEventArgs arg)
 
    {
 
        for (int i = 0; i < arg.added.Count; i++)
 
        {
 
            detectPlanes.Add(arg.added[i]);
 
            arg.added[i].gameObject.SetActive(isShowPlane);
 
        }
 
    }
 
 
 
    //检查设备运行环境
 
    void CheckDevice()
 
    {
 
        if (ARSession.state == ARSessionState.NeedsInstall)
 
        {
 
            ShowAndroidToastMessage("AR is supported, but requires an additional install. .");
 
            Invoke("Quit", 1);
 
        }
 
        else if (ARSession.state == ARSessionState.Ready)
 
        {
 
            Debug.Log("AR is supported and ready.");
 
        }
 
        else if (ARSession.state == ARSessionState.Unsupported)
 
        {
 
            ShowAndroidToastMessage("AR is not supported on the current device.");
 
            Invoke("Quit", 1);
 
        }
 
    }
 
 
 
    void ShowAndroidToastMessage(string message)
 
    {
 
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
 
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
 
        if (unityActivity != null)
 
        {
 
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
 
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
 
            {
 
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
 
                toastObject.Call("show");
 
            }));
 
        }
 
    }
 
 
 
    void Quit()
 
    {
 
        Application.Quit();
 
    }
 
 
 
    /// <summary>
 
    /// 一种省电设置，当设备没找到识别目标，允许屏幕在最后激活一段时间后变暗
 
    /// </summary>
 
    void SaveElePolicy()
 
    {
 
        if (ARSession.state != ARSessionState.SessionTracking)
 
        {
 
            const int lostTrackingSleepTimeout = 15;
 
            Screen.sleepTimeout = lostTrackingSleepTimeout;
 
        }
 
        else
 
        {
 
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
 
        }
 
    }
 
 
 
}