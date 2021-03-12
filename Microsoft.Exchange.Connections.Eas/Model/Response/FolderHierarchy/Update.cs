using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy
{
	// Token: 0x020000C8 RID: 200
	[XmlType(Namespace = "FolderHierarchy", TypeName = "Update")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Update
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000B557 File Offset: 0x00009757
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0000B55F File Offset: 0x0000975F
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000B568 File Offset: 0x00009768
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0000B570 File Offset: 0x00009770
		[XmlElement(ElementName = "ParentId")]
		public string ParentId { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000B579 File Offset: 0x00009779
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0000B581 File Offset: 0x00009781
		[XmlElement(ElementName = "DisplayName")]
		public string DisplayName { get; set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000B58A File Offset: 0x0000978A
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0000B592 File Offset: 0x00009792
		[XmlElement(ElementName = "Type")]
		public int Type { get; set; }
	}
}
