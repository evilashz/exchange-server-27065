using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.PushNotifications;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B03 RID: 2819
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	internal class PushNotificationServerSubscription : PushNotificationSubscription
	{
		// Token: 0x0600665F RID: 26207 RVA: 0x001B2572 File Offset: 0x001B0772
		public PushNotificationServerSubscription()
		{
		}

		// Token: 0x06006660 RID: 26208 RVA: 0x001B257C File Offset: 0x001B077C
		public PushNotificationServerSubscription(PushNotificationSubscription subscription, DateTime lastUpdated, string installationId)
		{
			base.AppId = subscription.AppId;
			base.DeviceNotificationId = subscription.DeviceNotificationId;
			base.DeviceNotificationType = subscription.DeviceNotificationType;
			base.InboxUnreadCount = subscription.InboxUnreadCount;
			this.LastSubscriptionUpdate = lastUpdated;
			base.SubscriptionOption = subscription.SubscriptionOption;
			this.InstallationId = installationId;
		}

		// Token: 0x17001C33 RID: 7219
		// (get) Token: 0x06006661 RID: 26209 RVA: 0x001B25D9 File Offset: 0x001B07D9
		// (set) Token: 0x06006662 RID: 26210 RVA: 0x001B25E1 File Offset: 0x001B07E1
		[DataMember(Name = "LastSubscriptionUpdate", EmitDefaultValue = false)]
		public DateTime LastSubscriptionUpdate { get; set; }

		// Token: 0x17001C34 RID: 7220
		// (get) Token: 0x06006663 RID: 26211 RVA: 0x001B25EA File Offset: 0x001B07EA
		// (set) Token: 0x06006664 RID: 26212 RVA: 0x001B25F2 File Offset: 0x001B07F2
		[DataMember(Name = "InstallationId", EmitDefaultValue = false)]
		public string InstallationId { get; set; }

		// Token: 0x06006665 RID: 26213 RVA: 0x001B25FB File Offset: 0x001B07FB
		public new static PushNotificationServerSubscription FromJson(string serializedJson)
		{
			return JsonConverter.Deserialize<PushNotificationServerSubscription>(serializedJson, null);
		}

		// Token: 0x06006666 RID: 26214 RVA: 0x001B2604 File Offset: 0x001B0804
		public override string ToJson()
		{
			return JsonConverter.Serialize<PushNotificationServerSubscription>(this, null);
		}

		// Token: 0x06006667 RID: 26215 RVA: 0x001B2610 File Offset: 0x001B0810
		public override string ToFullString()
		{
			if (this.toFullStringCache == null)
			{
				this.toFullStringCache = string.Format("{{AppId:{0}; DeviceNotificationId:{1}; DeviceNotificationType:{2}; InboxUnreadCount:{3}; LastSubscriptionUpdate:{4}; SubscriptionOption:{5}; InstallationId:{6}}}", new object[]
				{
					base.AppId ?? "null",
					base.DeviceNotificationId ?? "null",
					base.DeviceNotificationType ?? "null",
					(base.InboxUnreadCount != null) ? base.InboxUnreadCount.Value.ToString() : "null",
					this.LastSubscriptionUpdate,
					(base.SubscriptionOption != null) ? base.SubscriptionOption.Value.ToString() : "null",
					this.InstallationId ?? "null"
				});
			}
			return this.toFullStringCache;
		}

		// Token: 0x04003A21 RID: 14881
		[NonSerialized]
		private string toFullStringCache;
	}
}
