using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;

namespace Microsoft.Exchange.MailboxLoadBalance.ServiceSupport
{
	// Token: 0x020000EE RID: 238
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CmdletExecutionPool
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x000148E4 File Offset: 0x00012AE4
		public CmdletExecutionPool(LoadBalanceAnchorContext anchorContext)
		{
			this.anchorContext = anchorContext;
			this.runspaces = new ConcurrentQueue<IAnchorRunspaceProxy>();
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x000148FE File Offset: 0x00012AFE
		public bool HasRunspacesAvailable
		{
			get
			{
				return this.checkedOutRunspaceCount < this.anchorContext.Settings.MaximumNumberOfRunspaces;
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00014918 File Offset: 0x00012B18
		public RunspaceReservation AcquireRunspace()
		{
			if (!this.HasRunspacesAvailable)
			{
				throw new CmdletPoolExhaustedException();
			}
			Interlocked.Increment(ref this.checkedOutRunspaceCount);
			IAnchorRunspaceProxy runspace;
			if (!this.runspaces.TryDequeue(out runspace))
			{
				runspace = this.GetRunspace();
			}
			return new RunspaceReservation(this, runspace);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001495C File Offset: 0x00012B5C
		public void ReturnRunspace(IAnchorRunspaceProxy runspace)
		{
			Interlocked.Decrement(ref this.checkedOutRunspaceCount);
			this.runspaces.Enqueue(runspace);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00014978 File Offset: 0x00012B78
		protected virtual IAnchorRunspaceProxy GetRunspace()
		{
			IAnchorRunspaceProxy result;
			using (OperationTracker.Create(this.anchorContext.Logger, "Creating a new runspace.", new object[0]))
			{
				result = AnchorRunspaceProxy.CreateRunspaceForDatacenterAdmin(this.anchorContext, "Mailbox Load Balance");
			}
			return result;
		}

		// Token: 0x040002CD RID: 717
		private readonly LoadBalanceAnchorContext anchorContext;

		// Token: 0x040002CE RID: 718
		private ConcurrentQueue<IAnchorRunspaceProxy> runspaces;

		// Token: 0x040002CF RID: 719
		private int checkedOutRunspaceCount;
	}
}
