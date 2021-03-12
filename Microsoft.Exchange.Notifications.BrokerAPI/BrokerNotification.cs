using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000012 RID: 18
	[KnownType(typeof(BaseNotification))]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(ConsumerId))]
	[Serializable]
	public class BrokerNotification
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003215 File Offset: 0x00001415
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000321D File Offset: 0x0000141D
		[DataMember(EmitDefaultValue = false)]
		public Guid NotificationId { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003226 File Offset: 0x00001426
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000322E File Offset: 0x0000142E
		[DataMember(EmitDefaultValue = false)]
		public ConsumerId ConsumerId { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003237 File Offset: 0x00001437
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000323F File Offset: 0x0000143F
		[DataMember(EmitDefaultValue = false)]
		public Guid SubscriptionId { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003248 File Offset: 0x00001448
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003250 File Offset: 0x00001450
		[DataMember(EmitDefaultValue = false)]
		public string ChannelId { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003259 File Offset: 0x00001459
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003261 File Offset: 0x00001461
		[DataMember(EmitDefaultValue = false)]
		public int SequenceNumber { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000326A File Offset: 0x0000146A
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003272 File Offset: 0x00001472
		[DataMember(EmitDefaultValue = false)]
		public Guid ReceiverMailboxGuid { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000327B File Offset: 0x0000147B
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003283 File Offset: 0x00001483
		[DataMember(EmitDefaultValue = false)]
		public string ReceiverMailboxSmtp { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000328C File Offset: 0x0000148C
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003294 File Offset: 0x00001494
		[DataMember(EmitDefaultValue = false)]
		public DateTime CreationTime { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000329D File Offset: 0x0000149D
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000032A5 File Offset: 0x000014A5
		[DataMember(EmitDefaultValue = false)]
		public BaseNotification Payload { get; set; }

		// Token: 0x06000072 RID: 114 RVA: 0x000032AE File Offset: 0x000014AE
		public static BrokerNotification FromJson(string jsonString)
		{
			return JsonConverter.Deserialize<BrokerNotification>(jsonString, null, JsonConverter.RoundTripDateTimeFormat);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000032BC File Offset: 0x000014BC
		public string ToJson()
		{
			return JsonConverter.Serialize<BrokerNotification>(this, null, JsonConverter.RoundTripDateTimeFormat);
		}
	}
}
