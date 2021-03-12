using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D75 RID: 3445
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class UrlNotFoundException : ServiceDiscoveryPermanentException
	{
		// Token: 0x06007707 RID: 30471 RVA: 0x0020D9FE File Offset: 0x0020BBFE
		public UrlNotFoundException(string message, Uri url) : base(message)
		{
			this.Url = url;
		}

		// Token: 0x0400527A RID: 21114
		public readonly Uri Url;
	}
}
