using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000D2 RID: 210
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AutodiscoverClientResponse
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x0002F978 File Offset: 0x0002DB78
		public AutodiscoverClientResponse(Uri autodiscoverUrl, Exception ex, ExchangeVersion exchangeVersion)
		{
			MigrationUtil.ThrowOnNullArgument(ex, "ex");
			this.Status = AutodiscoverClientStatus.ConfigurationError;
			this.ErrorMessage = ServerStrings.MigrationAutodiscoverConfigurationFailure;
			this.ErrorDetail = ex.ToString();
			this.ExchangeVersion = new ExchangeVersion?(exchangeVersion);
			this.EffectiveAutodiscoverUrl = ((autodiscoverUrl == null) ? null : autodiscoverUrl.ToString());
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002F9D8 File Offset: 0x0002DBD8
		public AutodiscoverClientResponse(Uri autodiscoverUrl, string message, AutodiscoverErrorCode errorCode, ExchangeVersion exchangeVersion)
		{
			this.Status = AutodiscoverClientStatus.ConfigurationError;
			this.ErrorMessage = ServerStrings.MigrationAutodiscoverConfigurationFailure;
			this.ExchangeVersion = new ExchangeVersion?(exchangeVersion);
			this.EffectiveAutodiscoverUrl = ((autodiscoverUrl == null) ? null : autodiscoverUrl.ToString());
			this.ErrorDetail = string.Format("Error code:{0} message:'{1}'", errorCode, message);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002FA3C File Offset: 0x0002DC3C
		public AutodiscoverClientResponse(Uri autodiscoverUrl, GetUserSettingsResponse userSettingsResponse, ExchangeVersion exchangeVersion)
		{
			this.Status = AutodiscoverClientStatus.NoError;
			this.ExchangeVersion = new ExchangeVersion?(exchangeVersion);
			this.EffectiveAutodiscoverUrl = ((autodiscoverUrl == null) ? null : autodiscoverUrl.ToString());
			string autodiscoverSetting = AutodiscoverClientResponse.GetAutodiscoverSetting(userSettingsResponse, 4);
			if (autodiscoverSetting == null)
			{
				autodiscoverSetting = AutodiscoverClientResponse.GetAutodiscoverSetting(userSettingsResponse, 3);
			}
			this.MailboxDN = AutodiscoverClientResponse.GetAutodiscoverSetting(userSettingsResponse, 1);
			this.ExchangeServer = autodiscoverSetting;
			this.ExchangeServerDN = AutodiscoverClientResponse.GetAutodiscoverSetting(userSettingsResponse, 5);
			this.RPCProxyServer = AutodiscoverClientResponse.GetAutodiscoverSetting(userSettingsResponse, 29);
			AuthenticationMethod value;
			Enum.TryParse<AuthenticationMethod>(AutodiscoverClientResponse.GetAutodiscoverSetting(userSettingsResponse, 31), out value);
			this.AuthenticationMethod = new AuthenticationMethod?(value);
			if (!this.Validate())
			{
				this.Status = AutodiscoverClientStatus.ConfigurationError;
				this.ErrorMessage = ServerStrings.MigrationAutodiscoverConfigurationFailure;
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002FAF4 File Offset: 0x0002DCF4
		public AutodiscoverClientResponse(MigrationAutodiscoverGetUserSettingsRpcResult result)
		{
			MigrationUtil.ThrowOnNullArgument(result, "result");
			this.Status = result.Status.Value;
			this.ExchangeVersion = result.ExchangeVersion;
			this.EffectiveAutodiscoverUrl = result.AutodiscoverUrl;
			switch (this.Status)
			{
			case AutodiscoverClientStatus.NoError:
				this.MailboxDN = result.MailboxDN;
				this.ExchangeServerDN = result.ExchangeServerDN;
				this.ExchangeServer = result.ExchangeServer;
				this.RPCProxyServer = result.RpcProxyServer;
				this.AuthenticationMethod = result.AuthenticationMethod;
				if (!this.Validate())
				{
					this.Status = AutodiscoverClientStatus.ConfigurationError;
					this.ErrorMessage = ServerStrings.MigrationAutodiscoverConfigurationFailure;
					return;
				}
				return;
			case AutodiscoverClientStatus.ConfigurationError:
				this.ErrorMessage = ServerStrings.MigrationAutodiscoverConfigurationFailure;
				this.ErrorDetail = result.ErrorMessage;
				return;
			}
			this.ErrorMessage = ServerStrings.MigrationAutodiscoverConfigurationFailure;
			this.ErrorDetail = result.ErrorMessage;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002FBE4 File Offset: 0x0002DDE4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"AutodiscoverClientResponse('",
				this.MailboxDN,
				"':'",
				this.ExchangeServerDN,
				"':'",
				this.ExchangeServer,
				"':'",
				this.RPCProxyServer,
				"':'",
				this.ExchangeVersion,
				"':'",
				this.AuthenticationMethod,
				"':'",
				this.EffectiveAutodiscoverUrl,
				"')"
			});
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002FC90 File Offset: 0x0002DE90
		private static string GetAutodiscoverSetting(GetUserSettingsResponse response, UserSettingName key)
		{
			object obj;
			if (response.Settings.TryGetValue(key, out obj))
			{
				return (string)obj;
			}
			return null;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002FCB8 File Offset: 0x0002DEB8
		private bool Validate()
		{
			return !string.IsNullOrEmpty(this.MailboxDN) && !string.IsNullOrEmpty(this.ExchangeServerDN) && this.ExchangeServer != null && this.RPCProxyServer != null && this.AuthenticationMethod != null && this.ExchangeVersion != null;
		}

		// Token: 0x0400044E RID: 1102
		public readonly LocalizedString ErrorMessage;

		// Token: 0x0400044F RID: 1103
		public readonly string MailboxDN;

		// Token: 0x04000450 RID: 1104
		public readonly string ExchangeServerDN;

		// Token: 0x04000451 RID: 1105
		public readonly string ExchangeServer;

		// Token: 0x04000452 RID: 1106
		public readonly string RPCProxyServer;

		// Token: 0x04000453 RID: 1107
		public readonly string EffectiveAutodiscoverUrl;

		// Token: 0x04000454 RID: 1108
		public readonly AutodiscoverClientStatus Status;

		// Token: 0x04000455 RID: 1109
		public readonly ExchangeVersion? ExchangeVersion;

		// Token: 0x04000456 RID: 1110
		public readonly AuthenticationMethod? AuthenticationMethod;

		// Token: 0x04000457 RID: 1111
		public readonly string ErrorDetail;
	}
}
