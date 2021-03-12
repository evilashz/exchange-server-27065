using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000264 RID: 612
	[XmlType(TypeName = "ConversationQueryTraversalType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ConversationQueryTraversal
	{
		// Token: 0x04000BF6 RID: 3062
		Shallow,
		// Token: 0x04000BF7 RID: 3063
		Deep
	}
}
