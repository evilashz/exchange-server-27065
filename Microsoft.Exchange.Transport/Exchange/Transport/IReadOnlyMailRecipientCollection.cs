using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020000F1 RID: 241
	internal interface IReadOnlyMailRecipientCollection : IEnumerable<MailRecipient>, IEnumerable
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000952 RID: 2386
		int Count { get; }

		// Token: 0x17000256 RID: 598
		MailRecipient this[int index]
		{
			get;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000954 RID: 2388
		IEnumerable<MailRecipient> All { get; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000955 RID: 2389
		IEnumerable<MailRecipient> AllUnprocessed { get; }

		// Token: 0x06000956 RID: 2390
		bool Contains(MailRecipient item);
	}
}
