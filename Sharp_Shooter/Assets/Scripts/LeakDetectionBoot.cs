// Assets/Scripts/LeakDetectionBoot.cs

using UnityEngine;
using Unity.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class LeakDetectionBoot
{
    // 플레이 모드 진입 전에 강제로 스택트레이스 모드 켜기 (런타임/에디터 공통)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void EnableLeakTraces()
    {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;
        Debug.Log("[LeakDetection] Enabled with Stack Trace");
#else
        Debug.LogWarning("[LeakDetection] Collections checks are not enabled for this build (no leak stack traces).");
#endif
    }
}

#if UNITY_EDITOR
// 에디터가 켜질 때도 강제 활성화(선택사항)
[InitializeOnLoad]
static class LeakDetectionEditorBoot
{
    static LeakDetectionEditorBoot()
    {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;
        Debug.Log("[LeakDetection] Editor boot — Enabled with Stack Trace");
#endif
    }
}
#endif
