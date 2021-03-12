using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.ItemOperations
{
	// Token: 0x02000052 RID: 82
	[XmlType(Namespace = "ItemOperations", TypeName = "ItemOperations")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class ItemOperations
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00004D9C File Offset: 0x00002F9C
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00004DA4 File Offset: 0x00002FA4
		[XmlElement(ElementName = "Response")]
		public Response Response { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00004DAD File Offset: 0x00002FAD
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00004DB5 File Offset: 0x00002FB5
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }
	}
}
