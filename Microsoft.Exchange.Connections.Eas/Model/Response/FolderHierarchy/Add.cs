using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy
{
	// Token: 0x020000C5 RID: 197
	[XmlType(Namespace = "FolderHierarchy", TypeName = "Add")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Add
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000B495 File Offset: 0x00009695
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0000B49D File Offset: 0x0000969D
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000B4A6 File Offset: 0x000096A6
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0000B4AE File Offset: 0x000096AE
		[XmlElement(ElementName = "ParentId")]
		public string ParentId { get; set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000B4B7 File Offset: 0x000096B7
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0000B4BF File Offset: 0x000096BF
		[XmlElement(ElementName = "DisplayName")]
		public string DisplayName { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000B4C8 File Offset: 0x000096C8
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0000B4D0 File Offset: 0x000096D0
		[XmlElement(ElementName = "Type")]
		public int Type { get; set; }
	}
}
