using System.Diagnostics;
using Xunit.Abstractions;

namespace FunctionTestRunner.Utils
{
    public static class Try
    {
        public static void Until(Action action, Func<bool> validate, int timeoutInSeconds, ITestOutputHelper output, int intervalInMilliseconds = 1000, string? contextInfo = null)
        {
            var until = DateTime.Now.AddSeconds(timeoutInSeconds);

            Exception? error = null;
            try { action.Invoke(); } catch { }

            var timer = new Stopwatch();
            timer.Start();
            while (!validate.Invoke() && DateTime.Now < until)
            {
                Thread.Sleep(intervalInMilliseconds);

                try
                {
                    action.Invoke();
                    error = null;
                }
                catch (Exception e)
                {
                    error = e;
                }
            }
            timer.Stop();

            if (error != null)
            {
                output.WriteLine("Try.Until with error " + error);
                output.WriteLine($"Try.Until context {contextInfo}");
                throw error;
            }

            if (timer.Elapsed.TotalSeconds >= timeoutInSeconds)
            {
                output.WriteLine($"Try.Until exceeded timeout on {timeoutInSeconds}s");
                output.WriteLine($"Try.Until context {contextInfo}");
                throw new TimeoutException();
            }

            Debug.WriteLine($"Try.Until ran for {timer.ElapsedMilliseconds}ms, timeout set to: {timeoutInSeconds}s");
        }
    }
}
