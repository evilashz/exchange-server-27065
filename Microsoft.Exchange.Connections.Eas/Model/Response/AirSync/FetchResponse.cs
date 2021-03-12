using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSync
{
	// Token: 0x020000B1 RID: 177
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "AirSync", TypeName = "FetchResponse")]
	public class FetchResponse
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000AE9D File Offset: 0x0000909D
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x0000AEA5 File Offset: 0x000090A5
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000AEAE File Offset: 0x000090AE
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x0000AEB6 File Offset: 0x000090B6
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000AEBF File Offset: 0x000090BF
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x0000AEC7 File Offset: 0x000090C7
		[XmlElement(ElementName = "ApplicationData")]
		public ApplicationData ApplicationData { get; set; }
	}
}
