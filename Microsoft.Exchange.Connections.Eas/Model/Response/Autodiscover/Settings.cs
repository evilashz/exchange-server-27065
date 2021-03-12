using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Autodiscover
{
	// Token: 0x020000C0 RID: 192
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(TypeName = "Settings")]
	public class Settings
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000B2D5 File Offset: 0x000094D5
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x0000B2DD File Offset: 0x000094DD
		[XmlElement(ElementName = "Server")]
		public List<Server> Server { get; set; }
	}
}
