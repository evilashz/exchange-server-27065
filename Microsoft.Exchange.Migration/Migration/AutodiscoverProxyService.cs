using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000D5 RID: 213
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AutodiscoverProxyService : IAutodiscoverService
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x0002FD14 File Offset: 0x0002DF14
		public AutodiscoverProxyService(ExchangeVersion exchangeVersion, NetworkCredential credentials)
		{
			MigrationUtil.ThrowOnNullArgument(credentials, "credentials");
			this.service = new AutodiscoverService(exchangeVersion);
			this.service.IsExternal = new bool?(true);
			this.service.Credentials = credentials;
			this.service.EnableScpLookup = false;
			this.service.RedirectionUrlValidationCallback = new AutodiscoverRedirectionUrlValidationCallback(this.MigrationAutodiscoverRedirectionUrlValidationCallback);
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0002FD83 File Offset: 0x0002DF83
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x0002FD90 File Offset: 0x0002DF90
		public Uri Url
		{
			get
			{
				return this.service.Url;
			}
			set
			{
				this.service.Url = value;
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002FD9E File Offset: 0x0002DF9E
		public GetUserSettingsResponse GetUserSettings(string userSmtpAddress, params UserSettingName[] userSettingNames)
		{
			return this.service.GetUserSettings(userSmtpAddress, userSettingNames);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002FDAD File Offset: 0x0002DFAD
		public bool MigrationAutodiscoverRedirectionUrlValidationCallback(string redirectionUrl)
		{
			return true;
		}

		// Token: 0x0400045C RID: 1116
		private readonly AutodiscoverService service;
	}
}
