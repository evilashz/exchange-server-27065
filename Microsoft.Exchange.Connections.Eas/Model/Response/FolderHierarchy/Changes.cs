using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy
{
	// Token: 0x020000C6 RID: 198
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "FolderHierarchy", TypeName = "Changes")]
	public class Changes
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000B4E1 File Offset: 0x000096E1
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0000B4E9 File Offset: 0x000096E9
		[XmlElement(ElementName = "Count")]
		public uint Count { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000B4F2 File Offset: 0x000096F2
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0000B4FA File Offset: 0x000096FA
		[XmlIgnore]
		public bool CountSpecified { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000B503 File Offset: 0x00009703
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x0000B50B File Offset: 0x0000970B
		[XmlElement(ElementName = "Add")]
		public List<Add> Additions { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000B514 File Offset: 0x00009714
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0000B51C File Offset: 0x0000971C
		[XmlElement(ElementName = "Update")]
		public List<Update> Updates { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000B525 File Offset: 0x00009725
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0000B52D File Offset: 0x0000972D
		[XmlElement(ElementName = "Delete")]
		public List<Delete> Deletions { get; set; }
	}
}
