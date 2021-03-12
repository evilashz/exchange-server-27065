using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.GetItemEstimate
{
	// Token: 0x0200004A RID: 74
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "GetItemEstimate", TypeName = "GetItemEstimate")]
	public class GetItemEstimate
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00004BFC File Offset: 0x00002DFC
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00004C04 File Offset: 0x00002E04
		[XmlElement(ElementName = "Collections", Type = typeof(List<Collection>))]
		public List<Collection> Collections { get; set; }
	}
}
