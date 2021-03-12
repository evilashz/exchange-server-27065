using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Autodiscover
{
	// Token: 0x020000BD RID: 189
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(TypeName = "Response")]
	public class Response
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000B23D File Offset: 0x0000943D
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000B245 File Offset: 0x00009445
		[XmlElement(ElementName = "Culture")]
		public string Culture { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000B24E File Offset: 0x0000944E
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000B256 File Offset: 0x00009456
		[XmlElement(ElementName = "User")]
		public User User { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000B25F File Offset: 0x0000945F
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0000B267 File Offset: 0x00009467
		[XmlElement(ElementName = "Action")]
		public Action Action { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000B270 File Offset: 0x00009470
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0000B278 File Offset: 0x00009478
		[XmlElement(ElementName = "Error")]
		public Error Error { get; set; }
	}
}
