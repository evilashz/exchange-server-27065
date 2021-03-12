using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000909 RID: 2313
	internal abstract class CalendarActionBase<T> where T : CalendarActionResponse
	{
		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x000DFF46 File Offset: 0x000DE146
		// (set) Token: 0x06004317 RID: 17175 RVA: 0x000DFF4E File Offset: 0x000DE14E
		private protected MailboxSession MailboxSession { protected get; private set; }

		// Token: 0x06004318 RID: 17176 RVA: 0x000DFF57 File Offset: 0x000DE157
		public CalendarActionBase(MailboxSession session)
		{
			this.MailboxSession = session;
		}

		// Token: 0x06004319 RID: 17177
		public abstract T Execute();
	}
}
