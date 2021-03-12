using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000205 RID: 517
	internal class PushNotificationAssistantConfigSchema : RegistryObjectSchema
	{
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00073A43 File Offset: 0x00071C43
		public override string DefaultRegistryKeyPath
		{
			get
			{
				return "System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters";
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00073A4A File Offset: 0x00071C4A
		public override string DefaultName
		{
			get
			{
				return "PushNotificationAssistant";
			}
		}

		// Token: 0x04000C17 RID: 3095
		public static readonly RegistryPropertyDefinition IsAssistantPublishingEnabled = new RegistryPropertyDefinition("PublishingEnabled", typeof(bool), false);

		// Token: 0x04000C18 RID: 3096
		public static readonly RegistryPropertyDefinition SubscriptionExpirationInHours = new RegistryPropertyDefinition("SubscriptionExpirationInHours", typeof(uint), PropertyDefinitionFlags.PersistDefaultValue, 72U, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<uint>(1U, 168U)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<uint>(1U, 168U)
		});

		// Token: 0x04000C19 RID: 3097
		public static readonly RegistryPropertyDefinition ProxyWatcherIntervalTimeInMinutes = new RegistryPropertyDefinition("ProxyWatcherIntervalTimeInMinutes", typeof(uint), 15U);
	}
}
