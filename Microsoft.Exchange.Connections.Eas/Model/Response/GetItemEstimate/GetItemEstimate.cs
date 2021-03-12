using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.GetItemEstimate
{
	// Token: 0x0200004C RID: 76
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "GetItemEstimate", TypeName = "GetItemEstimate")]
	public class GetItemEstimate
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00004C28 File Offset: 0x00002E28
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00004C30 File Offset: 0x00002E30
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00004C39 File Offset: 0x00002E39
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00004C41 File Offset: 0x00002E41
		[XmlElement(ElementName = "Response")]
		public Response Response { get; set; }
	}
}
