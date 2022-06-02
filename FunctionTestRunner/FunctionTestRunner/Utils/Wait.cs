namespace FunctionTestRunner.Utils;

public static class Wait
{
    public static void ForMilliseconds(double milliseconds)
    {
        WaitInternal(TimeSpan.FromMilliseconds(milliseconds));
    }

    public static void ForSeconds(double seconds)
    {
        WaitInternal(TimeSpan.FromSeconds(seconds));
    }

    public static void ForMinutes(double minutes)
    {
        WaitInternal(TimeSpan.FromMinutes(minutes));
    }

    private static void WaitInternal(TimeSpan timespan)
    {
        Thread.Sleep(timespan);
    }
}
