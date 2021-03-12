using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200001F RID: 31
	internal static class BrokerSubscriptionSchema
	{
		// Token: 0x060000BF RID: 191 RVA: 0x0000364D File Offset: 0x0000184D
		private static GuidNamePropertyDefinition CreatePropertyDefinition(string propertyName, Type propertyType)
		{
			return GuidNamePropertyDefinition.CreateCustom(propertyName, propertyType, BrokerSubscriptionSchema.PropertySetGuid, propertyName, PropertyFlags.None);
		}

		// Token: 0x04000058 RID: 88
		public static readonly Guid PropertySetGuid = new Guid("4CB44F05-36EA-40E2-87E1-9581CCE7CC6F");

		// Token: 0x04000059 RID: 89
		public static readonly GuidNamePropertyDefinition SubscriptionId = BrokerSubscriptionSchema.CreatePropertyDefinition("SubscriptionId", typeof(Guid));

		// Token: 0x0400005A RID: 90
		public static readonly GuidNamePropertyDefinition ConsumerId = BrokerSubscriptionSchema.CreatePropertyDefinition("ConsumerId", typeof(string));

		// Token: 0x0400005B RID: 91
		public static readonly GuidNamePropertyDefinition ChannelId = BrokerSubscriptionSchema.CreatePropertyDefinition("ChannelId", typeof(string));

		// Token: 0x0400005C RID: 92
		public static readonly GuidNamePropertyDefinition Expiration = BrokerSubscriptionSchema.CreatePropertyDefinition("Expiration", typeof(ExDateTime));

		// Token: 0x0400005D RID: 93
		public static readonly GuidNamePropertyDefinition ReceiverMailboxGuid = BrokerSubscriptionSchema.CreatePropertyDefinition("ReceiverMailboxGuid", typeof(Guid));

		// Token: 0x0400005E RID: 94
		public static readonly GuidNamePropertyDefinition ReceiverMailboxSmtp = BrokerSubscriptionSchema.CreatePropertyDefinition("ReceiverMailboxSmtp", typeof(string));

		// Token: 0x0400005F RID: 95
		public static readonly GuidNamePropertyDefinition ReceiverUrl = BrokerSubscriptionSchema.CreatePropertyDefinition("ReceiverUrl", typeof(string));

		// Token: 0x04000060 RID: 96
		public static readonly GuidNamePropertyDefinition Parameters = BrokerSubscriptionSchema.CreatePropertyDefinition("Parameters", typeof(string));
	}
}
