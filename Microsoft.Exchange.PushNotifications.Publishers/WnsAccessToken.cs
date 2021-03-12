using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000D5 RID: 213
	[DataContract]
	internal sealed class WnsAccessToken
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x00015D34 File Offset: 0x00013F34
		public WnsAccessToken(string accessToken, string tokenType)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("accessToken", accessToken);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("tokenType", tokenType);
			this.AccessToken = accessToken;
			this.TokenType = tokenType;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00015D60 File Offset: 0x00013F60
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x00015D68 File Offset: 0x00013F68
		[DataMember(Name = "access_token", IsRequired = true, EmitDefaultValue = false)]
		public string AccessToken
		{
			get
			{
				return this.accessToken;
			}
			private set
			{
				this.accessToken = value;
				this.issuingTime = ExDateTime.UtcNow;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00015D7C File Offset: 0x00013F7C
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x00015D84 File Offset: 0x00013F84
		[DataMember(Name = "token_type", IsRequired = true, EmitDefaultValue = false)]
		public string TokenType { get; private set; }

		// Token: 0x060006F1 RID: 1777 RVA: 0x00015D90 File Offset: 0x00013F90
		public int GetUsageTimeInMinutes()
		{
			double totalMinutes = ExDateTime.UtcNow.Subtract(this.issuingTime).TotalMinutes;
			if (totalMinutes <= 2147483647.0)
			{
				return (int)totalMinutes;
			}
			return int.MaxValue;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00015DCD File Offset: 0x00013FCD
		public string ToWnsAuthorizationString()
		{
			if (this.toWnsAuthorizationString == null)
			{
				this.toWnsAuthorizationString = string.Format("Bearer {0}", this.AccessToken.ToNullableString());
			}
			return this.toWnsAuthorizationString;
		}

		// Token: 0x0400037C RID: 892
		private string accessToken;

		// Token: 0x0400037D RID: 893
		private ExDateTime issuingTime;

		// Token: 0x0400037E RID: 894
		private string toWnsAuthorizationString;
	}
}
