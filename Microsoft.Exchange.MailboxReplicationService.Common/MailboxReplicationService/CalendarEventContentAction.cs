using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000084 RID: 132
	[DataContract]
	internal abstract class CalendarEventContentAction : ReplayAction
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x0000A888 File Offset: 0x00008A88
		protected CalendarEventContentAction()
		{
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0000A890 File Offset: 0x00008A90
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0000A898 File Offset: 0x00008A98
		[DataMember]
		public string EventData { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000A8A1 File Offset: 0x00008AA1
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0000A8A9 File Offset: 0x00008AA9
		[DataMember]
		public IList<string> ExceptionalEventsData { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000A8B2 File Offset: 0x00008AB2
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0000A8BA File Offset: 0x00008ABA
		[DataMember]
		public IList<string> DeletedOccurrences { get; set; }

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		protected CalendarEventContentAction(byte[] itemId, byte[] folderId, string watermark, Event theEvent, IList<Event> exceptionalOccurrences = null, IList<string> deletedOccurrences = null) : base(watermark)
		{
			base.ItemId = itemId;
			base.FolderId = folderId;
			this.EventData = EntitySerializer.Serialize<Event>(theEvent);
			this.DeletedOccurrences = deletedOccurrences;
			if (exceptionalOccurrences != null)
			{
				List<string> list = new List<string>();
				foreach (Event obj in exceptionalOccurrences)
				{
					list.Add(EntitySerializer.Serialize<Event>(obj));
				}
				this.ExceptionalEventsData = list;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000A950 File Offset: 0x00008B50
		public Event Event
		{
			get
			{
				if (this.deserializedEvent == null && this.EventData != null)
				{
					this.deserializedEvent = EntitySerializer.Deserialize<Event>(this.EventData);
				}
				return this.deserializedEvent;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0000A97C File Offset: 0x00008B7C
		public IList<Event> ExceptionalEvents
		{
			get
			{
				if (this.deserializedExceptionalEvents == null && this.ExceptionalEventsData != null)
				{
					List<Event> list = new List<Event>(this.ExceptionalEventsData.Count);
					foreach (string serializedObject in this.ExceptionalEventsData)
					{
						list.Add(EntitySerializer.Deserialize<Event>(serializedObject));
					}
					this.deserializedExceptionalEvents = list;
				}
				return this.deserializedExceptionalEvents;
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000A9FC File Offset: 0x00008BFC
		public override string ToString()
		{
			return base.ToString() + ", EntryId: " + TraceUtils.DumpEntryId(base.ItemId);
		}

		// Token: 0x04000369 RID: 873
		private Event deserializedEvent;

		// Token: 0x0400036A RID: 874
		private IList<Event> deserializedExceptionalEvents;
	}
}
