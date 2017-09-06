using System.Collections.Generic;
using System.Runtime.Serialization;

//TODO: Check Signature
//TODO: Refactor all
//TODO: Property for tracing in UTC

namespace Daenet.Common.Logging
{
    [DataContract(Namespace = "http://www.daenet.de/diagnostics/entities/2014/04")]
    public class LoggingContext
    {
        #region Properties

        [DataMember(EmitDefaultValue = false)]
        public Dictionary<string, string> LoggingScopes { get; set; }

        #endregion

    }

}

