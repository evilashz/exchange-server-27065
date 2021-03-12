using System;
using System.Net;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000057 RID: 87
	public interface ICredentialHandler
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060006B2 RID: 1714
		NetworkCredential Credential { get; }

		// Token: 0x060006B3 RID: 1715
		bool RequestCredential(Uri serviceEndpoint);

		// Token: 0x060006B4 RID: 1716
		void InvalidateCredential(Uri serviceEndpoint);
	}
}
