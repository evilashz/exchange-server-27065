using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.ItemOperations
{
	// Token: 0x020000CB RID: 203
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "ItemOperations", TypeName = "Fetch")]
	public class Fetch
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000B5F7 File Offset: 0x000097F7
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x0000B5FF File Offset: 0x000097FF
		[XmlElement(ElementName = "Class", Namespace = "AirSync")]
		public string Class { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000B608 File Offset: 0x00009808
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x0000B610 File Offset: 0x00009810
		[XmlElement(ElementName = "CollectionId", Namespace = "AirSync")]
		public string CollectionId { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0000B619 File Offset: 0x00009819
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x0000B621 File Offset: 0x00009821
		[XmlElement(ElementName = "Properties")]
		public Properties Properties { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000B62A File Offset: 0x0000982A
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x0000B632 File Offset: 0x00009832
		[XmlElement(ElementName = "ServerId", Namespace = "AirSync")]
		public string ServerId { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000B63B File Offset: 0x0000983B
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x0000B643 File Offset: 0x00009843
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }
	}
}
