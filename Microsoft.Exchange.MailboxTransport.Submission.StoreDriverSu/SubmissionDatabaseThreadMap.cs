using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000033 RID: 51
	internal sealed class SubmissionDatabaseThreadMap : SynchronizedThreadMap<string>
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000C429 File Offset: 0x0000A629
		public SubmissionDatabaseThreadMap(Trace tracer) : base(SubmissionDatabaseThreadMap.EstimatedCapacity, tracer, "Submitting mailbox database", 100, Environment.ProcessorCount * SubmissionConfiguration.Instance.App.MaxConcurrentSubmissions, "Too many concurrent submissions from mailbox database.  The limit is {1}.", true)
		{
		}

		// Token: 0x040000EB RID: 235
		private const string KeyDisplayName = "Submitting mailbox database";

		// Token: 0x040000EC RID: 236
		private const int EstimatedEntrySize = 100;

		// Token: 0x040000ED RID: 237
		private static readonly int EstimatedCapacity = Components.Configuration.LocalServer.MaxConcurrentMailboxSubmissions;
	}
}
