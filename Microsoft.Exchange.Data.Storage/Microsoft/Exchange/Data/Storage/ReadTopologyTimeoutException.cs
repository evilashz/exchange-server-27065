using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D59 RID: 3417
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ReadTopologyTimeoutException : ServiceDiscoveryTransientException
	{
		// Token: 0x0600764B RID: 30283 RVA: 0x0020A698 File Offset: 0x00208898
		public ReadTopologyTimeoutException(string message) : base(message)
		{
		}
	}
}
