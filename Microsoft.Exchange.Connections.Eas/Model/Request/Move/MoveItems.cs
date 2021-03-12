using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.Move
{
	// Token: 0x02000056 RID: 86
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "Move", TypeName = "MoveItems")]
	public class MoveItems
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00004FF6 File Offset: 0x000031F6
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00004FFE File Offset: 0x000031FE
		[XmlElement(ElementName = "Move")]
		public Move[] Moves { get; set; }
	}
}
