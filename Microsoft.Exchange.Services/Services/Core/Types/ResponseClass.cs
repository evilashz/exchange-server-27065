using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000546 RID: 1350
	[XmlType("ResponseClassType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum ResponseClass
	{
		// Token: 0x040015F0 RID: 5616
		Success,
		// Token: 0x040015F1 RID: 5617
		Warning,
		// Token: 0x040015F2 RID: 5618
		Error
	}
}
