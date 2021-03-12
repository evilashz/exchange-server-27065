using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Move
{
	// Token: 0x02000058 RID: 88
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "Move", TypeName = "MoveItems")]
	public class MoveItems
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005017 File Offset: 0x00003217
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000501F File Offset: 0x0000321F
		[XmlElement(ElementName = "Response")]
		public Response[] Responses { get; set; }
	}
}
