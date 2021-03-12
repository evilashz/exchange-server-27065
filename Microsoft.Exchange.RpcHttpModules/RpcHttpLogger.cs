using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RpcHttpLogger : ExtensibleLogger
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003069 File Offset: 0x00001269
		public RpcHttpLogger() : base(new RpcHttpLogConfiguration())
		{
		}
	}
}
