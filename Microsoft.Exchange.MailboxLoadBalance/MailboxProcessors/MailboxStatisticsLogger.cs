using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Logging;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors
{
	// Token: 0x020000BB RID: 187
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxStatisticsLogger : MailboxProcessor
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		public MailboxStatisticsLogger(ILogger logger, ObjectLogCollector logCollector) : base(logger)
		{
			AnchorUtil.ThrowOnNullArgument(logCollector, "logCollector");
			this.logCollector = logCollector;
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00010003 File Offset: 0x0000E203
		public override bool RequiresRunspace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00010006 File Offset: 0x0000E206
		public override void ProcessMailbox(DirectoryMailbox mailbox, IAnchorRunspaceProxy runspace)
		{
			mailbox.EmitLogEntry(this.logCollector);
		}

		// Token: 0x04000245 RID: 581
		private readonly ObjectLogCollector logCollector;
	}
}
