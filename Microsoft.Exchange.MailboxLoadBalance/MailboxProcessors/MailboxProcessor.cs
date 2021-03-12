using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors
{
	// Token: 0x020000B9 RID: 185
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class MailboxProcessor
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x0000FD65 File Offset: 0x0000DF65
		protected MailboxProcessor(ILogger logger)
		{
			this.Logger = logger;
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000FD74 File Offset: 0x0000DF74
		public virtual string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600060B RID: 1547
		public abstract bool RequiresRunspace { get; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0000FD81 File Offset: 0x0000DF81
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x0000FD89 File Offset: 0x0000DF89
		private protected ILogger Logger { protected get; private set; }

		// Token: 0x0600060E RID: 1550
		public abstract void ProcessMailbox(DirectoryMailbox mailbox, IAnchorRunspaceProxy runspace);

		// Token: 0x0600060F RID: 1551 RVA: 0x0000FD92 File Offset: 0x0000DF92
		public virtual bool ShouldProcess(DirectoryMailbox mailbox)
		{
			return true;
		}
	}
}
