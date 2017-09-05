using System.Configuration;
using Daenet.Eventing;

namespace Daenet.Configuration
{
    /// <summary>
    /// Provides the scope section in the configuration file.<br/>
    /// It defines all needed information for scopes within log manager.
    /// </summary>
    /// <example>
    /// <code lang="xml" title="scope section">
    ///  &lt;scope name="UserName"/&gt;
    /// &lt;scope name="MachineName"/&gt;
    /// &lt;scope name="Activity"/&gt;
    /// &lt;scope name="Applicaton" value="Test"/&gt;
    /// </code>
    /// </example>
    public class ScopeConfigElement : ConfigurationElement
    {
        /// <summary>
        /// Defines the name of a Scope<br/>
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Defines the value for a new scope.<br/>
        /// </summary>
        [ConfigurationProperty("value", IsRequired = false)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
}