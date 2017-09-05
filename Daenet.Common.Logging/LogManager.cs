using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Security;
using System.Collections.Generic;
//using Daenet.Configuration;
using Daenet.Common.Logging;
using Microsoft.Extensions.Logging;

//TODO: Check Signature
//TODO: Refactor all
//TODO: Property for tracing in UTC

namespace Daenet.Diagnostics
{
    /// <summary>
    /// The LogManager is used to log messages from any application. The log manager uses the
    /// standard Trace technologie of the .NET Framework. Every Message is written in a spicified Trace Source
    /// which can be captured with a specified trace listener. The configuration is made in the application config,
    /// there can be specified for ever used trace source a number of trace listener.
    /// <example>
    /// <code lang="xml">
    /// <system.diagnostics>
    /// <sources>
    ///   <source name="Daenet.Diagnostic.Source">
    ///     <listeners>
    ///       <add name="TextListener"/>
    ///       <add name="ExDumper"/>
    ///       <remove name="Default" />
    ///     </listeners>
    ///   </source>
    /// </sources>
    /// <sharedListeners>
    ///   <add name="TextListener" type="Daenet.Diagnostics.FileTraceListener, Daenet.System"
    ///        traceLevel="Verbose"
    ///        fileName="c:\test.log"
    ///        stackTrace="Full"
    ///        exceptionStackTrace="Full"
    ///        exceptionTrace="InnerExceptions"
    ///        maxSize="10000"
    ///        utc="false"/>
    ///   <add name="ExDumper" type="Daenet.Diagnostics.FileExceptionDump, Daenet.System"/>
    /// </sharedListeners>
    /// </system.diagnostics>    
    /// </code>
    /// </example>
    /// </summary>
    /// <remarks>
    /// <list>
    /// 1. All messages have to have level between 1-4. On level 4 (Verbose) application vendor guarantees that all messages in the application are written in the
    /// log output. On level 0 (TraceOff) no message is written.<p/>
    /// 2. All errors are logged at level 1 (Low).
    /// 3. Warnings can be logged at any level.
    /// 4. Informations can be logged at any level.
    /// 5. TODO...
    /// </list>
    /// </remarks>
    public class LogManager : IDisposable
    {
        #region Private Fields

        /// <summary>
        /// Stores the source Name.
        /// </summary>
        private readonly string m_SourceName;

        /// <summary>
        /// Stores the trace source.
        /// </summary>
        private readonly TraceSource m_Source;



        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current trace source name.
        /// </summary>
        public string SourceName
        {
            get { return m_SourceName; }
        }

        /// <summary>
        /// Stores the trace source.
        /// </summary>
        public TraceSource Source
        {
            get { return m_Source; }
        }

        #endregion

        #region Constructors & Deconstructor
        /// <summary>
        /// Creates the instance of the logging manager.
        /// Default source is "Daenet.Diagnostics.Source"
        /// </summary>
        //public LogManager()
        //    : this("", null)
        //{
        //}

        public LogManager(ILogger logger)
        {
            m_Logger = logger;
        }


        /// <summary>
        /// Creates the instance of the logging manager.
        /// Default source is "Daenet.Diagnostics.Source"
        /// </summary>
        /// <param name="sourceName">The source name. If it is empty the default Source ("Daenet.Diagnostics.Source") is used.</param>
        //public LogManager(string sourceName)
        //    : this(sourceName, null)
        //{
        //}


        /// <summary>
        /// Creates the instance of the logging manager.
        /// Default source is "Daenet.Diagnostics.Source"
        /// </summary>
        /// <param name="parentLogMagr">The parent log manager to be used to derive scopes from.</param>
        //public LogManager(LogManager parentLogMagr)
        //    : this("", parentLogMagr)
        //{
        //}

        ///// <summary>
        ///// Creates the instance of the logging manager.
        ///// </summary>
        ///// <param name="parentLogMgr">The parent log manager to be used to derive scopes from.</param>
        ///// <param name="sourceName">The source name. If it is empty the default Source ("Daenet.Diagnostics.Source") is used.</param>
        //public LogManager(string sourceName, LogManager parentLogMgr)
        //{
        //}

        /// <summary>
        /// Creates the instance of the logging manager.
        /// </summary>
        /// <param name="parentLogMgr">The parent log manager to be used to derive scopes from.</param>
        /// <param name="sourceName">The source name. If it is empty the default Source ("Daenet.Diagnostics.Source") is used.</param>
        /// <param name="scopes">The scopes for the current LogManager if it is empty or null then no default scope will be used</param>
        //public LogManager(string sourceName, LogManager parentLogMgr, ScopesConfigElementCollection scopes)
        //{
        //    if (String.IsNullOrEmpty(sourceName))
        //        m_SourceName = "Daenet.Diagnostics.Source";
        //    else
        //        m_SourceName = sourceName;

