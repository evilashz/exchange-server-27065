using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISyncReportObject
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003A0 RID: 928
		string FolderName { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003A1 RID: 929
		string Sender { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003A2 RID: 930
		string Subject { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003A3 RID: 931
		string MessageClass { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003A4 RID: 932
		int? MessageSize { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003A5 RID: 933
		ExDateTime? DateSent { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003A6 RID: 934
		ExDateTime? DateReceived { get; }
	}
}
