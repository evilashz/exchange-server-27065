using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Description;
using Microsoft.CSharp.RuntimeBinder;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200035F RID: 863
	public class OverallSyncSummaryBase
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x00082827 File Offset: 0x00080A27
		// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x0008282F File Offset: 0x00080A2F
		public SizeAndCountStatistics LinkCount { get; set; }

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x00082838 File Offset: 0x00080A38
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x00082840 File Offset: 0x00080A40
		public SizeAndCountStatistics LinkSize { get; set; }

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x00082849 File Offset: 0x00080A49
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x00082851 File Offset: 0x00080A51
		public ThroughputStatistics LinkBytesPerSecond { get; set; }

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0008285A File Offset: 0x00080A5A
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x00082862 File Offset: 0x00080A62
		public ThroughputStatistics LinksPerSecond { get; set; }

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x0008286B File Offset: 0x00080A6B
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x00082873 File Offset: 0x00080A73
		public SizeAndCountStatistics ObjectCount { get; set; }

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0008287C File Offset: 0x00080A7C
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x00082884 File Offset: 0x00080A84
		public SizeAndCountStatistics ObjectSize { get; set; }

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x0008288D File Offset: 0x00080A8D
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x00082895 File Offset: 0x00080A95
		public ThroughputStatistics ObjectBytesPerSecond { get; set; }

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x0008289E File Offset: 0x00080A9E
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x000828A6 File Offset: 0x00080AA6
		public ThroughputStatistics ObjectsPerSecond { get; set; }

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x000828AF File Offset: 0x00080AAF
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x000828B7 File Offset: 0x00080AB7
		public ResponseTimeStatistics ResponseTime { get; set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x000828C0 File Offset: 0x00080AC0
		// (set) Token: 0x06001DEA RID: 7658 RVA: 0x000828C8 File Offset: 0x00080AC8
		public ServiceEndpoint MsoEndPoint { get; set; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x000828D1 File Offset: 0x00080AD1
		// (set) Token: 0x06001DEC RID: 7660 RVA: 0x000828D9 File Offset: 0x00080AD9
		public virtual IEnumerable<IEnumerable<ISyncBatchResults>> Samples { get; set; }

		// Token: 0x06001DED RID: 7661 RVA: 0x00082AB0 File Offset: 0x00080CB0
		public virtual void CalculateStats()
		{
			if (OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site1 == null)
			{
				OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, object, ResponseTimeStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(ResponseTimeStatistics), typeof(OverallSyncSummaryBase)));
			}
			this.ResponseTime = OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site1.Target(OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site1, ResponseTimeStatistics.Calculate(this.Samples.SelectMany((IEnumerable<ISyncBatchResults> iter) => from r in iter
			select r.Stats.ResponseTime)));
			if (OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site2 == null)
			{
				OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site2 = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(OverallSyncSummaryBase)));
			}
			this.ObjectCount = OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site2.Target(OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site2, SizeAndCountStatistics.Calculate(this.Samples.SelectMany((IEnumerable<ISyncBatchResults> iter) => from r in iter
			select r.Stats.ObjectCount)));
			if (OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site3 == null)
			{
				OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site3 = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(OverallSyncSummaryBase)));
			}
			this.LinkCount = OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site3.Target(OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site3, SizeAndCountStatistics.Calculate(this.Samples.SelectMany((IEnumerable<ISyncBatchResults> iter) => from r in iter
			select r.Stats.LinkCount)));
			if (OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site4 == null)
			{
				OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site4 = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(OverallSyncSummaryBase)));
			}
			this.ObjectSize = OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site4.Target(OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site4, SizeAndCountStatistics.Calculate(this.Samples.SelectMany((IEnumerable<ISyncBatchResults> iter) => from r in iter
			select (int)r.Stats.ObjectSize.Sum)));
			if (OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site5 == null)
			{
				OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site5 = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(OverallSyncSummaryBase)));
			}
			this.LinkSize = OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site5.Target(OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site5, SizeAndCountStatistics.Calculate(this.Samples.SelectMany((IEnumerable<ISyncBatchResults> iter) => from r in iter
			select (int)r.Stats.LinkSize.Sum)));
			if (OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site6 == null)
			{
				OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site6 = CallSite<Func<CallSite, object, ThroughputStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(ThroughputStatistics), typeof(OverallSyncSummaryBase)));
			}
			this.ObjectBytesPerSecond = OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site6.Target(OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site6, ThroughputStatistics.Calculate(this.Samples.SelectMany((IEnumerable<ISyncBatchResults> iter) => from r in iter
			select r.Stats.ObjectBytesPerSecond)));
			if (OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site7 == null)
			{
				OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site7 = CallSite<Func<CallSite, object, ThroughputStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(ThroughputStatistics), typeof(OverallSyncSummaryBase)));
			}
			this.LinkBytesPerSecond = OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site7.Target(OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site7, ThroughputStatistics.Calculate(this.Samples.SelectMany((IEnumerable<ISyncBatchResults> iter) => from r in iter
			select r.Stats.LinkBytesPerSecond)));
			if (OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site8 == null)
			{
				OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site8 = CallSite<Func<CallSite, object, ThroughputStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(ThroughputStatistics), typeof(OverallSyncSummaryBase)));
			}
			this.ObjectsPerSecond = OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site8.Target(OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site8, ThroughputStatistics.Calculate(this.Samples.SelectMany((IEnumerable<ISyncBatchResults> iter) => from r in iter
			select r.Stats.ObjectsPerSecond)));
			if (OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site9 == null)
			{
				OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site9 = CallSite<Func<CallSite, object, ThroughputStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(ThroughputStatistics), typeof(OverallSyncSummaryBase)));
			}
			this.LinksPerSecond = OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site9.Target(OverallSyncSummaryBase.<CalculateStats>o__SiteContainer0.<>p__Site9, ThroughputStatistics.Calculate(this.Samples.SelectMany((IEnumerable<ISyncBatchResults> iter) => from r in iter
			select r.Stats.LinksPerSecond)));
		}

		// Token: 0x0200129F RID: 4767
		[CompilerGenerated]
		private static class <CalculateStats>o__SiteContainer0
		{
			// Token: 0x0400686B RID: 26731
			public static CallSite<Func<CallSite, object, ResponseTimeStatistics>> <>p__Site1;

			// Token: 0x0400686C RID: 26732
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Site2;

			// Token: 0x0400686D RID: 26733
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Site3;

			// Token: 0x0400686E RID: 26734
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Site4;

			// Token: 0x0400686F RID: 26735
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Site5;

			// Token: 0x04006870 RID: 26736
			public static CallSite<Func<CallSite, object, ThroughputStatistics>> <>p__Site6;

			// Token: 0x04006871 RID: 26737
			public static CallSite<Func<CallSite, object, ThroughputStatistics>> <>p__Site7;

			// Token: 0x04006872 RID: 26738
			public static CallSite<Func<CallSite, object, ThroughputStatistics>> <>p__Site8;

			// Token: 0x04006873 RID: 26739
			public static CallSite<Func<CallSite, object, ThroughputStatistics>> <>p__Site9;
		}
	}
}
