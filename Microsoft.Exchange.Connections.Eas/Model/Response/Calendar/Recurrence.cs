using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Calendar
{
	// Token: 0x020000C4 RID: 196
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "Calendar", TypeName = "Recurrence")]
	public class Recurrence
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000B405 File Offset: 0x00009605
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x0000B40D File Offset: 0x0000960D
		[XmlElement(ElementName = "Type")]
		public int Type { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0000B416 File Offset: 0x00009616
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x0000B41E File Offset: 0x0000961E
		[XmlElement(ElementName = "Interval")]
		public int Interval { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000B427 File Offset: 0x00009627
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0000B42F File Offset: 0x0000962F
		[XmlElement(ElementName = "DayOfWeek")]
		public int DayOfWeek { get; set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000B438 File Offset: 0x00009638
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0000B440 File Offset: 0x00009640
		[XmlElement(ElementName = "WeekOfMonth")]
		public int WeekOfMonth { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000B449 File Offset: 0x00009649
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0000B451 File Offset: 0x00009651
		[XmlElement(ElementName = "DayOfMonth")]
		public int DayOfMonth { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000B45A File Offset: 0x0000965A
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0000B462 File Offset: 0x00009662
		[XmlElement(ElementName = "MonthOfYear")]
		public int MonthOfYear { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000B46B File Offset: 0x0000966B
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0000B473 File Offset: 0x00009673
		[XmlElement(ElementName = "Occurrences")]
		public int Occurrences { get; set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000B47C File Offset: 0x0000967C
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0000B484 File Offset: 0x00009684
		[XmlElement(ElementName = "Until", Namespace = "Calendar")]
		public string Until { get; set; }
	}
}
