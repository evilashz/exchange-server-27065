using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000147 RID: 327
	[XmlType(TypeName = "RequestedCapabilities", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum RequestedCapabilities
	{
		// Token: 0x040006C3 RID: 1731
		Restricted,
		// Token: 0x040006C4 RID: 1732
		ReadItem,
		// Token: 0x040006C5 RID: 1733
		ReadWriteMailbox,
		// Token: 0x040006C6 RID: 1734
		ReadWriteItem
	}
}
