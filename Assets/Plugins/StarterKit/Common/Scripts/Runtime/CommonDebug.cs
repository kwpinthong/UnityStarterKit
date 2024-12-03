namespace StarterKit.Common
{
    public static class CommonDebug
    {
        public static void Log(object message, UnityEngine.Object context = null)
        {
#if ENABLE_LOG
            UnityEngine.Debug.Log(message, context);
#endif
        }

        public static void LogWarning(object message, UnityEngine.Object context = null)
        {
#if ENABLE_LOG
            UnityEngine.Debug.LogWarning(message, context);
#endif
        }

        public static void LogError(object message, UnityEngine.Object context = null)
        {
#if ENABLE_LOG
            UnityEngine.Debug.LogError(message, context);
#endif
        }
    }
}

