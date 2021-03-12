using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A3C RID: 2620
	internal class DenyRuleCookiePattern : ConfigurationElement
	{
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06003902 RID: 14594 RVA: 0x00091638 File Offset: 0x0008F838
		// (set) Token: 0x06003903 RID: 14595 RVA: 0x0009164A File Offset: 0x0008F84A
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

		// Token: 0x06003904 RID: 14596 RVA: 0x00091658 File Offset: 0x0008F858
		protected override void DeserializeElement(XmlReader reader, bool s)
		{
			this.Value = (reader.ReadElementContentAs(typeof(string), null) as string);
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x00091678 File Offset: 0x0008F878
		internal bool TryLoad()
		{
			try
			{
				this.DenyRuleCookiePatternRegex = new Regex(this.Value, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			}
			catch (ArgumentNullException ex)
			{
				ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string, ArgumentNullException>((long)this.GetHashCode(), "[DenyRuleCookiePattern.TryLoad] Exception encountered while parsing regex {0}: {1}", this.Value, ex);
				RulesBasedHttpModule.EventLogger.LogEvent(CommonEventLogConstants.Tuple_RulesBasedHttpModule_InvalidRuleConfigured, this.Value, new object[]
				{
					"Cookie pattern",
					ex
				});
				return false;
			}
			catch (ArgumentException ex2)
			{
				ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string, ArgumentException>((long)this.GetHashCode(), "[HttpModuleAuthenticationDenyRule.TryLoad] Exception encountered while parsing regex {0}: {1}", this.Value, ex2);
				RulesBasedHttpModule.EventLogger.LogEvent(CommonEventLogConstants.Tuple_RulesBasedHttpModule_InvalidRuleConfigured, this.Value, new object[]
				{
					"Cookie pattern",
					ex2
				});
				return false;
			}
			return true;
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x00091758 File Offset: 0x0008F958
		internal bool Evaluate(string cookieForCurrentRequest)
		{
			return this.DenyRuleCookiePatternRegex.IsMatch(cookieForCurrentRequest);
		}

		// Token: 0x040030E1 RID: 12513
		private Regex DenyRuleCookiePatternRegex;
	}
}
