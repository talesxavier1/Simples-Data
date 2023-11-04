using System.Diagnostics;

namespace Simples_Data.APIs.TraceListeners;
public class LogTraceListener : TraceListener {

    public override void Write(string? message) {
        //Console.WriteLine(message);
    }

    public override void WriteLine(string? message) {
        Write(message);
    }
}
