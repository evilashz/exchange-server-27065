using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008A2 RID: 2210
	[XmlType(TypeName = "TeamMailboxLifecycleStateType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum TeamMailboxLifecycleState
	{
		// Token: 0x04002428 RID: 9256
		Active,
		// Token: 0x04002429 RID: 9257
		Closed,
		// Token: 0x0400242A RID: 9258
		Unlinked,
		// Token: 0x0400242B RID: 9259
		PendingDelete
	}
}
