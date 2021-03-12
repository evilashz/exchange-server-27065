using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D5B RID: 3419
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ServiceDiscoveryPermanentException : LocalizedException
	{
		// Token: 0x06007656 RID: 30294 RVA: 0x0020A75B File Offset: 0x0020895B
		public ServiceDiscoveryPermanentException(string message) : base(new LocalizedString(message))
		{
		}

		// Token: 0x06007657 RID: 30295 RVA: 0x0020A769 File Offset: 0x00208969
		public ServiceDiscoveryPermanentException(string message, Exception innerException) : base(new LocalizedString(message), innerException)
		{
		}
	}
}
