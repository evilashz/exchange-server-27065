using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSync
{
	// Token: 0x020000AD RID: 173
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "AirSync", TypeName = "Collection")]
	public class Collection
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0000AD6F File Offset: 0x00008F6F
		public Collection()
		{
			this.Commands = new Commands();
			this.Responses = new Responses();
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000AD8D File Offset: 0x00008F8D
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0000AD95 File Offset: 0x00008F95
		[XmlElement(ElementName = "CollectionId")]
		public string CollectionId { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000AD9E File Offset: 0x00008F9E
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0000ADA6 File Offset: 0x00008FA6
		[XmlElement(ElementName = "MoreAvailable")]
		public EmptyTag MoreAvailable { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0000ADAF File Offset: 0x00008FAF
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x0000ADB7 File Offset: 0x00008FB7
		[XmlElement(ElementName = "Commands")]
		public Commands Commands { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		[XmlElement(ElementName = "Responses")]
		public Responses Responses { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000ADD1 File Offset: 0x00008FD1
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x0000ADD9 File Offset: 0x00008FD9
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0000ADE2 File Offset: 0x00008FE2
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x0000ADEA File Offset: 0x00008FEA
		[XmlElement(ElementName = "SyncKey")]
		public string SyncKey { get; set; }
	}
}
