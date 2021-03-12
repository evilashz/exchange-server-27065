using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000360 RID: 864
	public class DeltaSyncSummary : OverallSyncSummaryBase
	{
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x00082EC7 File Offset: 0x000810C7
		// (set) Token: 0x06001E02 RID: 7682 RVA: 0x00082ECF File Offset: 0x000810CF
		public SizeAndCountStatistics ContextCount { get; set; }

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x00082ED8 File Offset: 0x000810D8
		// (set) Token: 0x06001E04 RID: 7684 RVA: 0x00082EE0 File Offset: 0x000810E0
		public SizeAndCountStatistics ContextSize { get; set; }

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x00082EE9 File Offset: 0x000810E9
		// (set) Token: 0x06001E06 RID: 7686 RVA: 0x00082EF1 File Offset: 0x000810F1
		public ThroughputStatistics ContextBytesPerSecond { get; set; }

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x00082EFA File Offset: 0x000810FA
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x00082F02 File Offset: 0x00081102
		public ThroughputStatistics ContextsPerSecond { get; set; }

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x00082F0B File Offset: 0x0008110B
		// (set) Token: 0x06001E0A RID: 7690 RVA: 0x00082F13 File Offset: 0x00081113
		public override IEnumerable<IEnumerable<ISyncBatchResults>> Samples { get; set; }

		// Token: 0x06001E0B RID: 7691 RVA: 0x00082F1C File Offset: 0x0008111C
		public DeltaSyncSummary()
		{
			this.Samples = new List<IEnumerable<DeltaSyncBatchResults>>();
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00083014 File Offset: 0x00081214
		public override void CalculateStats()
		{
			base.CalculateStats();
			List<IEnumerable<DeltaSyncBatchResults>> source = this.Samples as List<IEnumerable<DeltaSyncBatchResults>>;
			if (DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site1 == null)
			{
				DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(DeltaSyncSummary)));
			}
			this.ContextCount = DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site1.Target(DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site1, SizeAndCountStatistics.Calculate(source.SelectMany((IEnumerable<DeltaSyncBatchResults> iter) => from r in iter
			select (r.Stats as DeltaSyncBatchStatistics).ContextCount)));
			if (DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site2 == null)
			{
				DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site2 = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(DeltaSyncSummary)));
			}
			this.ContextSize = DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site2.Target(DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site2, SizeAndCountStatistics.Calculate(source.SelectMany((IEnumerable<DeltaSyncBatchResults> iter) => from r in iter
			select (int)(r.Stats as DeltaSyncBatchStatistics).ContextSize.Sum)));
			if (DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site3 == null)
			{
				DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site3 = CallSite<Func<CallSite, object, ThroughputStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(ThroughputStatistics), typeof(DeltaSyncSummary)));
			}
			this.ContextBytesPerSecond = DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site3.Target(DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site3, ThroughputStatistics.Calculate(source.SelectMany((IEnumerable<DeltaSyncBatchResults> iter) => from r in iter
			select (r.Stats as DeltaSyncBatchStatistics).ContextBytesPerSecond)));
			if (DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site4 == null)
			{
				DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site4 = CallSite<Func<CallSite, object, ThroughputStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(ThroughputStatistics), typeof(DeltaSyncSummary)));
			}
			this.ContextsPerSecond = DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site4.Target(DeltaSyncSummary.<CalculateStats>o__SiteContainer0.<>p__Site4, ThroughputStatistics.Calculate(source.SelectMany((IEnumerable<DeltaSyncBatchResults> iter) => from r in iter
			select (r.Stats as DeltaSyncBatchStatistics).ContextsPerSecond)));
		}

		// Token: 0x020012A0 RID: 4768
		[CompilerGenerated]
		private static class <CalculateStats>o__SiteContainer0
		{
			// Token: 0x04006874 RID: 26740
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Site1;

			// Token: 0x04006875 RID: 26741
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Site2;

			// Token: 0x04006876 RID: 26742
			public static CallSite<Func<CallSite, object, ThroughputStatistics>> <>p__Site3;

			// Token: 0x04006877 RID: 26743
			public static CallSite<Func<CallSite, object, ThroughputStatistics>> <>p__Site4;
		}
	}
}
