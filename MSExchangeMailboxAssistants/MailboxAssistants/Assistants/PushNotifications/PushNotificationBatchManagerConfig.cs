using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x0200020A RID: 522
	internal class PushNotificationBatchManagerConfig : RegistryObject
	{
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00074030 File Offset: 0x00072230
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x00074042 File Offset: 0x00072242
		public uint BatchSize
		{
			get
			{
				return (uint)this[PushNotificationBatchManagerConfigSchema.NotificationBatchSize];
			}
			set
			{
				this[PushNotificationBatchManagerConfigSchema.NotificationBatchSize] = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00074055 File Offset: 0x00072255
		// (set) Token: 0x0600140D RID: 5133 RVA: 0x00074067 File Offset: 0x00072267
		public uint BatchTimerInSeconds
		{
			get
			{
				return (uint)this[PushNotificationBatchManagerConfigSchema.NotificationProcessingTimeInSeconds];
			}
			set
			{
				this[PushNotificationBatchManagerConfigSchema.NotificationProcessingTimeInSeconds] = value;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x0007407A File Offset: 0x0007227A
		internal override RegistryObjectSchema RegistrySchema
		{
			get
			{
				return PushNotificationBatchManagerConfig.schema;
			}
		}

		// Token: 0x04000C24 RID: 3108
		private static readonly PushNotificationBatchManagerConfigSchema schema = ObjectSchema.GetInstance<PushNotificationBatchManagerConfigSchema>();
	}
}
