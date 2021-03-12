using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.Move
{
	// Token: 0x020000A5 RID: 165
	[XmlType(Namespace = "Move", TypeName = "Move")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Move
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000A715 File Offset: 0x00008915
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0000A71D File Offset: 0x0000891D
		[XmlElement(ElementName = "SrcMsgId")]
		public string SrcMsgId { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000A726 File Offset: 0x00008926
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000A72E File Offset: 0x0000892E
		[XmlElement(ElementName = "SrcFldId")]
		public string SrcFldId { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000A737 File Offset: 0x00008937
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0000A73F File Offset: 0x0000893F
		[XmlElement(ElementName = "DstFldId")]
		public string DstFldId { get; set; }
	}
}
