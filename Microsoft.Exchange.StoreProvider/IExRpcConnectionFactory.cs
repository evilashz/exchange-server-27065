using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExRpcConnectionFactory
	{
		// Token: 0x060000A6 RID: 166
		ExRpcConnection Create(ExRpcConnectionInfo connectionInfo);
	}
}
