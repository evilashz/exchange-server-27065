using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000098 RID: 152
	public enum ItemLoadStatus
	{
		// Token: 0x040001B1 RID: 433
		[LocDescription(Strings.IDs.Loading)]
		Loading,
		// Token: 0x040001B2 RID: 434
		[LocDescription(Strings.IDs.ResolverObjectNotFound)]
		Failed
	}
}
