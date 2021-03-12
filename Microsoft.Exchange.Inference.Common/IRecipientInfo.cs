using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRecipientInfo
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000FA RID: 250
		uint SentCount { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000FB RID: 251
		ExDateTime? FirstSentTimeUtc { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000FC RID: 252
		ExDateTime? LastSentTimeUtc { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000FD RID: 253
		string Address { get; }
	}
}
