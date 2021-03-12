using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x020000AA RID: 170
	internal class CalendarItemOccurrenceData : CalendarItemBaseData
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00031A56 File Offset: 0x0002FC56
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x00031A5E File Offset: 0x0002FC5E
		public ExDateTime OriginalStartTime
		{
			get
			{
				return this.originalStartTime;
			}
			set
			{
				this.originalStartTime = value;
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00031A67 File Offset: 0x0002FC67
		public CalendarItemOccurrenceData()
		{
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00031A6F File Offset: 0x0002FC6F
		public CalendarItemOccurrenceData(CalendarItemOccurrence calendarItem)
		{
			this.SetFrom(calendarItem);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00031A7E File Offset: 0x0002FC7E
		public CalendarItemOccurrenceData(CalendarItemBase calendarItemBase)
		{
			this.SetFrom(calendarItemBase);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00031A8D File Offset: 0x0002FC8D
		public CalendarItemOccurrenceData(CalendarItemOccurrenceData other) : base(other)
		{
			this.originalStartTime = other.originalStartTime;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00031AA4 File Offset: 0x0002FCA4
		public static EditCalendarItemHelper.CalendarItemUpdateFlags Compare(CalendarItemOccurrence calendarItem)
		{
			return EditCalendarItemHelper.CalendarItemUpdateFlags.None;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00031AB4 File Offset: 0x0002FCB4
		public override void SetFrom(CalendarItemBase calendarItemBase)
		{
			base.SetFrom(calendarItemBase);
			CalendarItemOccurrence calendarItemOccurrence = calendarItemBase as CalendarItemOccurrence;
			if (calendarItemOccurrence != null)
			{
				this.originalStartTime = calendarItemOccurrence.OriginalStartTime;
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00031ADE File Offset: 0x0002FCDE
		public override EditCalendarItemHelper.CalendarItemUpdateFlags CopyTo(CalendarItemBase calendarItemBase)
		{
			return base.CopyTo(calendarItemBase);
		}

		// Token: 0x04000480 RID: 1152
		private ExDateTime originalStartTime;
	}
}
