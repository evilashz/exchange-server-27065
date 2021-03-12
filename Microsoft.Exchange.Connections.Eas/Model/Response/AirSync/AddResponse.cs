using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSync
{
	// Token: 0x020000A9 RID: 169
	[XmlType(Namespace = "AirSync", TypeName = "AddResponse")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class AddResponse
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000A7BB File Offset: 0x000089BB
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x0000A7C3 File Offset: 0x000089C3
		[XmlElement(ElementName = "Class")]
		public string Class { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000A7CC File Offset: 0x000089CC
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000A7D4 File Offset: 0x000089D4
		[XmlElement(ElementName = "ClientId")]
		public string ClientId { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000A7DD File Offset: 0x000089DD
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000A7E5 File Offset: 0x000089E5
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000A7EE File Offset: 0x000089EE
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000A7F6 File Offset: 0x000089F6
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }
	}
}
