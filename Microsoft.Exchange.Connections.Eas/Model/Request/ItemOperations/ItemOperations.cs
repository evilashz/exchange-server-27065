using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.ItemOperations
{
	// Token: 0x02000050 RID: 80
	[XmlType(Namespace = "ItemOperations", TypeName = "ItemOperations")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class ItemOperations
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00004D7B File Offset: 0x00002F7B
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00004D83 File Offset: 0x00002F83
		[XmlElement(ElementName = "Fetch")]
		public Fetch[] Fetches { get; set; }
	}
}
