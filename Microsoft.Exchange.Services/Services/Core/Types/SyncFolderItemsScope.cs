using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200089E RID: 2206
	[XmlType("SyncFolderItemsScopeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum SyncFolderItemsScope
	{
		// Token: 0x04002418 RID: 9240
		NormalItems,
		// Token: 0x04002419 RID: 9241
		NormalAndAssociatedItems
	}
}
