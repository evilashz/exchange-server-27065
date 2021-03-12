using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200062F RID: 1583
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", TypeName = "ReminderMessageDataType")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ReminderMessageDataType
	{
		// Token: 0x06003152 RID: 12626 RVA: 0x000B6F6E File Offset: 0x000B516E
		public ReminderMessageDataType()
		{
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000B6F78 File Offset: 0x000B5178
		internal ReminderMessageDataType(ReminderMessage reminderMessage)
		{
			this.ReminderText = reminderMessage.GetValueOrDefault<string>(ReminderMessageSchema.ReminderText);
			this.Location = reminderMessage.GetValueOrDefault<string>(CalendarItemBaseSchema.Location);
			ExDateTime valueOrDefault = reminderMessage.GetValueOrDefault<ExDateTime>(ReminderMessageSchema.ReminderStartTime);
			this.StartTime = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(valueOrDefault);
			ExDateTime valueOrDefault2 = reminderMessage.GetValueOrDefault<ExDateTime>(ReminderMessageSchema.ReminderEndTime);
			this.EndTime = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(valueOrDefault2);
			CalendarItemBase cachedCorrelatedOccurrence = reminderMessage.GetCachedCorrelatedOccurrence();
			if (cachedCorrelatedOccurrence != null)
			{
				IdAndSession idAndSession = new IdAndSession(cachedCorrelatedOccurrence.Id, cachedCorrelatedOccurrence.Session);
				ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(idAndSession.Id, idAndSession, null);
				this.AssociatedCalendarItemId = new ItemId(concatenatedId.Id, concatenatedId.ChangeKey);
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06003154 RID: 12628 RVA: 0x000B7021 File Offset: 0x000B5221
		// (set) Token: 0x06003155 RID: 12629 RVA: 0x000B7029 File Offset: 0x000B5229
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string ReminderText { get; set; }

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x000B7032 File Offset: 0x000B5232
		// (set) Token: 0x06003157 RID: 12631 RVA: 0x000B703A File Offset: 0x000B523A
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string Location { get; set; }

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000B7043 File Offset: 0x000B5243
		// (set) Token: 0x06003159 RID: 12633 RVA: 0x000B704B File Offset: 0x000B524B
		[DataMember(EmitDefaultValue = false, Order = 3)]
		[DateTimeString]
		public string StartTime { get; set; }

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x000B7054 File Offset: 0x000B5254
		// (set) Token: 0x0600315B RID: 12635 RVA: 0x000B705C File Offset: 0x000B525C
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string EndTime { get; set; }

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x000B7065 File Offset: 0x000B5265
		// (set) Token: 0x0600315D RID: 12637 RVA: 0x000B706D File Offset: 0x000B526D
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public ItemId AssociatedCalendarItemId { get; set; }
	}
}
