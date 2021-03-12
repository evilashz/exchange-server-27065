using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.GetItemEstimate
{
	// Token: 0x020000CA RID: 202
	[XmlRoot(Namespace = "GetItemEstimate", ElementName = "Response")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "GetItemEstimate", TypeName = "Response")]
	public class Response
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000B5CD File Offset: 0x000097CD
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x0000B5D5 File Offset: 0x000097D5
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000B5DE File Offset: 0x000097DE
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0000B5E6 File Offset: 0x000097E6
		[XmlElement(ElementName = "Collection")]
		public Collection Collection { get; set; }
	}
}
