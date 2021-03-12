using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200002C RID: 44
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(BrokerNotification))]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class NotificationMessage : InterbrokerMessage
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00009F10 File Offset: 0x00008110
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00009F18 File Offset: 0x00008118
		[DataMember(EmitDefaultValue = false)]
		public IEnumerable<BrokerNotification> Notifications { get; set; }

		// Token: 0x060001C0 RID: 448 RVA: 0x00009F21 File Offset: 0x00008121
		public static NotificationMessage FromJson(string jsonString)
		{
			return JsonConverter.Deserialize<NotificationMessage>(jsonString, null, JsonConverter.RoundTripDateTimeFormat);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009F2F File Offset: 0x0000812F
		public override string ToJson()
		{
			return JsonConverter.Serialize<NotificationMessage>(this, null, JsonConverter.RoundTripDateTimeFormat);
		}
	}
}
