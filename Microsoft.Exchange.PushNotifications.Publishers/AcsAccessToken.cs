using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PushNotifications.Extensions;
using Microsoft.Exchange.PushNotifications.Utils;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000079 RID: 121
	internal sealed class AcsAccessToken
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x0000DF18 File Offset: 0x0000C118
		public AcsAccessToken(string accessToken)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("accessToken", accessToken);
			Dictionary<string, string> dictionary = AcsAccessToken.PropertyReader.Read(accessToken);
			ArgumentValidator.ThrowIfInvalidValue<Dictionary<string, string>>("parse(accessToken)", dictionary, (Dictionary<string, string> x) => x != null && x.ContainsKey("wrap_access_token"));
			this.AccessToken = dictionary["wrap_access_token"];
			this.ParseAdditionalPropertiesFromAccessToken();
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000DF81 File Offset: 0x0000C181
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000DF89 File Offset: 0x0000C189
		public string AccessToken { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000DF92 File Offset: 0x0000C192
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000DF9A File Offset: 0x0000C19A
		public ExDateTime? ExpirationTime { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000DFA3 File Offset: 0x0000C1A3
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000DFAB File Offset: 0x0000C1AB
		public string Issuer { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000DFB4 File Offset: 0x0000C1B4
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		public string Audience { get; private set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000DFC5 File Offset: 0x0000C1C5
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0000DFCD File Offset: 0x0000C1CD
		public string Action { get; private set; }

		// Token: 0x06000440 RID: 1088 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
		public bool IsValid()
		{
			return this.ExpirationTime != null && this.ExpirationTime.Value > ExDateTime.UtcNow;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000E00F File Offset: 0x0000C20F
		public string ToAzureAuthorizationString()
		{
			if (this.toAzureAuthorizationString == null)
			{
				this.toAzureAuthorizationString = string.Format("WRAP access_token=\"{0}\"", this.AccessToken.ToString());
			}
			return this.toAzureAuthorizationString;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000E03C File Offset: 0x0000C23C
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Format("Issuer:{0}; Action:{1}; Audience:{2}; Expiration:{3}", new object[]
				{
					this.Issuer.ToNullableString(),
					this.Action.ToNullableString(),
					this.Audience.ToNullableString(),
					this.ExpirationTime.ToNullableString<ExDateTime>()
				});
			}
			return this.toString;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000E0A8 File Offset: 0x0000C2A8
		private void ParseAdditionalPropertiesFromAccessToken()
		{
			Dictionary<string, string> dictionary = AcsAccessToken.PropertyReader.Read(this.AccessToken);
			int num;
			if (dictionary.ContainsKey("ExpiresOn") && int.TryParse(dictionary["ExpiresOn"], out num))
			{
				this.ExpirationTime = new ExDateTime?(Constants.EpochBaseTime.AddSeconds((double)num));
			}
			if (dictionary.ContainsKey("Issuer"))
			{
				this.Issuer = dictionary["Issuer"];
			}
			if (dictionary.ContainsKey("net.windows.servicebus.action"))
			{
				this.Action = dictionary["net.windows.servicebus.action"];
			}
			if (dictionary.ContainsKey("Audience"))
			{
				this.Audience = dictionary["Audience"];
			}
		}

		// Token: 0x040001F1 RID: 497
		public const string PropertySeparator = "&";

		// Token: 0x040001F2 RID: 498
		public const string PropertyValueSeparator = "=";

		// Token: 0x040001F3 RID: 499
		private const string ExpirationPropertyName = "ExpiresOn";

		// Token: 0x040001F4 RID: 500
		private const string IssuerPropertyName = "Issuer";

		// Token: 0x040001F5 RID: 501
		private const string AudiencePropertyName = "Audience";

		// Token: 0x040001F6 RID: 502
		private const string ActionPropertyName = "net.windows.servicebus.action";

		// Token: 0x040001F7 RID: 503
		private const string WrappedAccessTokenProperty = "wrap_access_token";

		// Token: 0x040001F8 RID: 504
		private static readonly PropertyReader PropertyReader = new PropertyReader(new string[]
		{
			"&"
		}, new string[]
		{
			"="
		});

		// Token: 0x040001F9 RID: 505
		private string toAzureAuthorizationString;

		// Token: 0x040001FA RID: 506
		private string toString;
	}
}
