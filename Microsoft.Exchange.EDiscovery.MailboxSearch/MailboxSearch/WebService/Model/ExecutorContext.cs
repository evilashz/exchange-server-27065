using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x0200003C RID: 60
	internal class ExecutorContext : DisposeTrackableBase
	{
		// Token: 0x060002DA RID: 730 RVA: 0x00014404 File Offset: 0x00012604
		public ExecutorContext()
		{
			this.Failures = new ConcurrentBag<Exception>();
			this.CancellationTokenSource = new CancellationTokenSource();
			this.WaitHandle = new ManualResetEvent(false);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0001442E File Offset: 0x0001262E
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00014436 File Offset: 0x00012636
		public CancellationTokenSource CancellationTokenSource { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0001443F File Offset: 0x0001263F
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00014447 File Offset: 0x00012647
		public ManualResetEvent WaitHandle { get; private set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00014450 File Offset: 0x00012650
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00014458 File Offset: 0x00012658
		public Exception FatalException { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00014461 File Offset: 0x00012661
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x00014469 File Offset: 0x00012669
		public ConcurrentBag<Exception> Failures { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00014472 File Offset: 0x00012672
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0001447A File Offset: 0x0001267A
		public object Input { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00014483 File Offset: 0x00012683
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0001448B File Offset: 0x0001268B
		public object Output { get; set; }

		// Token: 0x060002E7 RID: 743 RVA: 0x00014494 File Offset: 0x00012694
		protected override void InternalDispose(bool disposing)
		{
			this.WaitHandle.Dispose();
			this.CancellationTokenSource.Dispose();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000144AC File Offset: 0x000126AC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ExecutorContext>(this);
		}
	}
}
