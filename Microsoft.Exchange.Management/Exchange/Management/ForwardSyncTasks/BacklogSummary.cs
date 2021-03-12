using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Description;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000363 RID: 867
	public class BacklogSummary
	{
		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x00083555 File Offset: 0x00081755
		// (set) Token: 0x06001E2F RID: 7727 RVA: 0x0008355D File Offset: 0x0008175D
		public SizeAndCountStatistics LinkCount { get; set; }

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x00083566 File Offset: 0x00081766
		// (set) Token: 0x06001E31 RID: 7729 RVA: 0x0008356E File Offset: 0x0008176E
		public SizeAndCountStatistics ObjectCount { get; set; }

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x00083577 File Offset: 0x00081777
		// (set) Token: 0x06001E33 RID: 7731 RVA: 0x0008357F File Offset: 0x0008177F
		public ResponseTimeStatistics ResponseTime { get; set; }

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x00083588 File Offset: 0x00081788
		// (set) Token: 0x06001E35 RID: 7733 RVA: 0x00083590 File Offset: 0x00081790
		public ServiceEndpoint MsoEndPoint { get; set; }

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x00083599 File Offset: 0x00081799
		// (set) Token: 0x06001E37 RID: 7735 RVA: 0x000835A1 File Offset: 0x000817A1
		public IEnumerable<BacklogEstimateResults> Batches { get; set; }

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x000835B7 File Offset: 0x000817B7
		public int TotalTenants
		{
			get
			{
				return this.Batches.Sum((BacklogEstimateResults batch) => batch.ContextBacklogs.Count<ContextBacklogMeasurement>());
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x00083614 File Offset: 0x00081814
		public IEnumerable<string> TopTenTenants
		{
			get
			{
				if (this.topTenTenants == null)
				{
					this.topTenTenants = from t in this.Batches.SelectMany((BacklogEstimateResults batch) => batch.ContextBacklogs).OrderByDescending((ContextBacklogMeasurement t) => t, BacklogSummary.BacklogComparer).Take(10)
					select string.Format("{0} O:{1} L:{2}", t.ContextId, t.Objects, t.Links);
				}
				return this.topTenTenants;
			}
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x000836AD File Offset: 0x000818AD
		public BacklogSummary()
		{
			this.Batches = new List<BacklogEstimateResults>();
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0008372C File Offset: 0x0008192C
		public virtual void CalculateStats()
		{
			if (BacklogSummary.<CalculateStats>o__SiteContainer8.<>p__Site9 == null)
			{
				BacklogSummary.<CalculateStats>o__SiteContainer8.<>p__Site9 = CallSite<Func<CallSite, object, ResponseTimeStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(ResponseTimeStatistics), typeof(BacklogSummary)));
			}
			this.ResponseTime = BacklogSummary.<CalculateStats>o__SiteContainer8.<>p__Site9.Target(BacklogSummary.<CalculateStats>o__SiteContainer8.<>p__Site9, ResponseTimeStatistics.Calculate(from r in this.Batches
			select r.ResponseTime));
			if (BacklogSummary.<CalculateStats>o__SiteContainer8.<>p__Sitea == null)
			{
				BacklogSummary.<CalculateStats>o__SiteContainer8.<>p__Sitea = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(BacklogSummary)));
			}
			this.ObjectCount = BacklogSummary.<CalculateStats>o__SiteContainer8.<>p__Sitea.Target(BacklogSummary.<CalculateStats>o__SiteContainer8.<>p__Sitea, SizeAndCountStatistics.Calculate(this.Batches.SelectMany((BacklogEstimateResults batch) => from b in batch.ContextBacklogs
			select (int)b.Objects)));
			if (BacklogSummary.<CalculateStats>o__SiteContainer8.<>p__Siteb == null)
			{
				BacklogSummary.<CalculateStats>o__SiteContainer8.<>p__Siteb = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(BacklogSummary)));
			}
			this.LinkCount = BacklogSummary.<CalculateStats>o__SiteContainer8.<>p__Siteb.Target(BacklogSummary.<CalculateStats>o__SiteContainer8.<>p__Siteb, SizeAndCountStatistics.Calculate(this.Batches.SelectMany((BacklogEstimateResults batch) => from b in batch.ContextBacklogs
			select (int)b.Links)));
		}

		// Token: 0x0400190A RID: 6410
		private static BacklogSummary.ContextBacklogMeasurementComparer BacklogComparer = new BacklogSummary.ContextBacklogMeasurementComparer();

		// Token: 0x0400190B RID: 6411
		private IEnumerable<string> topTenTenants;

		// Token: 0x02000364 RID: 868
		public class ContextBacklogMeasurementComparer : IComparer<ContextBacklogMeasurement>
		{
			// Token: 0x06001E46 RID: 7750 RVA: 0x0008389C File Offset: 0x00081A9C
			public int Compare(ContextBacklogMeasurement x, ContextBacklogMeasurement y)
			{
				uint num = x.Objects + x.Links - y.Objects - y.Links;
				if (num != 0U)
				{
					return (int)num;
				}
				return (int)(x.Objects - y.Objects);
			}
		}

		// Token: 0x020012A2 RID: 4770
		[CompilerGenerated]
		private static class <CalculateStats>o__SiteContainer8
		{
			// Token: 0x0400687C RID: 26748
			public static CallSite<Func<CallSite, object, ResponseTimeStatistics>> <>p__Site9;

			// Token: 0x0400687D RID: 26749
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Sitea;

			// Token: 0x0400687E RID: 26750
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Siteb;
		}
	}
}
