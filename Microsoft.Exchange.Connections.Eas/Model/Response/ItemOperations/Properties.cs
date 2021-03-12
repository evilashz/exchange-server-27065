using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Common.Email;
using Microsoft.Exchange.Connections.Eas.Model.Common.WindowsLive;
using Microsoft.Exchange.Connections.Eas.Model.Response.AirSyncBase;
using Microsoft.Exchange.Connections.Eas.Model.Response.Calendar;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.ItemOperations
{
	// Token: 0x020000CC RID: 204
	[XmlType(Namespace = "ItemOperations", TypeName = "Properties")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Properties
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000B654 File Offset: 0x00009854
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0000B65C File Offset: 0x0000985C
		[XmlElement(ElementName = "Body", Namespace = "AirSyncBase")]
		public Body Body { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000B665 File Offset: 0x00009865
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0000B66D File Offset: 0x0000986D
		[XmlArray(ElementName = "Categories", Namespace = "Email")]
		public List<Category> Categories { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000B676 File Offset: 0x00009876
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000B67E File Offset: 0x0000987E
		[XmlArray(ElementName = "SystemCategories", Namespace = "WindowsLive")]
		public List<CategoryId> SystemCategories { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000B687 File Offset: 0x00009887
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0000B68F File Offset: 0x0000988F
		[XmlElement(ElementName = "ConversationId", Namespace = "Email2")]
		public string ConversationId { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000B698 File Offset: 0x00009898
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0000B6A0 File Offset: 0x000098A0
		[XmlElement(ElementName = "ConversationIndex", Namespace = "Email2")]
		public string ConversationIndex { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0000B6A9 File Offset: 0x000098A9
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x0000B6B1 File Offset: 0x000098B1
		[XmlElement(ElementName = "DateReceived", Namespace = "Email")]
		public string DateReceived { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000B6BA File Offset: 0x000098BA
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0000B6C2 File Offset: 0x000098C2
		[XmlElement(ElementName = "Flag", Namespace = "Email")]
		public Flag Flag { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000B6CB File Offset: 0x000098CB
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0000B6D3 File Offset: 0x000098D3
		[XmlElement(ElementName = "From", Namespace = "Email")]
		public string From { get; set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000B6DC File Offset: 0x000098DC
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x0000B6E4 File Offset: 0x000098E4
		[XmlElement(ElementName = "Importance", Namespace = "Email")]
		public byte? Importance { get; set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000B6ED File Offset: 0x000098ED
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0000B6F5 File Offset: 0x000098F5
		[XmlElement(ElementName = "MessageClass", Namespace = "Email")]
		public string MessageClass { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000B6FE File Offset: 0x000098FE
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0000B706 File Offset: 0x00009906
		[XmlElement(ElementName = "Read", Namespace = "Email")]
		public byte? Read { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000B70F File Offset: 0x0000990F
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0000B717 File Offset: 0x00009917
		[XmlElement(ElementName = "Subject", Namespace = "Email")]
		public string Subject { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000B720 File Offset: 0x00009920
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0000B728 File Offset: 0x00009928
		[XmlElement(ElementName = "Subject", Namespace = "Calendar")]
		public string CalendarSubject { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000B731 File Offset: 0x00009931
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0000B739 File Offset: 0x00009939
		[XmlElement(ElementName = "TimeZone", Namespace = "Calendar")]
		public string TimeZone { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000B742 File Offset: 0x00009942
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0000B74A File Offset: 0x0000994A
		[XmlElement(ElementName = "StartTime", Namespace = "Calendar")]
		public string StartTime { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000B753 File Offset: 0x00009953
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0000B75B File Offset: 0x0000995B
		[XmlElement(ElementName = "EndTime", Namespace = "Calendar")]
		public string EndTime { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000B764 File Offset: 0x00009964
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0000B76C File Offset: 0x0000996C
		[XmlElement(ElementName = "UID", Namespace = "Calendar")]
		public string Uid { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000B775 File Offset: 0x00009975
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x0000B77D File Offset: 0x0000997D
		[XmlElement(ElementName = "AllDayEvent", Namespace = "Calendar")]
		public bool AllDayEvent { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0000B786 File Offset: 0x00009986
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x0000B78E File Offset: 0x0000998E
		[XmlElement(ElementName = "BusyStatus", Namespace = "Calendar")]
		public int BusyStatus { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0000B797 File Offset: 0x00009997
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x0000B79F File Offset: 0x0000999F
		[XmlElement(ElementName = "Location", Namespace = "Calendar")]
		public string Location { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0000B7A8 File Offset: 0x000099A8
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x0000B7B0 File Offset: 0x000099B0
		[XmlElement(ElementName = "Sensitivity", Namespace = "Calendar")]
		public int Sensitivity { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000B7B9 File Offset: 0x000099B9
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x0000B7C1 File Offset: 0x000099C1
		[XmlElement(ElementName = "Reminder", Namespace = "Calendar")]
		public int Reminder { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000B7CA File Offset: 0x000099CA
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x0000B7D2 File Offset: 0x000099D2
		[XmlElement(ElementName = "OrganizerEmail", Namespace = "Calendar")]
		public string OrganizerEmail { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000B7DB File Offset: 0x000099DB
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x0000B7E3 File Offset: 0x000099E3
		[XmlElement(ElementName = "OrganizerName", Namespace = "Calendar")]
		public string OrganizerName { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000B7EC File Offset: 0x000099EC
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0000B7F4 File Offset: 0x000099F4
		[XmlElement(ElementName = "MeetingStatus", Namespace = "Calendar")]
		public int MeetingStatus { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000B7FD File Offset: 0x000099FD
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0000B805 File Offset: 0x00009A05
		[XmlArray(ElementName = "Attendees", Namespace = "Calendar")]
		public List<Attendee> Attendees { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000B80E File Offset: 0x00009A0E
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0000B816 File Offset: 0x00009A16
		[XmlArray(ElementName = "Exceptions", Namespace = "Calendar")]
		public List<Microsoft.Exchange.Connections.Eas.Model.Response.Calendar.Exception> Exceptions { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000B81F File Offset: 0x00009A1F
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0000B827 File Offset: 0x00009A27
		[XmlElement(ElementName = "Recurrence", Namespace = "Calendar")]
		public Recurrence Recurrence { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000B830 File Offset: 0x00009A30
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0000B838 File Offset: 0x00009A38
		[XmlElement(ElementName = "ResponseType", Namespace = "Calendar")]
		public int ResponseType { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000B841 File Offset: 0x00009A41
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0000B849 File Offset: 0x00009A49
		[XmlElement(ElementName = "To", Namespace = "Email")]
		public string To { get; set; }
	}
}
