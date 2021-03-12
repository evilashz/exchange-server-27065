using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Autodiscover
{
	// Token: 0x020000C1 RID: 193
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(TypeName = "User")]
	public class User
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0000B2EE File Offset: 0x000094EE
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x0000B2F6 File Offset: 0x000094F6
		[XmlElement(ElementName = "DisplayName")]
		public string DisplayName { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000B2FF File Offset: 0x000094FF
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x0000B307 File Offset: 0x00009507
		[XmlElement(ElementName = "EMailAddress")]
		public string EMailAddress { get; set; }
	}
}
