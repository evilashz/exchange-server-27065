using System;
using System.Collections.Generic;
using System.Web.Configuration;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x0200004D RID: 77
	internal class MobileRedirectOptimizationSettings
	{
		// Token: 0x06000227 RID: 551 RVA: 0x0000CDBC File Offset: 0x0000AFBC
		public MobileRedirectOptimizationSettings()
		{
			string text = WebConfigurationManager.AppSettings["MobileSyncRedirectBypassEnabled"];
			this.Enabled = (text != null && text.Equals("true"));
			this.enabledUserAgentPrefixes = new List<string>();
			string text2 = WebConfigurationManager.AppSettings["MobileSyncRedirectBypassClientPrefixes"];
			if (text2 != null)
			{
				string[] array = text2.Split(new char[]
				{
					MobileRedirectOptimizationSettings.clientPrefixListDelimeter
				});
				foreach (string item in array)
				{
					this.enabledUserAgentPrefixes.Add(item);
				}
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000CE58 File Offset: 0x0000B058
		public bool UserAgentEnabled(string userAgent)
		{
			if (this.enabledUserAgentPrefixes != null && userAgent != null)
			{
				foreach (string value in this.enabledUserAgentPrefixes)
				{
					if (userAgent.StartsWith(value, StringComparison.CurrentCultureIgnoreCase))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0400024B RID: 587
		public readonly bool Enabled;

		// Token: 0x0400024C RID: 588
		private static char clientPrefixListDelimeter = ',';

		// Token: 0x0400024D RID: 589
		private List<string> enabledUserAgentPrefixes;
	}
}
