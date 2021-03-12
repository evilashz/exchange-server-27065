using System;
using System.Configuration;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A38 RID: 2616
	internal class DenyRulePort : ConfigurationElement
	{
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x060038EA RID: 14570 RVA: 0x00091109 File Offset: 0x0008F309
		// (set) Token: 0x060038EB RID: 14571 RVA: 0x0009111B File Offset: 0x0008F31B
		[ConfigurationProperty("Value", IsRequired = true)]
		public int Value
		{
			get
			{
				return (int)base["Value"];
			}
			set
			{
				base["Value"] = value;
			}
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x00091130 File Offset: 0x0008F330
		protected override void DeserializeElement(XmlReader reader, bool s)
		{
			string text = reader.ReadElementContentAs(typeof(string), null) as string;
			int value;
			if (!int.TryParse(text, out value))
			{
				ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string>((long)this.GetHashCode(), "[PortCollection.DeserializeElement] Invalid Server Port {0}", text);
				ExEventLog eventLogger = RulesBasedHttpModule.EventLogger;
				ExEventLog.EventTuple tuple_RulesBasedHttpModule_InvalidRuleConfigured = CommonEventLogConstants.Tuple_RulesBasedHttpModule_InvalidRuleConfigured;
				string periodicKey = text;
				object[] array = new object[2];
				array[0] = "Port";
				eventLogger.LogEvent(tuple_RulesBasedHttpModule_InvalidRuleConfigured, periodicKey, array);
			}
			this.Value = value;
		}
	}
}
