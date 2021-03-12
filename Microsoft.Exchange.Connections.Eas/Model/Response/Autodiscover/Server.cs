using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Autodiscover
{
	// Token: 0x020000BE RID: 190
	[XmlType(TypeName = "Server")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Server
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000B289 File Offset: 0x00009489
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0000B291 File Offset: 0x00009491
		[XmlElement(ElementName = "Type")]
		public ServerType Type { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0000B29A File Offset: 0x0000949A
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x0000B2A2 File Offset: 0x000094A2
		[XmlElement(ElementName = "Url")]
		public string Url { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x0000B2AB File Offset: 0x000094AB
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x0000B2B3 File Offset: 0x000094B3
		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0000B2BC File Offset: 0x000094BC
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x0000B2C4 File Offset: 0x000094C4
		[XmlElement(ElementName = "ServerData")]
		public string ServerData { get; set; }
	}
}
