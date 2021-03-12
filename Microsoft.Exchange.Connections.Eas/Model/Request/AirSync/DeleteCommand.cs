using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.AirSync
{
	// Token: 0x02000095 RID: 149
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "AirSync", TypeName = "Delete")]
	public class DeleteCommand : Command
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00009CB7 File Offset: 0x00007EB7
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00009CBF File Offset: 0x00007EBF
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }
	}
}
