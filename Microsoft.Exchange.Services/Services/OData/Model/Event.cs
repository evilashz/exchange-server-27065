using System;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E68 RID: 3688
	internal class Event : Item
	{
		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06005FD3 RID: 24531 RVA: 0x0012B03F File Offset: 0x0012923F
		// (set) Token: 0x06005FD4 RID: 24532 RVA: 0x0012B051 File Offset: 0x00129251
		public DateTimeOffset Start
		{
			get
			{
				return (DateTimeOffset)base[EventSchema.Start];
			}
			set
			{
				base[EventSchema.Start] = value;
			}
		}

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06005FD5 RID: 24533 RVA: 0x0012B064 File Offset: 0x00129264
		// (set) Token: 0x06005FD6 RID: 24534 RVA: 0x0012B076 File Offset: 0x00129276
		public DateTimeOffset End
		{
			get
			{
				return (DateTimeOffset)base[EventSchema.End];
			}
			set
			{
				base[EventSchema.End] = value;
			}
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06005FD7 RID: 24535 RVA: 0x0012B089 File Offset: 0x00129289
		// (set) Token: 0x06005FD8 RID: 24536 RVA: 0x0012B09B File Offset: 0x0012929B
		public Location Location
		{
			get
			{
				return (Location)base[EventSchema.Location];
			}
			set
			{
				base[EventSchema.Location] = value;
			}
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06005FD9 RID: 24537 RVA: 0x0012B0A9 File Offset: 0x001292A9
		// (set) Token: 0x06005FDA RID: 24538 RVA: 0x0012B0BB File Offset: 0x001292BB
		public FreeBusyStatus ShowAs
		{
			get
			{
				return (FreeBusyStatus)base[EventSchema.ShowAs];
			}
			set
			{
				base[EventSchema.ShowAs] = value;
			}
		}

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06005FDB RID: 24539 RVA: 0x0012B0CE File Offset: 0x001292CE
		// (set) Token: 0x06005FDC RID: 24540 RVA: 0x0012B0E0 File Offset: 0x001292E0
		public bool IsAllDay
		{
			get
			{
				return (bool)base[EventSchema.IsAllDay];
			}
			set
			{
				base[EventSchema.IsAllDay] = value;
			}
		}

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06005FDD RID: 24541 RVA: 0x0012B0F3 File Offset: 0x001292F3
		// (set) Token: 0x06005FDE RID: 24542 RVA: 0x0012B105 File Offset: 0x00129305
		public bool IsCancelled
		{
			get
			{
				return (bool)base[EventSchema.IsCancelled];
			}
			set
			{
				base[EventSchema.IsCancelled] = value;
			}
		}

		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06005FDF RID: 24543 RVA: 0x0012B118 File Offset: 0x00129318
		// (set) Token: 0x06005FE0 RID: 24544 RVA: 0x0012B12A File Offset: 0x0012932A
		public bool IsOrganizer
		{
			get
			{
				return (bool)base[EventSchema.IsOrganizer];
			}
			set
			{
				base[EventSchema.IsOrganizer] = value;
			}
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06005FE1 RID: 24545 RVA: 0x0012B13D File Offset: 0x0012933D
		// (set) Token: 0x06005FE2 RID: 24546 RVA: 0x0012B14F File Offset: 0x0012934F
		public bool ResponseRequested
		{
			get
			{
				return (bool)base[EventSchema.ResponseRequested];
			}
			set
			{
				base[EventSchema.ResponseRequested] = value;
			}
		}

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06005FE3 RID: 24547 RVA: 0x0012B162 File Offset: 0x00129362
		// (set) Token: 0x06005FE4 RID: 24548 RVA: 0x0012B174 File Offset: 0x00129374
		public EventType Type
		{
			get
			{
				return (EventType)base[EventSchema.Type];
			}
			set
			{
				base[EventSchema.Type] = value;
			}
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06005FE5 RID: 24549 RVA: 0x0012B187 File Offset: 0x00129387
		// (set) Token: 0x06005FE6 RID: 24550 RVA: 0x0012B199 File Offset: 0x00129399
		public string SeriesId
		{
			get
			{
				return (string)base[EventSchema.SeriesId];
			}
			set
			{
				base[EventSchema.SeriesId] = value;
			}
		}

		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x06005FE7 RID: 24551 RVA: 0x0012B1A7 File Offset: 0x001293A7
		// (set) Token: 0x06005FE8 RID: 24552 RVA: 0x0012B1B9 File Offset: 0x001293B9
		public string SeriesMasterId
		{
			get
			{
				return (string)base[EventSchema.SeriesMasterId];
			}
			set
			{
				base[EventSchema.SeriesMasterId] = value;
			}
		}

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06005FE9 RID: 24553 RVA: 0x0012B1C7 File Offset: 0x001293C7
		// (set) Token: 0x06005FEA RID: 24554 RVA: 0x0012B1D9 File Offset: 0x001293D9
		public Attendee[] Attendees
		{
			get
			{
				return (Attendee[])base[EventSchema.Attendees];
			}
			set
			{
				base[EventSchema.Attendees] = value;
			}
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06005FEB RID: 24555 RVA: 0x0012B1E7 File Offset: 0x001293E7
		// (set) Token: 0x06005FEC RID: 24556 RVA: 0x0012B1F9 File Offset: 0x001293F9
		public PatternedRecurrence Recurrence
		{
			get
			{
				return (PatternedRecurrence)base[EventSchema.Recurrence];
			}
			set
			{
				base[EventSchema.Recurrence] = value;
			}
		}

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06005FED RID: 24557 RVA: 0x0012B207 File Offset: 0x00129407
		// (set) Token: 0x06005FEE RID: 24558 RVA: 0x0012B219 File Offset: 0x00129419
		public Recipient Organizer
		{
			get
			{
				return (Recipient)base[EventSchema.Organizer];
			}
			set
			{
				base[EventSchema.Organizer] = value;
			}
		}

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06005FEF RID: 24559 RVA: 0x0012B227 File Offset: 0x00129427
		// (set) Token: 0x06005FF0 RID: 24560 RVA: 0x0012B239 File Offset: 0x00129439
		public Calendar Calendar
		{
			get
			{
				return (Calendar)base[EventSchema.Calendar];
			}
			set
			{
				base[EventSchema.Calendar] = value;
			}
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06005FF1 RID: 24561 RVA: 0x0012B247 File Offset: 0x00129447
		internal override EntitySchema Schema
		{
			get
			{
				return EventSchema.SchemaInstance;
			}
		}

		// Token: 0x040033FC RID: 13308
		internal new static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(Event).Namespace, typeof(Event).Name, Microsoft.Exchange.Services.OData.Model.Item.EdmEntityType);
	}
}
