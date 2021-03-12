using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy
{
	// Token: 0x020000C7 RID: 199
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "FolderHierarchy", TypeName = "Delete")]
	public class Delete
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000B53E File Offset: 0x0000973E
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0000B546 File Offset: 0x00009746
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }
	}
}
