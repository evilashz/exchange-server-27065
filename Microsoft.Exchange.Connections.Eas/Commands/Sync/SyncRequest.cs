using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Request.AirSync;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Sync
{
	// Token: 0x0200006D RID: 109
	[XmlRoot(ElementName = "Sync", Namespace = "AirSync", IsNullable = false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class SyncRequest : Sync
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00005763 File Offset: 0x00003963
		public SyncRequest()
		{
			base.Collections = new List<Collection>();
		}
	}
}
