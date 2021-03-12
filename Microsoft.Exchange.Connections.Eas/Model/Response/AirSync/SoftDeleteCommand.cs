using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSync
{
	// Token: 0x020000B3 RID: 179
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "AirSync", TypeName = "SoftDeleteCommand")]
	public class SoftDeleteCommand : Command
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000AF34 File Offset: 0x00009134
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0000AF3C File Offset: 0x0000913C
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }
	}
}
