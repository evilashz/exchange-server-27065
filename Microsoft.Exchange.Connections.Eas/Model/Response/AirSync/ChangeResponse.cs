using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSync
{
	// Token: 0x020000AC RID: 172
	[XmlType(Namespace = "AirSync", TypeName = "ChangeResponse")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class ChangeResponse
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000AD34 File Offset: 0x00008F34
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0000AD3C File Offset: 0x00008F3C
		[XmlElement(ElementName = "Class")]
		public string Class { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000AD45 File Offset: 0x00008F45
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0000AD4D File Offset: 0x00008F4D
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000AD56 File Offset: 0x00008F56
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0000AD5E File Offset: 0x00008F5E
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }
	}
}
