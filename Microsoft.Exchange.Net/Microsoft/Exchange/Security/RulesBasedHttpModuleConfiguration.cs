using System;
using System.Configuration;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A2B RID: 2603
	public class RulesBasedHttpModuleConfiguration : ConfigurationSection
	{
		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x00090578 File Offset: 0x0008E778
		public static RulesBasedHttpModuleConfiguration Instance
		{
			get
			{
				if (RulesBasedHttpModuleConfiguration.instance == null)
				{
					try
					{
						RulesBasedHttpModuleConfiguration.instance = (RulesBasedHttpModuleConfiguration)ConfigurationManager.GetSection("RulesBasedHttpModuleConfigurationSection");
					}
					catch (ConfigurationErrorsException ex)
					{
						ExTraceGlobals.RulesBasedHttpModuleTracer.TraceError<string>(0L, "Failed to load RulesBasedHttpModuleConfiguration: {0}", ex.ToString());
					}
					if (RulesBasedHttpModuleConfiguration.instance == null)
					{
						RulesBasedHttpModuleConfiguration.instance = new RulesBasedHttpModuleConfiguration();
					}
				}
				return RulesBasedHttpModuleConfiguration.instance;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x060038B4 RID: 14516 RVA: 0x000905E4 File Offset: 0x0008E7E4
		[ConfigurationProperty("DenyRules", IsRequired = false)]
		public HttpModuleAuthenticationDenyRulesCollection AuthenticationDenyRules
		{
			get
			{
				return base["DenyRules"] as HttpModuleAuthenticationDenyRulesCollection;
			}
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000905F6 File Offset: 0x0008E7F6
		internal bool TryLoad()
		{
			return this.AuthenticationDenyRules.TryLoad();
		}

		// Token: 0x040030B4 RID: 12468
		private static RulesBasedHttpModuleConfiguration instance;

		// Token: 0x02000A2C RID: 2604
		private static class RulesBasedHttpModuleConfigSchema
		{
			// Token: 0x040030B5 RID: 12469
			internal const string SectionName = "RulesBasedHttpModuleConfigurationSection";

			// Token: 0x040030B6 RID: 12470
			internal const string DenyRules = "DenyRules";
		}
	}
}
