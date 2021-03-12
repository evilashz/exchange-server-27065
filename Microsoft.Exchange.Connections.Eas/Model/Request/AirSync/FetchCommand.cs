using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.AirSync
{
	// Token: 0x02000096 RID: 150
	[XmlType(Namespace = "AirSync", TypeName = "Fetch")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class FetchCommand : Command
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00009CD0 File Offset: 0x00007ED0
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00009CD8 File Offset: 0x00007ED8
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }
	}
}
