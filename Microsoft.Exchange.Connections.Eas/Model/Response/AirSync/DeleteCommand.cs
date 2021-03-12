using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSync
{
	// Token: 0x020000AF RID: 175
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "AirSync", TypeName = "DeleteCommand")]
	public class DeleteCommand : Command
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000AE6B File Offset: 0x0000906B
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x0000AE73 File Offset: 0x00009073
		[XmlElement(ElementName = "Class")]
		public string Class { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000AE7C File Offset: 0x0000907C
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0000AE84 File Offset: 0x00009084
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }
	}
}
