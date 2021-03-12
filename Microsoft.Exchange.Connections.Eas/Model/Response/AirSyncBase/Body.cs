using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Common.AirSyncBase;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSyncBase
{
	// Token: 0x020000B6 RID: 182
	[XmlType(Namespace = "AirSyncBase", TypeName = "Body")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Body
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0000AFF6 File Offset: 0x000091F6
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0000AFFE File Offset: 0x000091FE
		[XmlElement(ElementName = "Data")]
		public string Data { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000B007 File Offset: 0x00009207
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0000B00F File Offset: 0x0000920F
		[XmlElement(ElementName = "EstimatedDataSize")]
		public uint EstimatedDataSize { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000B018 File Offset: 0x00009218
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0000B020 File Offset: 0x00009220
		[XmlElement(ElementName = "Preview")]
		public string Preview { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000B029 File Offset: 0x00009229
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0000B031 File Offset: 0x00009231
		[XmlElement(ElementName = "Type")]
		public BodyType Type { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000B03A File Offset: 0x0000923A
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x0000B042 File Offset: 0x00009242
		[XmlElement(ElementName = "Truncated")]
		public bool Truncated { get; set; }
	}
}
