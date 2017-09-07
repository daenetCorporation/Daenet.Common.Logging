using System;
using System.Collections.Generic;

namespace Daenet.Common.Logging
{
    public interface ILogManager
    {
        Dictionary<string, string> CurrentScope { get; }
        string SourceName { get; }

        void AddScope(string scopeName, string scopeValue);
        void ClearScope();
        void Close();
        void Dispose();
        void RemoveScope(string scopeName);
        void TraceCritical(int eventId, Exception err, string msg, params object[] myParams);
        void TraceCritical(int eventId, string msg, params object[] myParams);
        void TraceDebug(int eventId, string msg, params object[] myParams);
        void TraceError(TracingLevel traceLevel, int eventId, Exception err, string msg, params object[] myParams);
        void TraceError(TracingLevel traceLevel, int eventId, string msg, params object[] myParams);
        void TraceMessage(TracingLevel traceLevel, int eventId, string msg, params object[] myParams);
        void TraceWarning(TracingLevel traceLevel, int eventId, string msg, params object[] myParams);
    }
}