using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Calendar
{
	// Token: 0x020000C3 RID: 195
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "Calendar", TypeName = "Exception")]
	public class Exception
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000B364 File Offset: 0x00009564
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0000B36C File Offset: 0x0000956C
		[XmlElement(ElementName = "Deleted")]
		public bool Deleted { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000B375 File Offset: 0x00009575
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0000B37D File Offset: 0x0000957D
		[XmlElement(ElementName = "StartTime")]
		public string StartTime { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000B386 File Offset: 0x00009586
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0000B38E File Offset: 0x0000958E
		[XmlElement(ElementName = "Subject")]
		public string Subject { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0000B397 File Offset: 0x00009597
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0000B39F File Offset: 0x0000959F
		[XmlElement(ElementName = "EndTime")]
		public string EndTime { get; set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000B3A8 File Offset: 0x000095A8
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x0000B3B0 File Offset: 0x000095B0
		[XmlElement(ElementName = "ExceptionStartTime")]
		public string ExceptionStartTime { get; set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000B3B9 File Offset: 0x000095B9
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x0000B3C1 File Offset: 0x000095C1
		[XmlElement(ElementName = "BusyStatus")]
		public int BusyStatus { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000B3CA File Offset: 0x000095CA
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x0000B3D2 File Offset: 0x000095D2
		[XmlElement(ElementName = "AllDayEvent")]
		public bool AllDayEvent { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000B3DB File Offset: 0x000095DB
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x0000B3E3 File Offset: 0x000095E3
		[XmlElement(ElementName = "Location")]
		public string Location { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000B3EC File Offset: 0x000095EC
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0000B3F4 File Offset: 0x000095F4
		[XmlElement(ElementName = "Reminder")]
		public int Reminder { get; set; }
	}
}
