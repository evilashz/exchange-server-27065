using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000145 RID: 325
	[XmlType(TypeName = "ExtensionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ExtensionType
	{
		// Token: 0x040006BA RID: 1722
		Default,
		// Token: 0x040006BB RID: 1723
		Private,
		// Token: 0x040006BC RID: 1724
		MarketPlace
	}
}
