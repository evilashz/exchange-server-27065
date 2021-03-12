using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.GetItemEstimate
{
	// Token: 0x020000C9 RID: 201
	[XmlRoot(Namespace = "GetItemEstimate", ElementName = "Collection")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "GetItemEstimate", TypeName = "Collection")]
	public class Collection
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000B5A3 File Offset: 0x000097A3
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0000B5AB File Offset: 0x000097AB
		[XmlElement(ElementName = "CollectionId")]
		public string CollectionId { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000B5B4 File Offset: 0x000097B4
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0000B5BC File Offset: 0x000097BC
		[XmlElement(ElementName = "Estimate")]
		public int Estimate { get; set; }
	}
}
