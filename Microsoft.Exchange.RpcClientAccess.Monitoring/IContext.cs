using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000031 RID: 49
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IContext
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000149 RID: 329
		ILogger Logger { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600014A RID: 330
		IEnvironment Environment { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600014B RID: 331
		IPropertyBag Properties { get; }

		// Token: 0x0600014C RID: 332
		IContext CreateDerived();
	}
}
