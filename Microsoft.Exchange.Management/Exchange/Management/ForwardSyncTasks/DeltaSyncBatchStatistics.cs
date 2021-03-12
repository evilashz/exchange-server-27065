using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200035A RID: 858
	public class DeltaSyncBatchStatistics : SyncBatchStatisticsBase
	{
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x00082475 File Offset: 0x00080675
		// (set) Token: 0x06001DAB RID: 7595 RVA: 0x0008247D File Offset: 0x0008067D
		public int ContextCount { get; set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x00082486 File Offset: 0x00080686
		// (set) Token: 0x06001DAD RID: 7597 RVA: 0x0008248E File Offset: 0x0008068E
		public SizeAndCountStatistics ContextSize { get; set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x00082497 File Offset: 0x00080697
		// (set) Token: 0x06001DAF RID: 7599 RVA: 0x0008249F File Offset: 0x0008069F
		public double ContextsPerSecond { get; set; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x000824A8 File Offset: 0x000806A8
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x000824B0 File Offset: 0x000806B0
		public double ContextBytesPerSecond { get; set; }

		// Token: 0x06001DB2 RID: 7602 RVA: 0x000824C4 File Offset: 0x000806C4
		public void Calculate(DeltaSyncBatchResults deltaSyncBatch)
		{
			base.Calculate(deltaSyncBatch.Objects, deltaSyncBatch.Links);
			this.ContextCount = deltaSyncBatch.Contexts.Count<DirectoryContext>();
			if (DeltaSyncBatchStatistics.<Calculate>o__SiteContainer0.<>p__Site1 == null)
			{
				DeltaSyncBatchStatistics.<Calculate>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(DeltaSyncBatchStatistics)));
			}
			this.ContextSize = DeltaSyncBatchStatistics.<Calculate>o__SiteContainer0.<>p__Site1.Target(DeltaSyncBatchStatistics.<Calculate>o__SiteContainer0.<>p__Site1, SizeAndCountStatistics.Calculate(from o in deltaSyncBatch.Contexts
			select SyncBatchStatisticsBase.SerializedSize(o)));
			this.ContextsPerSecond = (double)this.ContextCount / base.ResponseTime.TotalSeconds;
			this.ContextBytesPerSecond = (double)this.ContextSize.Sum / base.ResponseTime.TotalSeconds;
		}

		// Token: 0x0200129D RID: 4765
		[CompilerGenerated]
		private static class <Calculate>o__SiteContainer0
		{
			// Token: 0x04006869 RID: 26729
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Site1;
		}
	}
}
