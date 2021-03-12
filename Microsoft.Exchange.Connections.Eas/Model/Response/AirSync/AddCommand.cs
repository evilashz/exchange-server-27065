using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSync
{
	// Token: 0x020000A8 RID: 168
	[XmlType(Namespace = "AirSync", TypeName = "AddCommand")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class AddCommand : Command
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000A791 File Offset: 0x00008991
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000A799 File Offset: 0x00008999
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000A7A2 File Offset: 0x000089A2
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000A7AA File Offset: 0x000089AA
		[XmlElement(ElementName = "ApplicationData")]
		public ApplicationData ApplicationData { get; set; }
	}
}
