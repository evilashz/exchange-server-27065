using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000055 RID: 85
	internal class PushNotificationSubscriptionSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040000FD RID: 253
		internal static SimpleProviderPropertyDefinition SubscriptionStoreId = new SimpleProviderPropertyDefinition("SubscriptionStoreId", ExchangeObjectVersion.Exchange2003, typeof(PushNotificationStoreId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000FE RID: 254
		internal static SimpleProviderPropertyDefinition SubscriptionId = new SimpleProviderPropertyDefinition("SubscriptionId", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, string.Empty, new PropertyDefinitionConstraint[]
		{
			new NotNullOrEmptyConstraint()
		}, new PropertyDefinitionConstraint[]
		{
			new NotNullOrEmptyConstraint()
		});

		// Token: 0x040000FF RID: 255
		internal static SimpleProviderPropertyDefinition DeserializedSubscription = new SimpleProviderPropertyDefinition("DeserializedSubscription", ExchangeObjectVersion.Exchange2003, typeof(PushNotificationServerSubscription), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000100 RID: 256
		internal static SimpleProviderPropertyDefinition AppId = new SimpleProviderPropertyDefinition("AppId", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PushNotificationSubscriptionSchema.DeserializedSubscription
		}, null, new GetterDelegate(PushNotificationSubscription.AppIdGetter), null);

		// Token: 0x04000101 RID: 257
		internal static SimpleProviderPropertyDefinition DeviceNotificationId = new SimpleProviderPropertyDefinition("DeviceNotificationId", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PushNotificationSubscriptionSchema.DeserializedSubscription
		}, null, new GetterDelegate(PushNotificationSubscription.DeviceNotificationIdGetter), null);

		// Token: 0x04000102 RID: 258
		internal static SimpleProviderPropertyDefinition DeviceNotificationType = new SimpleProviderPropertyDefinition("DeviceNotificationType", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PushNotificationSubscriptionSchema.DeserializedSubscription
		}, null, new GetterDelegate(PushNotificationSubscription.DeviceNotificationTypeGetter), null);

		// Token: 0x04000103 RID: 259
		internal static SimpleProviderPropertyDefinition InboxUnreadCount = new SimpleProviderPropertyDefinition("InboxUnreadCount", ExchangeObjectVersion.Exchange2003, typeof(long?), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PushNotificationSubscriptionSchema.DeserializedSubscription
		}, null, new GetterDelegate(PushNotificationSubscription.InboxUnreadCountGetter), null);

		// Token: 0x04000104 RID: 260
		internal static SimpleProviderPropertyDefinition LastSubscriptionUpdate = new SimpleProviderPropertyDefinition("LastSubscriptionUpdate", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PushNotificationSubscriptionSchema.DeserializedSubscription
		}, null, new GetterDelegate(PushNotificationSubscription.LastSubscriptionUpdateGetter), null);
	}
}
