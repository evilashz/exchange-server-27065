using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200027D RID: 637
	[XmlType(TypeName = "SortDirectionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SortDirection
	{
		// Token: 0x04000C2D RID: 3117
		Ascending,
		// Token: 0x04000C2E RID: 3118
		Descending
	}
}
