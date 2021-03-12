using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.ItemOperations
{
	// Token: 0x020000CD RID: 205
	[XmlRoot(Namespace = "ItemOperations", ElementName = "Response")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "ItemOperations", TypeName = "Response")]
	public class Response
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0000B85A File Offset: 0x00009A5A
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x0000B862 File Offset: 0x00009A62
		[XmlElement(ElementName = "Fetch")]
		public Fetch[] Fetches { get; set; }
	}
}
