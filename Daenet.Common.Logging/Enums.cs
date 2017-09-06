using System;

namespace Daenet.Common.Logging
{
    /// <summary>
    /// Describes the who which stacktrace is shown.
    /// </summary>
    public enum StackTraceLevel
    {
        /// <summary>
        /// Show no Stacktrace.
        /// </summary>
        None,
        /// <summary>
        /// Shows methodnames only.
        /// </summary>
        Method,

        /// <summary>
        /// Show Stacktrace always.
        /// </summary>
        Full
    }

    /// <summary>
    /// Specifed the trace level for an exception.
    /// </summary>
    public enum ExceptionTraceLevel
    {
        /// <summary>
        /// Show no Stacktrace.
        /// </summary>
        None,
        /// <summary>
        /// Shows the Exception.
        /// </summary>
        Exception,

        /// <summary>
        /// Shows the InnerExceptions.
        /// </summary>
        InnerExceptions

    }

    /// <summary>
    /// Specifies the trace (log) level.
    /// </summary>
    public enum TracingLevel : int
    {
        /// <summary>
        /// The Trace Level 1
        /// This are the neccessary tracings, Start/End Application and all exceptions.
        /// e.g. (ServiceStart, ServiceEnds)
        /// </summary>
        Level1 = 1,

        /// <summary>
        /// The Trace Level 2
        /// </summary>
        Level2 = 2,

        /// <summary>
        /// The Trace Level 3
        /// </summary>
        Level3 = 3,

        /// <summary>
        /// The Trace Level 4
        /// </summary>
        /// <remarks>
        /// This is the verbose level, all developer messages are should trace at this level.
        /// </remarks>
        Level4 = 4,

        /// <summary>
        /// Tracing disabled.
        /// </summary>
        TraceOff = 0,

        /// <summary>
        /// Low trace level
        /// </summary>
        [Obsolete("Use the Level1")]
        Low = 1,

        /// <summary>
        /// Normal trace level
        /// </summary>
        [Obsolete("Use the Level2")]
        Normal = 2,

        /// <summary>
        /// Hiher than normal.
        /// </summary>
        [Obsolete("Use the Level3")]
        High = 3,

        /// <summary>
        /// At this level are all messages traced to the output.
        /// </summary>
        [Obsolete("Use the Level4")]
        Verbose = 4
    }
}
