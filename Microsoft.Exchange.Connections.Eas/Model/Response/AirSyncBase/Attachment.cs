using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSyncBase
{
	// Token: 0x020000B4 RID: 180
	[XmlType(Namespace = "AirSyncBase", TypeName = "Attachment")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Attachment
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000AF4D File Offset: 0x0000914D
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x0000AF55 File Offset: 0x00009155
		[XmlElement(ElementName = "DisplayName")]
		public string DisplayName { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0000AF5E File Offset: 0x0000915E
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0000AF66 File Offset: 0x00009166
		[XmlElement(ElementName = "FileReference")]
		public string FileReference { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000AF6F File Offset: 0x0000916F
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x0000AF77 File Offset: 0x00009177
		[XmlElement(ElementName = "Method")]
		public byte Method { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000AF80 File Offset: 0x00009180
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0000AF88 File Offset: 0x00009188
		[XmlElement(ElementName = "EstimatedDataSize")]
		public uint EstimatedDataSize { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000AF91 File Offset: 0x00009191
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x0000AF99 File Offset: 0x00009199
		[XmlElement(ElementName = "ContentId")]
		public string ContentId { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000AFA2 File Offset: 0x000091A2
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x0000AFAA File Offset: 0x000091AA
		[XmlElement(ElementName = "ContentLocation")]
		public string ContentLocation { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000AFB3 File Offset: 0x000091B3
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x0000AFBB File Offset: 0x000091BB
		[XmlElement(ElementName = "IsInline")]
		public bool IsInline { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000AFC4 File Offset: 0x000091C4
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x0000AFCC File Offset: 0x000091CC
		[XmlIgnore]
		public bool IsInlineSpecified { get; set; }
	}
}
