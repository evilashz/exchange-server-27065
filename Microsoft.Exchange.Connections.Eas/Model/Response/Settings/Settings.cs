using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Settings
{
	// Token: 0x02000068 RID: 104
	[XmlType(Namespace = "Settings", TypeName = "Settings")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Settings
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000055AD File Offset: 0x000037AD
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x000055B5 File Offset: 0x000037B5
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }
	}
}
