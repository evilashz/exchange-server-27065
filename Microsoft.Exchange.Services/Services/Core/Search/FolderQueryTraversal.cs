using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000268 RID: 616
	[XmlType(TypeName = "FolderQueryTraversalType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum FolderQueryTraversal
	{
		// Token: 0x04000BFF RID: 3071
		Shallow,
		// Token: 0x04000C00 RID: 3072
		Deep = 2,
		// Token: 0x04000C01 RID: 3073
		SoftDeleted = 1
	}
}
