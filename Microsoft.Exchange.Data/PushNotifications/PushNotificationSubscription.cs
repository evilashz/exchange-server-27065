using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Data.PushNotifications
{
	// Token: 0x0200026D RID: 621
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class PushNotificationSubscription
	{
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00042B17 File Offset: 0x00040D17
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x00042B1F File Offset: 0x00040D1F
		[DataMember(Name = "AppId", IsRequired = true)]
		public string AppId { get; set; }

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00042B28 File Offset: 0x00040D28
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x00042B30 File Offset: 0x00040D30
		[DataMember(Name = "DeviceNotificationId", IsRequired = true)]
		public string DeviceNotificationId { get; set; }

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00042B39 File Offset: 0x00040D39
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x00042B41 File Offset: 0x00040D41
		[DataMember(Name = "DeviceNotificationType", IsRequired = true)]
		public string DeviceNotificationType { get; set; }

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x00042B4A File Offset: 0x00040D4A
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x00042B52 File Offset: 0x00040D52
		[DataMember(Name = "InboxUnreadCount", EmitDefaultValue = false)]
		public long? InboxUnreadCount { get; set; }

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00042B5B File Offset: 0x00040D5B
		// (set) Token: 0x060014CB RID: 5323 RVA: 0x00042B63 File Offset: 0x00040D63
		[DataMember(Name = "SubscriptionOption", IsRequired = false, EmitDefaultValue = false)]
		public PushNotificationSubscriptionOption? SubscriptionOption { get; set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00042B6C File Offset: 0x00040D6C
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x00042B74 File Offset: 0x00040D74
		[DataMember(Name = "RegistrationChallenge", IsRequired = false)]
		public string RegistrationChallenge { get; set; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x00042B80 File Offset: 0x00040D80
		public PushNotificationPlatform Platform
		{
			get
			{
				if (this.platform == null)
				{
					PushNotificationPlatform pushNotificationPlatform;
					this.platform = new PushNotificationPlatform?(Enum.TryParse<PushNotificationPlatform>(this.DeviceNotificationType, out pushNotificationPlatform) ? pushNotificationPlatform : PushNotificationPlatform.None);
				}
				return this.platform.Value;
			}
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00042BC3 File Offset: 0x00040DC3
		public static PushNotificationSubscription FromJson(string serializedJson)
		{
			return JsonConverter.Deserialize<PushNotificationSubscription>(serializedJson, null);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00042BCC File Offset: 0x00040DCC
		public virtual string ToJson()
		{
			return JsonConverter.Serialize<PushNotificationSubscription>(this, null);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00042BD8 File Offset: 0x00040DD8
		public virtual string ToFullString()
		{
			if (this.toFullStringCache == null)
			{
				this.toFullStringCache = string.Format("{{AppId:{0}; DeviceNotificationId:{1}; DeviceNotificationType:{2}; InboxUnreadCount:{3}; SubscriptionOption:{4}; RegistrationChallenge:{5}}}", new object[]
				{
					this.AppId ?? "null",
					this.DeviceNotificationId ?? "null",
					this.DeviceNotificationType ?? "null",
					(this.InboxUnreadCount != null) ? this.InboxUnreadCount.Value.ToString() : "null",
					(this.SubscriptionOption != null) ? this.SubscriptionOption.Value.ToString() : "null",
					this.RegistrationChallenge ?? "null"
				});
			}
			return this.toFullStringCache;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00042CBC File Offset: 0x00040EBC
		public PushNotificationSubscriptionOption GetSubscriptionOption()
		{
			if (this.SubscriptionOption != null)
			{
				return this.SubscriptionOption.Value;
			}
			return PushNotificationSubscriptionOption.Email | PushNotificationSubscriptionOption.Calendar | PushNotificationSubscriptionOption.VoiceMail | PushNotificationSubscriptionOption.MissedCall | PushNotificationSubscriptionOption.BackgroundSync;
		}

		// Token: 0x04000C25 RID: 3109
		private PushNotificationPlatform? platform;

		// Token: 0x04000C26 RID: 3110
		[NonSerialized]
		private string toFullStringCache;
	}
}
