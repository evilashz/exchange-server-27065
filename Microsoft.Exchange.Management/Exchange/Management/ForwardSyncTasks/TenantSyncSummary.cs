using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000361 RID: 865
	public class TenantSyncSummary : OverallSyncSummaryBase
	{
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x000831E7 File Offset: 0x000813E7
		// (set) Token: 0x06001E16 RID: 7702 RVA: 0x000831EF File Offset: 0x000813EF
		public SizeAndCountStatistics ErrorCount { get; set; }

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x000831F8 File Offset: 0x000813F8
		// (set) Token: 0x06001E18 RID: 7704 RVA: 0x00083200 File Offset: 0x00081400
		public SizeAndCountStatistics ErrorSize { get; set; }

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x00083209 File Offset: 0x00081409
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x00083211 File Offset: 0x00081411
		public ThroughputStatistics ErrorBytesPerSecond { get; set; }

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x0008321A File Offset: 0x0008141A
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x00083222 File Offset: 0x00081422
		public ThroughputStatistics ErrorsPerSecond { get; set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x0008322B File Offset: 0x0008142B
		// (set) Token: 0x06001E1E RID: 7710 RVA: 0x00083233 File Offset: 0x00081433
		public override IEnumerable<IEnumerable<ISyncBatchResults>> Samples { get; set; }

		// Token: 0x06001E1F RID: 7711 RVA: 0x0008323C File Offset: 0x0008143C
		public TenantSyncSummary()
		{
			this.Samples = new List<IEnumerable<TenantSyncBatchResults>>();
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x00083334 File Offset: 0x00081534
		public override void CalculateStats()
		{
			base.CalculateStats();
			List<IEnumerable<TenantSyncBatchResults>> source = this.Samples as List<IEnumerable<TenantSyncBatchResults>>;
			if (TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site1 == null)
			{
				TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(TenantSyncSummary)));
			}
			this.ErrorCount = TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site1.Target(TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site1, SizeAndCountStatistics.Calculate(source.SelectMany((IEnumerable<TenantSyncBatchResults> iter) => from r in iter
			select (r.Stats as TenantSyncBatchStatistics).ErrorCount)));
			if (TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site2 == null)
			{
				TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site2 = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(TenantSyncSummary)));
			}
			this.ErrorSize = TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site2.Target(TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site2, SizeAndCountStatistics.Calculate(source.SelectMany((IEnumerable<TenantSyncBatchResults> iter) => from r in iter
			select (int)(r.Stats as TenantSyncBatchStatistics).ErrorSize.Sum)));
			if (TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site3 == null)
			{
				TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site3 = CallSite<Func<CallSite, object, ThroughputStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(ThroughputStatistics), typeof(TenantSyncSummary)));
			}
			this.ErrorBytesPerSecond = TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site3.Target(TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site3, ThroughputStatistics.Calculate(source.SelectMany((IEnumerable<TenantSyncBatchResults> iter) => from r in iter
			select (r.Stats as TenantSyncBatchStatistics).ErrorBytesPerSecond)));
			if (TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site4 == null)
			{
				TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site4 = CallSite<Func<CallSite, object, ThroughputStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(ThroughputStatistics), typeof(TenantSyncSummary)));
			}
			this.ErrorsPerSecond = TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site4.Target(TenantSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site4, ThroughputStatistics.Calculate(source.SelectMany((IEnumerable<TenantSyncBatchResults> iter) => from r in iter
			select (r.Stats as TenantSyncBatchStatistics).ErrorsPerSecond)));
		}

		// Token: 0x020012A1 RID: 4769
		[CompilerGenerated]
		private static class <CalculateStats>o__SiteContainer0
		{
			// Token: 0x04006878 RID: 26744
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Site1;

			// Token: 0x04006879 RID: 26745
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Site2;

			// Token: 0x0400687A RID: 26746
			public static CallSite<Func<CallSite, object, ThroughputStatistics>> <>p__Site3;

			// Token: 0x0400687B RID: 26747
			public static CallSite<Func<CallSite, object, ThroughputStatistics>> <>p__Site4;
		}
	}
}
