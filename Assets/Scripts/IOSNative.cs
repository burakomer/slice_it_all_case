#if !UNITY_EDITOR && UNITY_IOS
using System.Runtime.InteropServices;

public class IOSNative
{
    [DllImport("__Internal")]
    private static extern void HapticFeedback(int type);

    public static void StartHapticFeedback(HapticFeedbackTypes type)
    {
        HapticFeedback((int)type);
    }
}

public enum HapticFeedbackTypes
{
    LIGHT,
    MEDIUM,
    HEAVY
}
#endif