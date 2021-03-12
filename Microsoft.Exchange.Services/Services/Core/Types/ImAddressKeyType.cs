using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005C6 RID: 1478
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ImAddressKeyType
	{
		// Token: 0x04001AC7 RID: 6855
		ImAddress1,
		// Token: 0x04001AC8 RID: 6856
		ImAddress2,
		// Token: 0x04001AC9 RID: 6857
		ImAddress3
	}
}
