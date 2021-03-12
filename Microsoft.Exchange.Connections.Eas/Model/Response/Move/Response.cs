using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Move
{
	// Token: 0x020000CE RID: 206
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "Move", TypeName = "Response")]
	public class Response
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000B873 File Offset: 0x00009A73
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x0000B87B File Offset: 0x00009A7B
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000B884 File Offset: 0x00009A84
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x0000B88C File Offset: 0x00009A8C
		[XmlElement(ElementName = "SrcMsgId")]
		public string SrcMsgId { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000B895 File Offset: 0x00009A95
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0000B89D File Offset: 0x00009A9D
		[XmlElement(ElementName = "DstMsgId")]
		public string DstMsgId { get; set; }
	}
}
