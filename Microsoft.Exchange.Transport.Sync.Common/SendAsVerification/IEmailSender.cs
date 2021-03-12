using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.SendAsVerification
{
	// Token: 0x020000A5 RID: 165
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IEmailSender
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600040D RID: 1037
		bool SendAttempted { get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600040E RID: 1038
		bool SendSuccessful { get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600040F RID: 1039
		string MessageId { get; }

		// Token: 0x06000410 RID: 1040
		void SendWith(Guid sharedSecret);
	}
}
