using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000209 RID: 521
	internal class PushNotificationBatchManagerConfigSchema : RegistryObjectSchema
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00073FCA File Offset: 0x000721CA
		public override string DefaultRegistryKeyPath
		{
			get
			{
				return "System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters";
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x00073FD1 File Offset: 0x000721D1
		public override string DefaultName
		{
			get
			{
				return "PushNotificationAssistant";
			}
		}

		// Token: 0x04000C22 RID: 3106
		public static readonly RegistryPropertyDefinition NotificationBatchSize = new RegistryPropertyDefinition("NotificationBatchSize", typeof(uint), 256U);

		// Token: 0x04000C23 RID: 3107
		public static readonly RegistryPropertyDefinition NotificationProcessingTimeInSeconds = new RegistryPropertyDefinition("NotificationProcessingTimeInSeconds", typeof(uint), 60U);
	}
}
