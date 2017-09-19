using System;
using System.Collections.Generic;

namespace Daenet.Common.Logging
{
    public interface ILogManager
    {
        Dictionary<string, string> CurrentScope { get; }
        string SourceName { get; }

        void AddAdditionalParams(string paramName, string paramValue);
        [Obsolete("Use AddAdditionalParams(string paramName, string paramValue) instead.")]
        void AddScope(string scopeName, string scopeValue);
        IDisposable BeginScope<TState>(TState state);
        IDisposable BeginScope(string messageFormat, params object[] args);
        void ClearScope();
        void Close();
        void Dispose();
        void RemoveAdditionalParams(string paramName);
        [Obsolete("Use RemoveAdditionalParams(string paramName) instead.")]
        void RemoveScope(string scopeName);
        void TraceCritical(int eventId, Exception err, string msg, params object[] myParams);
        void TraceCritical(int eventId, string msg, params object[] myParams);
        void TraceDebug(int eventId, string msg, params object[] myParams);
        void TraceError(TracingLevel traceLevel, int eventId, Exception err, string msg, params object[] myParams);
        void TraceError(TracingLevel traceLevel, int eventId, string msg, params object[] myParams);
        void TraceError(int eventId, string msg, params object[] myParams);
        void TraceError(int eventId, Exception err, string msg, params object[] myParams);
        void TraceMessage(TracingLevel traceLevel, int eventId, string msg, params object[] myParams);
        void TraceMessage(int eventId, string msg, params object[] myParams);
        void TraceWarning(TracingLevel traceLevel, int eventId, string msg, params object[] myParams);
        void TraceWarning(int eventId, string msg, params object[] myParams);
    }
}