using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.ItemOperations
{
	// Token: 0x020000A1 RID: 161
	[XmlType(Namespace = "ItemOperations", TypeName = "Fetch")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Fetch
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000A4B2 File Offset: 0x000086B2
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0000A4BA File Offset: 0x000086BA
		[XmlElement(ElementName = "Store")]
		public string Store { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000A4C3 File Offset: 0x000086C3
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000A4CB File Offset: 0x000086CB
		[XmlElement(ElementName = "CollectionId", Namespace = "AirSync")]
		public string CollectionId { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000A4D4 File Offset: 0x000086D4
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000A4DC File Offset: 0x000086DC
		[XmlElement(ElementName = "ServerId", Namespace = "AirSync")]
		public string ServerId { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000A4E5 File Offset: 0x000086E5
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000A4ED File Offset: 0x000086ED
		[XmlElement(ElementName = "LongId", Namespace = "Search")]
		public string LongId { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000A4F6 File Offset: 0x000086F6
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000A4FE File Offset: 0x000086FE
		[XmlElement(ElementName = "FileReference", Namespace = "AirSyncBase")]
		public string FileReference { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000A507 File Offset: 0x00008707
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000A50F File Offset: 0x0000870F
		[XmlElement(ElementName = "Options")]
		public Options Options { get; set; }
	}
}
