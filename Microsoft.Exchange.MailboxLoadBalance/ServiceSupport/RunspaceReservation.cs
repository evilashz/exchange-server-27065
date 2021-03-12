using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.ServiceSupport
{
	// Token: 0x020000F6 RID: 246
	internal class RunspaceReservation : DisposeTrackableBase
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x00014FAF File Offset: 0x000131AF
		public RunspaceReservation(CmdletExecutionPool pool, IAnchorRunspaceProxy runspace)
		{
			this.Runspace = runspace;
			this.originatingPool = pool;
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00014FC5 File Offset: 0x000131C5
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x00014FCD File Offset: 0x000131CD
		public IAnchorRunspaceProxy Runspace { get; private set; }

		// Token: 0x06000773 RID: 1907 RVA: 0x00014FD6 File Offset: 0x000131D6
		protected override void InternalDispose(bool disposing)
		{
			this.originatingPool.ReturnRunspace(this.Runspace);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00014FE9 File Offset: 0x000131E9
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RunspaceReservation>(this);
		}

		// Token: 0x040002DC RID: 732
		private readonly CmdletExecutionPool originatingPool;
	}
}
