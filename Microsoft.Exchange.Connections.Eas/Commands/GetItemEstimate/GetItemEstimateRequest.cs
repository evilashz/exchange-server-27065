using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Request.GetItemEstimate;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.GetItemEstimate
{
	// Token: 0x0200004B RID: 75
	[XmlRoot(ElementName = "GetItemEstimate", Namespace = "GetItemEstimate", IsNullable = false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class GetItemEstimateRequest : GetItemEstimate
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00004C15 File Offset: 0x00002E15
		public GetItemEstimateRequest()
		{
			base.Collections = new List<Collection>();
		}
	}
}
