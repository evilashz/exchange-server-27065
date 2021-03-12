using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000148 RID: 328
	[XmlType(TypeName = "LicenseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum LicenseType
	{
		// Token: 0x040006C8 RID: 1736
		Free,
		// Token: 0x040006C9 RID: 1737
		Trial,
		// Token: 0x040006CA RID: 1738
		Paid
	}
}
