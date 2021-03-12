using System;
using System.Threading;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000206 RID: 518
	internal class PushNotificationAssistantConfig : RegistryObject
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00073AEE File Offset: 0x00071CEE
		public bool IsPublishingEnabled
		{
			get
			{
				return this.IsAssistantPublishingEnabled || this.IsProxyPublishingEnabled;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x00073B00 File Offset: 0x00071D00
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x00073B12 File Offset: 0x00071D12
		public bool IsAssistantPublishingEnabled
		{
			get
			{
				return (bool)this[PushNotificationAssistantConfigSchema.IsAssistantPublishingEnabled];
			}
			set
			{
				this[PushNotificationAssistantConfigSchema.IsAssistantPublishingEnabled] = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00073B2D File Offset: 0x00071D2D
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x00073B3F File Offset: 0x00071D3F
		public uint SubscriptionExpirationInHours
		{
			get
			{
				return (uint)this[PushNotificationAssistantConfigSchema.SubscriptionExpirationInHours];
			}
			set
			{
				this[PushNotificationAssistantConfigSchema.SubscriptionExpirationInHours] = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00073B52 File Offset: 0x00071D52
		// (set) Token: 0x060013E9 RID: 5097 RVA: 0x00073B64 File Offset: 0x00071D64
		public uint ProxyWatcherIntervalTimeInMinutes
		{
			get
			{
				return (uint)this[PushNotificationAssistantConfigSchema.ProxyWatcherIntervalTimeInMinutes];
			}
			set
			{
				this[PushNotificationAssistantConfigSchema.ProxyWatcherIntervalTimeInMinutes] = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00073B77 File Offset: 0x00071D77
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x00073B8C File Offset: 0x00071D8C
		public bool IsProxyPublishingEnabled
		{
			get
			{
				return Interlocked.CompareExchange(ref this.isProxyPublishingEnabled, 0, 0) == 1;
			}
			set
			{
				int num = Interlocked.Exchange(ref this.isProxyPublishingEnabled, value ? 1 : 0);
				if (num > 0 != value)
				{
					PushNotificationHelper.LogAssistantPublishingStatus(value);
				}
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00073BB9 File Offset: 0x00071DB9
		internal override RegistryObjectSchema RegistrySchema
		{
			get
			{
				return PushNotificationAssistantConfig.schema;
			}
		}

		// Token: 0x04000C1A RID: 3098
		private static readonly PushNotificationAssistantConfigSchema schema = ObjectSchema.GetInstance<PushNotificationAssistantConfigSchema>();

		// Token: 0x04000C1B RID: 3099
		private int isProxyPublishingEnabled;
	}
}
