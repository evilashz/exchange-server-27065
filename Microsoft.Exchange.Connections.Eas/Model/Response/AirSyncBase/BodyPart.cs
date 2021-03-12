using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSyncBase
{
	// Token: 0x020000B7 RID: 183
	[XmlType(Namespace = "AirSyncBase", TypeName = "BodyPart")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class BodyPart
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0000B053 File Offset: 0x00009253
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x0000B05B File Offset: 0x0000925B
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000B064 File Offset: 0x00009264
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x0000B06C File Offset: 0x0000926C
		[XmlElement(ElementName = "Type")]
		public byte Type { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000B075 File Offset: 0x00009275
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x0000B07D File Offset: 0x0000927D
		[XmlElement(ElementName = "EstimatedDataSize")]
		public uint EstimatedDataSize { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0000B086 File Offset: 0x00009286
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x0000B08E File Offset: 0x0000928E
		[XmlElement(ElementName = "Truncated")]
		public bool Truncated { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000B097 File Offset: 0x00009297
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0000B09F File Offset: 0x0000929F
		[XmlIgnore]
		public bool TruncatedSpecified { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000B0A8 File Offset: 0x000092A8
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0000B0B0 File Offset: 0x000092B0
		[XmlElement(ElementName = "Data")]
		public string Data { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000B0B9 File Offset: 0x000092B9
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0000B0C1 File Offset: 0x000092C1
		[XmlElement(ElementName = "Preview")]
		public string Preview { get; set; }
	}
}
