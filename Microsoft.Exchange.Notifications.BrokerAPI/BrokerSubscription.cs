using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class BrokerSubscription
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000034D1 File Offset: 0x000016D1
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000034D9 File Offset: 0x000016D9
		[DataMember(EmitDefaultValue = false)]
		public Guid SubscriptionId { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000034E2 File Offset: 0x000016E2
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000034EA File Offset: 0x000016EA
		[DataMember(EmitDefaultValue = false)]
		public ConsumerId ConsumerId { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000034F3 File Offset: 0x000016F3
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000034FB File Offset: 0x000016FB
		[DataMember(EmitDefaultValue = false)]
		public string ChannelId { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003504 File Offset: 0x00001704
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000350C File Offset: 0x0000170C
		[DataMember(EmitDefaultValue = false)]
		public DateTime Expiration { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003515 File Offset: 0x00001715
		// (set) Token: 0x060000AB RID: 171 RVA: 0x0000351D File Offset: 0x0000171D
		[DataMember(EmitDefaultValue = false)]
		public NotificationParticipant Sender { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003526 File Offset: 0x00001726
		// (set) Token: 0x060000AD RID: 173 RVA: 0x0000352E File Offset: 0x0000172E
		[DataMember(EmitDefaultValue = false)]
		public NotificationParticipant Receiver { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003537 File Offset: 0x00001737
		// (set) Token: 0x060000AF RID: 175 RVA: 0x0000353F File Offset: 0x0000173F
		[DataMember(EmitDefaultValue = false)]
		public BaseSubscription Parameters { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003548 File Offset: 0x00001748
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsValid
		{
			get
			{
				return this.Parameters != null && this.Parameters.IsValid;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000355F File Offset: 0x0000175F
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003567 File Offset: 0x00001767
		[XmlIgnore]
		[IgnoreDataMember]
		internal IBrokerMailboxData MailboxData { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003570 File Offset: 0x00001770
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003578 File Offset: 0x00001778
		[IgnoreDataMember]
		internal StoreObjectId StoreId { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003581 File Offset: 0x00001781
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003589 File Offset: 0x00001789
		[IgnoreDataMember]
		internal INotificationHandler Handler { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003592 File Offset: 0x00001792
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x0000359A File Offset: 0x0000179A
		[IgnoreDataMember]
		internal int LastSequenceNumber
		{
			get
			{
				return this.lastSequenceNumber;
			}
			private set
			{
				this.lastSequenceNumber = value;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000035A3 File Offset: 0x000017A3
		public static BrokerSubscription FromJson(string jsonString)
		{
			return JsonConverter.Deserialize<BrokerSubscription>(jsonString, BrokerSubscription.knownTypes, JsonConverter.RoundTripDateTimeFormat);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000035B5 File Offset: 0x000017B5
		public string ToJson()
		{
			return JsonConverter.Serialize<BrokerSubscription>(this, BrokerSubscription.knownTypes, JsonConverter.RoundTripDateTimeFormat);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000035C7 File Offset: 0x000017C7
		internal int GetNextSequenceNumber()
		{
			return Interlocked.Increment(ref this.lastSequenceNumber);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000035D4 File Offset: 0x000017D4
		internal void TrimForSubscribeRequest()
		{
			this.Sender = this.Sender.AsNotificationSender();
			this.Receiver = this.Receiver.AsNotificationReceiver();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000035F8 File Offset: 0x000017F8
		internal void TrimForUnsubscribeRequest()
		{
			this.Sender = this.Sender.AsNotificationSender();
		}

		// Token: 0x0400004C RID: 76
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(ConsumerId),
			typeof(NotificationType),
			typeof(BaseSubscription)
		};

		// Token: 0x0400004D RID: 77
		private int lastSequenceNumber;
	}
}
