using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D58 RID: 3416
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ServiceDiscoveryTransientException : TransientException
	{
		// Token: 0x06007649 RID: 30281 RVA: 0x0020A67B File Offset: 0x0020887B
		public ServiceDiscoveryTransientException(string message) : base(new LocalizedString(message))
		{
		}

		// Token: 0x0600764A RID: 30282 RVA: 0x0020A689 File Offset: 0x00208889
		public ServiceDiscoveryTransientException(string message, Exception innerException) : base(new LocalizedString(message), innerException)
		{
		}
	}
}
