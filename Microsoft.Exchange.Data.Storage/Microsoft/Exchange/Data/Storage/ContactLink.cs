using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200048E RID: 1166
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ContactLink
	{
		// Token: 0x060033BE RID: 13246 RVA: 0x000D217E File Offset: 0x000D037E
		protected ContactLink(MailboxInfoForLinking mailboxInfo, IExtensibleLogger logger, IContactLinkingPerformanceTracker performanceTracker)
		{
			Util.ThrowOnNullArgument(mailboxInfo, "mailboxInfo");
			Util.ThrowOnNullArgument(logger, "logger");
			Util.ThrowOnNullArgument(performanceTracker, "performanceTracker");
			this.mailboxInfo = mailboxInfo;
			this.logger = logger;
			this.performanceTracker = performanceTracker;
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x000D21BC File Offset: 0x000D03BC
		protected MailboxInfoForLinking MailboxInfo
		{
			get
			{
				return this.mailboxInfo;
			}
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000D21C4 File Offset: 0x000D03C4
		protected void Commit(IEnumerable<ContactInfoForLinking> contacts)
		{
			foreach (ContactInfoForLinking contact in contacts)
			{
				this.Commit(contact);
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x060033C1 RID: 13249 RVA: 0x000D220C File Offset: 0x000D040C
		protected IContactLinkingPerformanceTracker PerformanceTracker
		{
			get
			{
				return this.performanceTracker;
			}
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000D2214 File Offset: 0x000D0414
		protected void Commit(ContactInfoForLinking contact)
		{
			contact.Commit(this.logger, this.performanceTracker);
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000D2228 File Offset: 0x000D0428
		protected void LogEvent(ILogEvent logEvent)
		{
			this.logger.LogEvent(logEvent);
		}

		// Token: 0x04001BDB RID: 7131
		protected static readonly Trace Tracer = ExTraceGlobals.ContactLinkingTracer;

		// Token: 0x04001BDC RID: 7132
		private IExtensibleLogger logger;

		// Token: 0x04001BDD RID: 7133
		private MailboxInfoForLinking mailboxInfo;

		// Token: 0x04001BDE RID: 7134
		private IContactLinkingPerformanceTracker performanceTracker;
	}
}
