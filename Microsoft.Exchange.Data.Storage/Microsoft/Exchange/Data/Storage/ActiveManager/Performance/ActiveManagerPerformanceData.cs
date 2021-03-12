using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;

namespace Microsoft.Exchange.Data.Storage.ActiveManager.Performance
{
	// Token: 0x02000306 RID: 774
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ActiveManagerPerformanceData
	{
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x0008DB98 File Offset: 0x0008BD98
		public static ActiveManagerPerformanceData.ProviderAndLogStrings[] Providers
		{
			get
			{
				if (ActiveManagerPerformanceData.providers == null)
				{
					ActiveManagerPerformanceData.providers = new ActiveManagerPerformanceData.ProviderAndLogStrings[]
					{
						new ActiveManagerPerformanceData.ProviderAndLogStrings(ActiveManagerPerformanceData.CalculatePreferredHomeServerDataProvider)
					};
				}
				return ActiveManagerPerformanceData.providers;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x0008DBCB File Offset: 0x0008BDCB
		public static PerformanceDataProvider CalculatePreferredHomeServerDataProvider
		{
			get
			{
				if (ActiveManagerPerformanceData.calculatePreferredHomeServerDataProvider == null)
				{
					ActiveManagerPerformanceData.calculatePreferredHomeServerDataProvider = new PerformanceDataProvider("ActiveManager.CalculatePreferredHomeServer");
				}
				return ActiveManagerPerformanceData.calculatePreferredHomeServerDataProvider;
			}
		}

		// Token: 0x0400146B RID: 5227
		[ThreadStatic]
		private static ActiveManagerPerformanceData.ProviderAndLogStrings[] providers;

		// Token: 0x0400146C RID: 5228
		[ThreadStatic]
		private static PerformanceDataProvider calculatePreferredHomeServerDataProvider;

		// Token: 0x02000307 RID: 775
		public class ProviderAndLogStrings
		{
			// Token: 0x06002304 RID: 8964 RVA: 0x0008DBE8 File Offset: 0x0008BDE8
			public ProviderAndLogStrings(IPerformanceDataProvider provider)
			{
				this.provider = provider;
				this.logCount = string.Format("{0}.Count", provider.Name);
				this.logLatency = string.Format("{0}.Latency", provider.Name);
			}

			// Token: 0x17000BC4 RID: 3012
			// (get) Token: 0x06002305 RID: 8965 RVA: 0x0008DC23 File Offset: 0x0008BE23
			public IPerformanceDataProvider Provider
			{
				get
				{
					return this.provider;
				}
			}

			// Token: 0x17000BC5 RID: 3013
			// (get) Token: 0x06002306 RID: 8966 RVA: 0x0008DC2B File Offset: 0x0008BE2B
			public string LogCount
			{
				get
				{
					return this.logCount;
				}
			}

			// Token: 0x17000BC6 RID: 3014
			// (get) Token: 0x06002307 RID: 8967 RVA: 0x0008DC33 File Offset: 0x0008BE33
			public string LogLatency
			{
				get
				{
					return this.logLatency;
				}
			}

			// Token: 0x0400146D RID: 5229
			private readonly IPerformanceDataProvider provider;

			// Token: 0x0400146E RID: 5230
			private readonly string logCount;

			// Token: 0x0400146F RID: 5231
			private readonly string logLatency;
		}
	}
}
