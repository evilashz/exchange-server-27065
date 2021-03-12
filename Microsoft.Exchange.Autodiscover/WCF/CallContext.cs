using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Autodiscover.ConfigurationSettings;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000079 RID: 121
	internal sealed class CallContext
	{
		// Token: 0x0600032F RID: 815 RVA: 0x00014E4C File Offset: 0x0001304C
		internal CallContext(HttpContext httpContext, UserCollection users, HashSet<UserConfigurationSettingName> requestedSettings, ExchangeServerVersion? requestedVersion, UserSettingErrorCollection settingErrors, GetUserSettingsResponse response)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (httpContext.Request == null)
			{
				throw new ArgumentException("The specified HTTP context has the Request property null", "httpContext");
			}
			this.Users = users;
			this.RequestedSettings = requestedSettings;
			this.RequestedVersion = requestedVersion;
			this.SettingErrors = settingErrors;
			this.Response = response;
			this.UseClientCertificateAuthentication = Common.CheckClientCertificate(httpContext.Request);
			this.UserAgent = httpContext.Request.UserAgent;
			this.UserAuthType = httpContext.Request.ServerVariables["AUTH_TYPE"];
			this.CallerCapabilities = CallerRequestedCapabilities.GetInstance(httpContext);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00014EF4 File Offset: 0x000130F4
		// (set) Token: 0x06000331 RID: 817 RVA: 0x00014EFC File Offset: 0x000130FC
		internal string UserAgent { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00014F05 File Offset: 0x00013105
		// (set) Token: 0x06000333 RID: 819 RVA: 0x00014F0D File Offset: 0x0001310D
		internal string UserAuthType { get; private set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00014F16 File Offset: 0x00013116
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00014F1E File Offset: 0x0001311E
		internal CallerRequestedCapabilities CallerCapabilities { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00014F27 File Offset: 0x00013127
		// (set) Token: 0x06000337 RID: 823 RVA: 0x00014F2F File Offset: 0x0001312F
		internal UserCollection Users { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00014F38 File Offset: 0x00013138
		// (set) Token: 0x06000339 RID: 825 RVA: 0x00014F40 File Offset: 0x00013140
		internal HashSet<UserConfigurationSettingName> RequestedSettings { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00014F49 File Offset: 0x00013149
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00014F51 File Offset: 0x00013151
		internal ExchangeServerVersion? RequestedVersion { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00014F5A File Offset: 0x0001315A
		// (set) Token: 0x0600033D RID: 829 RVA: 0x00014F62 File Offset: 0x00013162
		internal UserSettingErrorCollection SettingErrors { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00014F6B File Offset: 0x0001316B
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00014F73 File Offset: 0x00013173
		internal GetUserSettingsResponse Response { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00014F7C File Offset: 0x0001317C
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00014F84 File Offset: 0x00013184
		internal bool UseClientCertificateAuthentication { get; private set; }
	}
}
