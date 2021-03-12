using System;
using System.Collections.Generic;
using Microsoft.Exchange.Autodiscover.ConfigurationSettings;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200007A RID: 122
	internal sealed class GetDomainSettingsCallContext
	{
		// Token: 0x06000342 RID: 834 RVA: 0x00014F8D File Offset: 0x0001318D
		internal GetDomainSettingsCallContext(string userAgent, ExchangeVersion? requestedVersion, DomainCollection domains, HashSet<DomainConfigurationSettingName> requestedSettings, DomainSettingErrorCollection settingErrors, GetDomainSettingsResponse response)
		{
			this.userAgent = userAgent;
			this.requestedVersion = requestedVersion;
			this.domains = domains;
			this.requestedSettings = requestedSettings;
			this.settingErrors = settingErrors;
			this.response = response;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00014FC2 File Offset: 0x000131C2
		internal string UserAgent
		{
			get
			{
				return this.userAgent;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00014FCA File Offset: 0x000131CA
		internal DomainCollection Domains
		{
			get
			{
				return this.domains;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00014FD2 File Offset: 0x000131D2
		internal HashSet<DomainConfigurationSettingName> RequestedSettings
		{
			get
			{
				return this.requestedSettings;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00014FDA File Offset: 0x000131DA
		internal ExchangeVersion? RequestedVersion
		{
			get
			{
				return this.requestedVersion;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00014FE2 File Offset: 0x000131E2
		internal DomainSettingErrorCollection SettingErrors
		{
			get
			{
				return this.settingErrors;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00014FEA File Offset: 0x000131EA
		internal GetDomainSettingsResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040002FD RID: 765
		private string userAgent;

		// Token: 0x040002FE RID: 766
		private DomainCollection domains;

		// Token: 0x040002FF RID: 767
		private HashSet<DomainConfigurationSettingName> requestedSettings;

		// Token: 0x04000300 RID: 768
		private DomainSettingErrorCollection settingErrors;

		// Token: 0x04000301 RID: 769
		private GetDomainSettingsResponse response;

		// Token: 0x04000302 RID: 770
		private ExchangeVersion? requestedVersion;
	}
}
