using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200026E RID: 622
	[XmlType(TypeName = "ItemQueryTraversalType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum ItemQueryTraversal
	{
		// Token: 0x04000C0D RID: 3085
		Shallow,
		// Token: 0x04000C0E RID: 3086
		SoftDeleted = 2,
		// Token: 0x04000C0F RID: 3087
		Associated = 1
	}
}
