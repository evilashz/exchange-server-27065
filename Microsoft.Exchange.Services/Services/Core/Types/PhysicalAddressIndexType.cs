using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005C7 RID: 1479
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PhysicalAddressIndexType
	{
		// Token: 0x04001ACB RID: 6859
		None,
		// Token: 0x04001ACC RID: 6860
		Home,
		// Token: 0x04001ACD RID: 6861
		Business,
		// Token: 0x04001ACE RID: 6862
		Other
	}
}
