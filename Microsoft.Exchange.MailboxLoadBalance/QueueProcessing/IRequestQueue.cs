using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000DF RID: 223
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IRequestQueue
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060006E4 RID: 1764
		Guid Id { get; }

		// Token: 0x060006E5 RID: 1765
		void EnqueueRequest(IRequest request);

		// Token: 0x060006E6 RID: 1766
		QueueDiagnosticData GetDiagnosticData(bool includeRequestDetails, bool includeRequestVerboseDiagnostics);

		// Token: 0x060006E7 RID: 1767
		void Clean();
	}
}