        //    m_Source = new TraceSource(m_SourceName);
        //    m_ParentLogMgr = parentLogMgr;
        //    Source.Switch.Level = SourceLevels.All;

        //    if (scopes != null)
        //    {
        //        foreach (ScopeConfigElement scope in scopes)
        //        {
        //            if (scope.Name.Equals("UserName"))
        //                AddScope("UserName", Thread.CurrentPrincipal.Identity.Name);
        //            else if (scope.Name.Equals("MachineName"))
        //                AddScope("MachineName", System.Environment.MachineName);
        //            else if (scope.Name.Equals("ActivityId"))
        //                AddScope("ActivityId", Guid.NewGuid().ToString());
        //            else
        //                AddScope(scope.Name, scope.Value);
        //        }

        //    }
        //}

        /// <summary>
        /// Implements the finalizer. Calls Dispose with false.
        /// </summary>
        ~LogManager()
        {
            Dispose(false);
        }

        /// <summary>
        /// Override this method by implementing of custom logging.
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Closes all resources.
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// The Method traces a message.
        /// </summary>
        /// <param name="traceLevel">The Trace level for this message.</param>
        /// <param name="eventId">The event id to identifiy a specific event..</param>
        /// <param name="msg">The message to trace. The message can contains format placeholders, which are
        /// filled with the parameters.</param>
        /// <param name="myParams">The Parameters to fill the message.</param>
        /// <exception cref="Daenet.LogManagerException">If a log exception is thrown.</exception>
        public void TraceMessage(TracingLevel traceLevel, int eventId, string msg, params object[] myParams)
        {
            trace(TraceEventType.Information, traceLevel, eventId, null, msg, myParams);
        }


        /// <summary>
        /// The Method traces a error which is occured in the application.
        /// </summary>
        /// <param name="traceLevel">The Trace level for this message.</param>
        /// <param name="eventId">The event id to identifiy a specific event..</param>
        /// <param name="msg">The message to trace. The message can contains format placeholders, which are
        /// filled with the parameters.</param>
        /// <param name="myParams">The Parameters to fill the message.</param>
        /// <exception cref="Daenet.LogManagerException">If a log exception is thrown.</exception>
        public void TraceError(TracingLevel traceLevel, int eventId, string msg, params object[] myParams)
        {
            trace(TraceEventType.Error, traceLevel, eventId, null, msg, myParams);
        }

        /// <summary>
        /// The Method traces a error which is occured in the application.
        /// The Error contain an exception.
        /// </summary>
        /// <param name="traceLevel">The Trace level for this message.</param>
        /// <param name="eventId">The event id to identifiy a specific event..</param>
        /// <param name="err">The Error exception.</param>
        /// <param name="msg">The message to trace. The message can contains format placeholders, which are
        /// filled with the parameters.</param>
        /// <param name="myParams">The Parameters to fill the message.</param>
        /// <exception cref="Daenet.LogManagerException">If a log exception is thrown.</exception>
        public void TraceError(TracingLevel traceLevel, int eventId, Exception err, string msg, params object[] myParams)
        {
            trace(TraceEventType.Error, traceLevel, eventId, err, msg, myParams);
        }

