using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Optics
{
	// Token: 0x02000028 RID: 40
	internal class PerformanceCounterAccessorRegistry : IPerformanceCounterAccessorRegistry
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00005CC0 File Offset: 0x00003EC0
		private PerformanceCounterAccessorRegistry()
		{
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005CD3 File Offset: 0x00003ED3
		public static PerformanceCounterAccessorRegistry Instance
		{
			get
			{
				return PerformanceCounterAccessorRegistry.instance;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005CDA File Offset: 0x00003EDA
		public IPerformanceCounterAccessor GetOrAddPerformanceCounterAccessor(string type)
		{
			return this.accessorLookupTable.GetOrAdd(type, new PerformanceCounterAccessor(type));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005CEE File Offset: 0x00003EEE
		internal IEnumerable<PerformanceCounterAccessor> GetAllRegisteredAccessors()
		{
			return this.accessorLookupTable.Values;
		}

		// Token: 0x04000060 RID: 96
		private static PerformanceCounterAccessorRegistry instance = new PerformanceCounterAccessorRegistry();

		// Token: 0x04000061 RID: 97
		private ConcurrentDictionary<string, PerformanceCounterAccessor> accessorLookupTable = new ConcurrentDictionary<string, PerformanceCounterAccessor>();
	}
}
