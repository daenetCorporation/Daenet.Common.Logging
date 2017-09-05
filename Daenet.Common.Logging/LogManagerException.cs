using System;
using System.Collections.Generic;
using System.Text;

namespace Daenet.Common.Logging
{
    public class LogManagerException : Exception
    {
        /// <summary>
        /// Constructs a DaenetSystemException.
        /// </summary>
        public LogManagerException()
        {
        }

        /// <summary>
        /// Constructs a DaenetSystemException with a message
        /// </summary>
        /// <param name="messageFormat">The message format string.</param>
        /// <param name="args">Arguments for the message format string. See String.Format()</param>
        public LogManagerException(string messageFormat, params object[] args)
            : base(String.Format(System.Globalization.CultureInfo.InvariantCulture, messageFormat, args))
        {
        }

        /// <summary>
        /// Constructs a LogManagerException with a message
        /// </summary>
        /// <param name="innerException"> The inner exception of the currenct expcetion.</param>
        /// <param name="messageFormat">The message format string.</param>
        /// <param name="args">Arguments for the message format string. See String.Format()</param>
        public LogManagerException(Exception innerException, string messageFormat, params object[] args)
            : base(String.Format(System.Globalization.CultureInfo.InvariantCulture, messageFormat, args), innerException)
        {
        }
    }
}
