using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200008E RID: 142
	internal class MailboxRulesPerformanceTracker : DisposeTrackableBase
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x00019CC0 File Offset: 0x00017EC0
		public MailboxRulesPerformanceTracker(Stopwatch stopwatch)
		{
			PerformanceContext.Current.TakeSnapshot(true);
			this.initialLdap = PerformanceContext.Current.RequestCount;
			RpcDataProvider.Instance.TakeSnapshot(true);
			this.initialMapi = RpcDataProvider.Instance.RequestCount;
			this.stopwatch = stopwatch;
			this.stopwatch.Start();
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00019D1D File Offset: 0x00017F1D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxRulesPerformanceTracker>(this);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00019D28 File Offset: 0x00017F28
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.stopwatch.Stop();
				PerformanceContext.Current.TakeSnapshot(false);
				uint num = PerformanceContext.Current.RequestCount - this.initialLdap;
				MSExchangeStoreDriver.MailboxRulesActiveDirectoryQueries.IncrementBy((long)num);
				RpcDataProvider.Instance.TakeSnapshot(false);
				uint num2 = RpcDataProvider.Instance.RequestCount - this.initialMapi;
				MSExchangeStoreDriver.MailboxRulesMapiOperations.IncrementBy((long)num2);
			}
		}

		// Token: 0x040002B8 RID: 696
		private readonly Stopwatch stopwatch;

		// Token: 0x040002B9 RID: 697
		private readonly uint initialLdap;

		// Token: 0x040002BA RID: 698
		private readonly uint initialMapi;
	}
}
