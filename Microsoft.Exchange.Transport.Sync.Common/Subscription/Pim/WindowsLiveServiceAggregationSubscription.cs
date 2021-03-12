using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim
{
	// Token: 0x020000E0 RID: 224
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class WindowsLiveServiceAggregationSubscription : PimAggregationSubscription
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x00020310 File Offset: 0x0001E510
		public WindowsLiveServiceAggregationSubscription()
		{
			this.AuthPolicy = string.Empty;
			this.IncommingServerUrl = string.Empty;
			this.AuthTokenExpirationTime = new DateTime(0L, DateTimeKind.Utc);
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0002033C File Offset: 0x0001E53C
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x00020344 File Offset: 0x0001E544
		public string LogonName
		{
			get
			{
				return this.logonName;
			}
			set
			{
				this.logonName = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0002034D File Offset: 0x0001E54D
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x00020355 File Offset: 0x0001E555
		public string IncommingServerUrl
		{
			get
			{
				return this.incommingServerUrl;
			}
			set
			{
				this.incommingServerUrl = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0002035E File Offset: 0x0001E55E
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x00020366 File Offset: 0x0001E566
		public string AuthPolicy
		{
			get
			{
				return this.authPolicy;
			}
			set
			{
				this.authPolicy = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0002036F File Offset: 0x0001E56F
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x00020377 File Offset: 0x0001E577
		public string Puid
		{
			get
			{
				return this.puid;
			}
			set
			{
				this.puid = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00020380 File Offset: 0x0001E580
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00020388 File Offset: 0x0001E588
		public string AuthToken
		{
			get
			{
				return this.authToken;
			}
			set
			{
				this.authToken = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00020391 File Offset: 0x0001E591
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00020399 File Offset: 0x0001E599
		public DateTime AuthTokenExpirationTime
		{
			get
			{
				return this.authTokenExpirationTime;
			}
			set
			{
				this.authTokenExpirationTime = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x000203A2 File Offset: 0x0001E5A2
		public override bool PasswordRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000203A8 File Offset: 0x0001E5A8
		protected override void SetPropertiesToMessageObject(MessageItem message)
		{
			base.SetPropertiesToMessageObject(message);
			message[AggregationSubscriptionMessageSchema.SharingRemoteUser] = base.UserEmailAddress.ToString();
			message[MessageItemSchema.SharingRemotePath] = this.IncommingServerUrl;
			message[AggregationSubscriptionMessageSchema.SharingWlidAuthPolicy] = this.AuthPolicy;
			if (this.Puid != null)
			{
				message[AggregationSubscriptionMessageSchema.SharingWlidUserPuid] = this.Puid;
			}
			if (this.AuthToken != null)
			{
				message[AggregationSubscriptionMessageSchema.SharingWlidAuthToken] = this.AuthToken;
			}
			message[AggregationSubscriptionMessageSchema.SharingWlidAuthTokenExpireTime] = new ExDateTime(ExTimeZone.UtcTimeZone, this.AuthTokenExpirationTime.ToUniversalTime());
			message[MessageItemSchema.SharingDetail] = (int)((AggregationType)(-49) | base.AggregationType);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00020470 File Offset: 0x0001E670
		protected override void LoadProperties(MessageItem message)
		{
			base.LoadProperties(message);
			base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingRemoteUser, false, new uint?(0U), new uint?(256U), out this.logonName);
			base.GetStringProperty(message, MessageItemSchema.SharingRemotePath, true, null, null, out this.incommingServerUrl);
			if (!string.IsNullOrEmpty(this.incommingServerUrl) && !this.ValidateIncomingServerUrl(this.incommingServerUrl))
			{
				throw new SyncPropertyValidationException(MessageItemSchema.SharingRemotePath.ToString(), this.incommingServerUrl.ToString(), null);
			}
			base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingWlidAuthPolicy, true, null, null, out this.authPolicy);
			ExDateTime exDateTime;
			base.GetExDateTimeProperty(message, AggregationSubscriptionMessageSchema.SharingWlidAuthTokenExpireTime, out exDateTime);
			this.authTokenExpirationTime = (DateTime)exDateTime;
			bool flag = this.LogonPasswordSecured != null && this.LogonPasswordSecured.Length > 0;
			base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingWlidUserPuid, flag, flag, new uint?(0U), new uint?(16U), out this.puid);
			base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingWlidAuthToken, true, true, null, null, out this.authToken);
		}

		// Token: 0x060006A9 RID: 1705
		protected abstract bool ValidateIncomingServerUrl(string incomingServerUrl);

		// Token: 0x04000397 RID: 919
		private const int MaxStringLength = 256;

		// Token: 0x04000398 RID: 920
		private string logonName;

		// Token: 0x04000399 RID: 921
		private string incommingServerUrl;

		// Token: 0x0400039A RID: 922
		private string authPolicy;

		// Token: 0x0400039B RID: 923
		private string puid;

		// Token: 0x0400039C RID: 924
		private string authToken;

		// Token: 0x0400039D RID: 925
		private DateTime authTokenExpirationTime;
	}
}
