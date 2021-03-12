using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000ECD RID: 3789
	[Serializable]
	public class Recurrence
	{
		// Token: 0x060082B3 RID: 33459 RVA: 0x00239E7E File Offset: 0x0023807E
		public Recurrence()
		{
		}

		// Token: 0x060082B4 RID: 33460 RVA: 0x00239E86 File Offset: 0x00238086
		public Recurrence(RecurrenceType type, uint interval, uint nthDayInMonth, DaysOfWeek daysOfWeek, WeekOrderInMonth weekOrderInMonth, uint monthOrder)
		{
			this.Type = type;
			this.Interval = interval;
			this.NthDayInMonth = nthDayInMonth;
			this.DaysOfWeek = daysOfWeek;
			this.WeekOrderInMonth = weekOrderInMonth;
			this.MonthOrder = monthOrder;
		}

		// Token: 0x170022A4 RID: 8868
		// (get) Token: 0x060082B5 RID: 33461 RVA: 0x00239EBB File Offset: 0x002380BB
		// (set) Token: 0x060082B6 RID: 33462 RVA: 0x00239EC3 File Offset: 0x002380C3
		[XmlElement("Type")]
		public RecurrenceType Type { get; set; }

		// Token: 0x170022A5 RID: 8869
		// (get) Token: 0x060082B7 RID: 33463 RVA: 0x00239ECC File Offset: 0x002380CC
		// (set) Token: 0x060082B8 RID: 33464 RVA: 0x00239ED4 File Offset: 0x002380D4
		[XmlElement("Interval")]
		public uint Interval { get; set; }

		// Token: 0x170022A6 RID: 8870
		// (get) Token: 0x060082B9 RID: 33465 RVA: 0x00239EDD File Offset: 0x002380DD
		// (set) Token: 0x060082BA RID: 33466 RVA: 0x00239EE5 File Offset: 0x002380E5
		[XmlElement("NthDayInMonth")]
		public uint NthDayInMonth { get; set; }

		// Token: 0x170022A7 RID: 8871
		// (get) Token: 0x060082BB RID: 33467 RVA: 0x00239EEE File Offset: 0x002380EE
		// (set) Token: 0x060082BC RID: 33468 RVA: 0x00239EF6 File Offset: 0x002380F6
		[XmlElement("DaysOfWeek")]
		public DaysOfWeek DaysOfWeek { get; set; }

		// Token: 0x170022A8 RID: 8872
		// (get) Token: 0x060082BD RID: 33469 RVA: 0x00239EFF File Offset: 0x002380FF
		// (set) Token: 0x060082BE RID: 33470 RVA: 0x00239F07 File Offset: 0x00238107
		[XmlElement("WeekOrderInMonth")]
		public WeekOrderInMonth WeekOrderInMonth { get; set; }

		// Token: 0x170022A9 RID: 8873
		// (get) Token: 0x060082BF RID: 33471 RVA: 0x00239F10 File Offset: 0x00238110
		// (set) Token: 0x060082C0 RID: 33472 RVA: 0x00239F18 File Offset: 0x00238118
		[XmlElement("MonthOrder")]
		public uint MonthOrder { get; set; }
	}
}
