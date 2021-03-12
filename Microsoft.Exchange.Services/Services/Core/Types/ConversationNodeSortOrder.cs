using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200072E RID: 1838
	[XmlType(TypeName = "ConversationNodeSortOrder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum ConversationNodeSortOrder
	{
		// Token: 0x04001EF2 RID: 7922
		TreeOrderAscending,
		// Token: 0x04001EF3 RID: 7923
		TreeOrderDescending,
		// Token: 0x04001EF4 RID: 7924
		DateOrderAscending,
		// Token: 0x04001EF5 RID: 7925
		DateOrderDescending
	}
}
