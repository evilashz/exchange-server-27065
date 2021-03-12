using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICrossServerConnectionPolicy
	{
		// Token: 0x06000068 RID: 104
		void Apply(ExRpcConnectionInfo connectionInfo);
	}
}
