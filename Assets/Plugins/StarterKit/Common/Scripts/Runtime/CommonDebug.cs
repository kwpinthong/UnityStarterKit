namespace StarterKit.Common
{
    public static class CommonDebug
    {
        public static void Log(object message)
        {
#if STARTERKIT_ENABLE_LOG
            UnityEngine.Debug.Log(message);
#endif
        }
        
        public static void Log(object message, UnityEngine.Object context)
        {
#if STARTERKIT_ENABLE_LOG
            UnityEngine.Debug.Log(message, context);
#endif
        }

        public static void LogWarning(object message)
        {
#if STARTERKIT_ENABLE_LOG
            UnityEngine.Debug.LogWarning(message);
#endif
        }
        
        public static void LogWarning(object message, UnityEngine.Object context)
        {
#if STARTERKIT_ENABLE_LOG
            UnityEngine.Debug.LogWarning(message, context);
#endif
        }

        public static void LogError(object message)
        {
#if STARTERKIT_ENABLE_LOG
            UnityEngine.Debug.LogError(message);
#endif
        }
        
        public static void LogError(object message, UnityEngine.Object context)
        {
#if STARTERKIT_ENABLE_LOG
            UnityEngine.Debug.LogError(message, context);
#endif
        }
    }
}

