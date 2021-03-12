using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NullCrossServerConnectionPolicy : ICrossServerConnectionPolicy
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public void Apply(ExRpcConnectionInfo connectionInfo)
		{
		}
	}
}
