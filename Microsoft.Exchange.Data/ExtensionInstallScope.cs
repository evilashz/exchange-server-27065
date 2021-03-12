using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000146 RID: 326
	[XmlType(TypeName = "ExtensionInstallScope", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ExtensionInstallScope
	{
		// Token: 0x040006BE RID: 1726
		None,
		// Token: 0x040006BF RID: 1727
		User,
		// Token: 0x040006C0 RID: 1728
		Organization,
		// Token: 0x040006C1 RID: 1729
		Default
	}
}
