using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A3A RID: 2618
	internal class DenyRuleUserPattern : ConfigurationElement
	{
		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x00091350 File Offset: 0x0008F550
		// (set) Token: 0x060038F6 RID: 14582 RVA: 0x00091362 File Offset: 0x0008F562
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

		// Token: 0x060038F7 RID: 14583 RVA: 0x00091370 File Offset: 0x0008F570
		protected override void DeserializeElement(XmlReader reader, bool s)
		{
			this.Value = (reader.ReadElementContentAs(typeof(string), null) as string);
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x00091390 File Offset: 0x0008F590
		internal bool TryLoad()
		{
			try
			{
				this.denyRuleUserPattern = new Regex(this.Value, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			}
			catch (ArgumentNullException ex)
			{
				ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string, ArgumentNullException>((long)this.GetHashCode(), "[DenyRuleUserPattern.TryLoad] Exception encountered while parsing regex {0}: {1}", this.Value, ex);
				RulesBasedHttpModule.EventLogger.LogEvent(CommonEventLogConstants.Tuple_RulesBasedHttpModule_InvalidRuleConfigured, this.Value, new object[]
				{
					"User pattern",
					ex
				});
				return false;
			}
			catch (ArgumentException ex2)
			{
				ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string, ArgumentException>((long)this.GetHashCode(), "[HttpModuleAuthenticationDenyRule.TryLoad] Exception encountered while parsing regex {0}: {1}", this.Value, ex2);
				RulesBasedHttpModule.EventLogger.LogEvent(CommonEventLogConstants.Tuple_RulesBasedHttpModule_InvalidRuleConfigured, this.Value, new object[]
				{
					"User pattern",
					ex2
				});
				return false;
			}
			return true;
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x00091470 File Offset: 0x0008F670
		internal bool Evaluate(string userName)
		{
			return this.denyRuleUserPattern.IsMatch(userName);
		}

		// Token: 0x040030DF RID: 12511
		private Regex denyRuleUserPattern;
	}
}
