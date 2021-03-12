using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.Settings
{
	// Token: 0x02000066 RID: 102
	[XmlType(Namespace = "Settings", TypeName = "Settings")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Settings
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000555D File Offset: 0x0000375D
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00005565 File Offset: 0x00003765
		[XmlElement(ElementName = "UserInformation")]
		public UserInformation UserInformation { get; set; }
	}
}
