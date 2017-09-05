using System;
using System.Configuration;
using Daenet.Eventing;

namespace Daenet.Configuration
{
    /// <summary>
    /// Provides the scope section in the configuration file.<br/>
    /// It defines all needed information for scopes withing log manager.
    /// </summary>
    /// <example>
    /// <code lang="xml" title="database section for MS-SQL">
    ///  &lt;scope name="UserName"/&gt;
    /// &lt;scope name="MachineName"/&gt;
    /// &lt;scope name="Activity"/&gt;
    /// &lt;scope name="Applicaton" value="Test"/&gt;
    /// </code>
    /// </example>
    [ConfigurationCollection(typeof(ScopeConfigElement), AddItemName = "scope")]
    public class ScopesConfigElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Creates a new ScopeConfigElement.
        /// </summary>
        /// <returns>The new EventMgrConfigElement.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ScopeConfigElement();
        }
               

        /// <summary>
        /// Gets the key for the ScopeConfigElement.
        /// </summary>
        /// <param name="element">The ScopeConfigElement.</param>
        /// <returns>Returns the NAME of the ScopeConfigElement.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ScopeConfigElement)element).Name;
        }

        /// <summary>
        /// Gets the configuration for an scope.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns the configuration element. It Returns NULL if no configurattion is found.</returns>
        public ScopeConfigElement GetScopeConfiguration(string name)
        {
            foreach (ScopeConfigElement mgrConfigElement in this)
            {
                if (mgrConfigElement.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return mgrConfigElement;
            }
            return null;
        }
    }
}