        /// <summary>
        /// The Method traces a warning which is occured in the application.
        /// </summary>
        /// <param name="traceLevel">The Trace level for this message.</param>
        /// <param name="eventId">The event id to identifiy a specific event..</param>
        /// <param name="msg">The message to trace. The message can contains format placeholders, which are
        /// filled with the parameters.</param>
        /// <param name="myParams">The Parameters to fill the message.</param>
        /// <exception cref="Daenet.LogManagerException">If a log exception is thrown.</exception>
        public void TraceWarning(TracingLevel traceLevel, int eventId, string msg, params object[] myParams)
        {
            trace(TraceEventType.Warning, traceLevel, eventId, null, msg, myParams);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Trace the message in the trace source. 
        /// </summary>
        /// <param name="traceEvent">The tace event which is occured.</param>
        /// <param name="traceLevel">The Trace level for this message.</param>
        /// <param name="eventId">The event id to identifiy a specific event.</param>
        /// <param name="exception">The excetion which is thrown.</param>
        /// <param name="msg">The message to trace. The message can contains format placeholders, which are
        /// filled with the parameters.</param>
        /// <param name="myParams">The Parameters to fill the message.</param>
        /// <exception cref="Daenet.LogManagerException">If a log exception is thrown.</exception>
        private void trace(TraceEventType traceEvent, TracingLevel traceLevel, int eventId, Exception exception, string msg, params object[] myParams)
        {
            try
            {
                m_Logger.Log(mapToLogLevel(traceLevel), eventId, new Microsoft.Extensions.Logging.Internal.FormattedLogValues(msg, myParams), exception, null);
                //Source.TraceData(traceEvent, eventId, msg, traceLevel, exception, CurrentScope, myParams);
            }
            catch (Exception ex)
            {
                throw new LogManagerException(ex, "Cannot Log: {0}", ex.Message+ex.StackTrace);
            }
        }

        /// <summary>
        /// Maps <see cref="TracingLevel"/> to <see cref="LogLevel"/>.
        /// Default is <see cref="LogLevel.None"/>
        /// </summary>
        /// <param name="traceLevel"></param>
        /// <returns></returns>
        private LogLevel mapToLogLevel(TracingLevel traceLevel)
        {
            switch (traceLevel)
            {
                case TracingLevel.Level1:
                    return LogLevel.Critical;
                case TracingLevel.Level2:
                    return LogLevel.Error;
                case TracingLevel.Level3:
                    return LogLevel.Information;
                case TracingLevel.Level4:
                    return LogLevel.Trace;
                case TracingLevel.TraceOff:
                    return LogLevel.None;
                default:
                    return LogLevel.None;
            }
        }


        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the 
        /// runtime from inside the finalizer and you should not reference 
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing">Specifies whether to dispose all managed resources.</param>
        /// <remarks>Override this method by implementing of custom logging.</remarks>
        protected virtual void Dispose(bool disposing)
        {
            // If disposing equals true, dispose all managed 
            // and unmanaged resources.
            if (disposing)
            {

            }
        }
        #endregion

        #region Scope

        ///// <summary>
        ///// Gets the current scope. This is a reference to the curretn scope. Use it only internal.
        ///// </summary>
        //private Dictionary<string, string> currentScopeInternal
        //{
        //    get
        //    {
        //        if (m_StaticScope == null)
        //        {

        //            lock (typeof(LogManager))
        //            {
        //                if (m_StaticScope == null)
        //                    m_StaticScope = new Dictionary<string, string>();
        //            }
        //        }

        //        return m_StaticScope;
        //    }
        //}

        /// <summary>
        /// Gets the current scope, with all parrent LogManagers.
        /// This Scope is a copy and not the original.
        /// </summary>
        public Dictionary<string, string> CurrentScope
        {
            get
            {
                var dic = new Dictionary<string, string>();
                buildScope(dic);
                return dic;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, string> m_LocalScopes = new Dictionary<string, string>();

        private LogManager m_ParentLogMgr;
        private ILogger m_Logger;

        /// <summary>
        /// Add a scope
        /// </summary>
        /// <param name="scopeName">The name of the scope</param>
        /// <param name="scopeValue">The value of the scope.</param>
        public void AddScope(string scopeName, string scopeValue)
        {
            lock (m_LocalScopes)
            {
                if (m_LocalScopes.ContainsKey(scopeName))
                    m_LocalScopes.Remove(scopeName);
                m_LocalScopes.Add(scopeName, scopeValue);
            }
        }

        /// <summary>
        /// Remove a scope
        /// </summary>
        /// <param name="scopeName">The name of the scope</param>
        public void RemoveScope(string scopeName)
        {
            if (String.IsNullOrEmpty(scopeName))
                throw new ArgumentException("The scope name is null or empty", "scopeName");

            lock (m_LocalScopes)
            {
                if (m_LocalScopes.ContainsKey(scopeName))
                    m_LocalScopes.Remove(scopeName);
            }
        }

        /// <summary>
        /// Clears the scopes.
        /// </summary>
        public void ClearScope()
        {
            lock (m_LocalScopes)
            {
                m_LocalScopes.Clear();
            }
        }

        private void buildScope(Dictionary<string, string> scopeToFill)
        {
            if (m_ParentLogMgr != null)
                m_ParentLogMgr.buildScope(scopeToFill);

            foreach (var scopes in m_LocalScopes)
            {
                if (scopeToFill.ContainsKey(scopes.Key))
                    scopeToFill.Remove(scopes.Key);
                if(scopes.Key == "UserName")
                    scopeToFill.Add(scopes.Key, Thread.CurrentPrincipal.Identity.Name);
                else
                    scopeToFill.Add(scopes.Key, scopes.Value);
            }
        }


        #endregion
    }
}

