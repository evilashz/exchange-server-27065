using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSync
{
	// Token: 0x0200006E RID: 110
	[XmlType(Namespace = "AirSync", TypeName = "Sync")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Sync
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00005776 File Offset: 0x00003976
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000577E File Offset: 0x0000397E
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00005787 File Offset: 0x00003987
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000578F File Offset: 0x0000398F
		[XmlIgnore]
		public bool StatusSpecified { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00005798 File Offset: 0x00003998
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x000057A0 File Offset: 0x000039A0
		[XmlElement(ElementName = "Collections", Type = typeof(List<Collection>))]
		public List<Collection> Collections { get; set; }
	}
}
