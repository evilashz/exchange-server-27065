using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSyncBase
{
	// Token: 0x020000B8 RID: 184
	[XmlType(Namespace = "AirSyncBase", TypeName = "BodyPreference")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class BodyPreference
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000B0D2 File Offset: 0x000092D2
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0000B0DA File Offset: 0x000092DA
		[XmlElement(ElementName = "Type")]
		public byte Type { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000B0E3 File Offset: 0x000092E3
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0000B0EB File Offset: 0x000092EB
		[XmlElement(ElementName = "TruncationSize")]
		public uint TruncationSize { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000B0F4 File Offset: 0x000092F4
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0000B0FC File Offset: 0x000092FC
		[XmlIgnore]
		public bool TruncationSizeSpecified { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0000B105 File Offset: 0x00009305
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x0000B10D File Offset: 0x0000930D
		[XmlElement(ElementName = "AllOrNone")]
		public bool AllOrNone { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0000B116 File Offset: 0x00009316
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0000B11E File Offset: 0x0000931E
		[XmlIgnore]
		public bool AllOrNoneSpecified { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0000B127 File Offset: 0x00009327
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0000B12F File Offset: 0x0000932F
		[XmlElement(ElementName = "Preview")]
		public uint Preview { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0000B138 File Offset: 0x00009338
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0000B140 File Offset: 0x00009340
		[XmlIgnore]
		public bool PreviewSpecified { get; set; }
	}
}
