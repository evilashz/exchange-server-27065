using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors
{
	// Token: 0x020000BD RID: 189
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ProcessMailboxRequest : BaseRequest
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x0001004C File Offset: 0x0000E24C
		public ProcessMailboxRequest(DirectoryMailbox mailbox, MailboxProcessor processor, ILogger logger, CmdletExecutionPool cmdletPool)
		{
			AnchorUtil.ThrowOnNullArgument(mailbox, "mailbox");
			AnchorUtil.ThrowOnNullArgument(processor, "processor");
			this.Mailbox = mailbox;
			this.Processor = processor;
			this.logger = logger;
			this.cmdletPool = cmdletPool;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00010087 File Offset: 0x0000E287
		public override bool IsBlocked
		{
			get
			{
				return !this.cmdletPool.HasRunspacesAvailable;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00010097 File Offset: 0x0000E297
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0001009F File Offset: 0x0000E29F
		internal MailboxProcessor Processor { get; private set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x000100A8 File Offset: 0x0000E2A8
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x000100B0 File Offset: 0x0000E2B0
		internal DirectoryMailbox Mailbox { get; private set; }

		// Token: 0x06000621 RID: 1569 RVA: 0x000100BC File Offset: 0x0000E2BC
		public override RequestDiagnosticData GetDiagnosticData(bool verbose)
		{
			RequestDiagnosticData diagnosticData = base.GetDiagnosticData(verbose);
			diagnosticData.RequestKind = string.Format("ProcessMailboxRequest/{0}", this.Processor.Name);
			return diagnosticData;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000100F0 File Offset: 0x0000E2F0
		protected override void ProcessRequest()
		{
			using (OperationTracker.Create(this.logger, "Applying processor {0} to mailbox {1}.", new object[]
			{
				this.Processor.GetType().FullName,
				this.Mailbox.Identity
			}))
			{
				using (RunspaceReservation runspaceReservation = this.cmdletPool.AcquireRunspace())
				{
					this.Processor.ProcessMailbox(this.Mailbox, runspaceReservation.Runspace);
				}
			}
		}

		// Token: 0x04000248 RID: 584
		private readonly ILogger logger;

		// Token: 0x04000249 RID: 585
		private readonly CmdletExecutionPool cmdletPool;
	}
}
