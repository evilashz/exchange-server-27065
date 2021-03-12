using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscriptionMessage : InterbrokerMessage
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
		[DataMember(EmitDefaultValue = false)]
		internal BrokerSubscription Subscription { get; set; }

		// Token: 0x0600024E RID: 590 RVA: 0x0000CAF1 File Offset: 0x0000ACF1
		public static SubscriptionMessage FromJson(string jsonString)
		{
			return JsonConverter.Deserialize<SubscriptionMessage>(jsonString, SubscriptionMessage.supportedTypes, JsonConverter.RoundTripDateTimeFormat);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000CB03 File Offset: 0x0000AD03
		public override string ToJson()
		{
			return JsonConverter.Serialize<SubscriptionMessage>(this, SubscriptionMessage.supportedTypes, JsonConverter.RoundTripDateTimeFormat);
		}

		// Token: 0x04000100 RID: 256
		private static Type[] supportedTypes = new Type[]
		{
			typeof(BaseSubscription),
			typeof(BrokerSubscription),
			typeof(NewMailSubscription),
			typeof(string),
			typeof(CultureInfo),
			typeof(Guid),
			typeof(ConsumerId),
			typeof(NotificationType),
			typeof(ExDateTime),
			typeof(StoreObjectId)
		};
	}
}
