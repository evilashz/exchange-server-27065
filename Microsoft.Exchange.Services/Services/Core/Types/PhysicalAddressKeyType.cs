using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005C3 RID: 1475
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PhysicalAddressKeyType
	{
		// Token: 0x04001AAC RID: 6828
		Home,
		// Token: 0x04001AAD RID: 6829
		Business,
		// Token: 0x04001AAE RID: 6830
		Other
	}
}
