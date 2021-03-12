using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D8D RID: 3469
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CheckRecipientsResults
	{
		// Token: 0x17001FEB RID: 8171
		// (get) Token: 0x0600776C RID: 30572 RVA: 0x0020EB67 File Offset: 0x0020CD67
		// (set) Token: 0x0600776D RID: 30573 RVA: 0x0020EB6F File Offset: 0x0020CD6F
		internal ValidRecipient[] ValidRecipients { get; private set; }

		// Token: 0x17001FEC RID: 8172
		// (get) Token: 0x0600776E RID: 30574 RVA: 0x0020EB78 File Offset: 0x0020CD78
		// (set) Token: 0x0600776F RID: 30575 RVA: 0x0020EB80 File Offset: 0x0020CD80
		internal string[] InvalidRecipients { get; private set; }

		// Token: 0x17001FED RID: 8173
		// (get) Token: 0x06007770 RID: 30576 RVA: 0x0020EB89 File Offset: 0x0020CD89
		internal string TargetRecipients
		{
			get
			{
				return string.Join(";", ValidRecipient.ConvertToStringArray(this.ValidRecipients));
			}
		}

		// Token: 0x06007771 RID: 30577 RVA: 0x0020EBA0 File Offset: 0x0020CDA0
		internal CheckRecipientsResults(ValidRecipient[] validRecipients) : this(validRecipients, Array<string>.Empty)
		{
		}

		// Token: 0x06007772 RID: 30578 RVA: 0x0020EBAE File Offset: 0x0020CDAE
		internal CheckRecipientsResults(string[] invalidRecipients) : this(ValidRecipient.EmptyRecipients, invalidRecipients)
		{
		}

		// Token: 0x06007773 RID: 30579 RVA: 0x0020EBBC File Offset: 0x0020CDBC
		internal CheckRecipientsResults(ValidRecipient[] validRecipients, string[] invalidRecipients)
		{
			this.ValidRecipients = validRecipients;
			this.InvalidRecipients = invalidRecipients;
		}
	}
}
