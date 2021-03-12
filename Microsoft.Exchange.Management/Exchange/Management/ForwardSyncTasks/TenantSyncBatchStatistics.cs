using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200035B RID: 859
	public class TenantSyncBatchStatistics : SyncBatchStatisticsBase
	{
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x000825AB File Offset: 0x000807AB
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x000825B3 File Offset: 0x000807B3
		public int ErrorCount { get; set; }

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x000825BC File Offset: 0x000807BC
		// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x000825C4 File Offset: 0x000807C4
		public SizeAndCountStatistics ErrorSize { get; set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x000825CD File Offset: 0x000807CD
		// (set) Token: 0x06001DBA RID: 7610 RVA: 0x000825D5 File Offset: 0x000807D5
		public double ErrorsPerSecond { get; set; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x000825DE File Offset: 0x000807DE
		// (set) Token: 0x06001DBC RID: 7612 RVA: 0x000825E6 File Offset: 0x000807E6
		public double ErrorBytesPerSecond { get; set; }

		// Token: 0x06001DBD RID: 7613 RVA: 0x000825F8 File Offset: 0x000807F8
		public void Calculate(TenantSyncBatchResults tenantSyncBatch)
		{
			base.Calculate(tenantSyncBatch.Objects, tenantSyncBatch.Links);
			this.ErrorCount = tenantSyncBatch.Errors.Count<DirectoryObjectError>();
			if (TenantSyncBatchStatistics.<Calculate>o__SiteContainer0.<>p__Site1 == null)
			{
				TenantSyncBatchStatistics.<Calculate>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, object, SizeAndCountStatistics>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SizeAndCountStatistics), typeof(TenantSyncBatchStatistics)));
			}
			this.ErrorSize = TenantSyncBatchStatistics.<Calculate>o__SiteContainer0.<>p__Site1.Target(TenantSyncBatchStatistics.<Calculate>o__SiteContainer0.<>p__Site1, SizeAndCountStatistics.Calculate(from o in tenantSyncBatch.Errors
			select SyncBatchStatisticsBase.SerializedSize(o)));
			this.ErrorsPerSecond = (double)this.ErrorCount / base.ResponseTime.TotalSeconds;
			this.ErrorBytesPerSecond = (double)this.ErrorSize.Sum / base.ResponseTime.TotalSeconds;
		}

		// Token: 0x0200129E RID: 4766
		[CompilerGenerated]
		private static class <Calculate>o__SiteContainer0
		{
			// Token: 0x0400686A RID: 26730
			public static CallSite<Func<CallSite, object, SizeAndCountStatistics>> <>p__Site1;
		}
	}
}
