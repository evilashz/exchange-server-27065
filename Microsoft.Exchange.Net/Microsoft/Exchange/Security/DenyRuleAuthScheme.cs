using System;
using System.Configuration;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A34 RID: 2612
	internal class DenyRuleAuthScheme : ConfigurationElement
	{
		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x060038CF RID: 14543 RVA: 0x00090B2C File Offset: 0x0008ED2C
		internal DenyRuleAuthenticationType AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x00090B34 File Offset: 0x0008ED34
		// (set) Token: 0x060038D1 RID: 14545 RVA: 0x00090B46 File Offset: 0x0008ED46
		[ConfigurationProperty("Value", IsRequired = true)]
		public string Value
		{
			get
			{
				return (string)base["Value"];
			}
			set
			{
				base["Value"] = value;
			}
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x00090B54 File Offset: 0x0008ED54
		protected override void DeserializeElement(XmlReader reader, bool s)
		{
			this.Value = (reader.ReadElementContentAs(typeof(string), null) as string);
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x00090B74 File Offset: 0x0008ED74
		internal bool TryLoad()
		{
			bool result = true;
			if (!Enum.TryParse<DenyRuleAuthenticationType>(this.Value, true, out this.authenticationType))
			{
				ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string>((long)this.GetHashCode(), "[DenyRuleAuthScheme.TryLoad] Authentication type {0} is not supported", this.Value);
				ExEventLog eventLogger = RulesBasedHttpModule.EventLogger;
				ExEventLog.EventTuple tuple_RulesBasedHttpModule_InvalidRuleConfigured = CommonEventLogConstants.Tuple_RulesBasedHttpModule_InvalidRuleConfigured;
				string value = this.Value;
				object[] array = new object[2];
				array[0] = "Auth Scheme";
				eventLogger.LogEvent(tuple_RulesBasedHttpModule_InvalidRuleConfigured, value, array);
				return false;
			}
			return result;
		}

		// Token: 0x040030D9 RID: 12505
		private DenyRuleAuthenticationType authenticationType;
	}
}
